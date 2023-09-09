<template>
    <el-form style="position: absolute;top:13%;left:28%">
    <el-form-item v-for="(item, index) in list" :key="index">
        <!-- 每条申请对应一条卡片 -->
        <el-card :class='index==currentCard?"profession-list-active":"profession-list-inactive"' @click.native="intoCard(index)">
            <!-- 下面组件的v-if都不能省略，否则刷新页面会出错 -->
            <userAvatar v-if="Info[index]" :userId=item.id :width=50 :addLink="0"
                style="position: absolute;top:15%;left:5%;"></userAvatar>
            <b v-if="Info[index]"
                style="position: absolute;top:15%;left:20%;font-size: 18px;color:rgb(61, 61, 61)">
                用户名：{{ Info[index].data.name }}
            </b>
            <b style="position: absolute;top:55%;left:20%;font-size: 14px;color:rgb(120, 120, 120)">
                ID：{{ item.id }}
            </b>
            <el-button class="pass_btn" style="position:absolute;top:25%;left:78%;"
                @click.stop="pass(item.requestId)">
                <el-icon>
                    <Check />
                </el-icon>
            </el-button>
            <el-button class="close_btn" style="position: absolute;top:25%;left:85%;"
                @click.stop="decline(item.requestId)">
                <el-icon>
                    <Close />
                </el-icon>
            </el-button>
        </el-card>
    </el-form-item>
    </el-form>
    <el-card class="card" v-show="card_show" v-if="Info[currentCard]">
    <!-- 资料信息的详细显示 -->
    <b style="position:absolute;top:8%;left:5%;">ID</b>
    <div class="blank" style="top:6%;left:20%;">
        <div style="position:absolute;top:28%;left:5%">{{ list[currentCard].id }} </div>
    </div>
    <b style="position:absolute;top:20%;left:5%;">用户名</b>
    <div class="blank" style="top:18%;left:20%;">
        <div style="position:absolute;top:28%;left:5%">{{ Info[currentCard].data.name }} </div>
    </div>

    <b style="position:absolute;top:32%;left:5%;">邮箱</b>
    <div class="blank" style="top:30%;left:20%;">
        <div style="position:absolute;top:28%;left:5%">{{ Info[currentCard].data.email }} </div>
    </div>

    <b style="position:absolute;top:44%;left:5%;">阐述</b>
    <div class="blank" style="top:42%;left:20%;">
        <div style="position:absolute;top:28%;left:5%">{{ list[currentCard].illustrate }} </div>
    </div>

    <b style="position:absolute;top:56%;left:5%;">上传资料</b>
    <div class="blank-evidence" style="top:54%;left:20%;">
        <div style="position:absolute;top:7%;left:5%">{{ list[currentCard].evidence }} </div>
    </div>
    <el-button class="pass_btn" style="position: absolute;bottom:5%;left:42%;"
        @click="pass(list[currentCard].requestId)">
        <el-icon>
            <Check />
        </el-icon>
    </el-button>
    <el-button class="close_btn" style="position: absolute;bottom:5%;left:52%;"
        @click="decline(list[currentCard].requestId)">
        <el-icon>
            <Close />
        </el-icon>
    </el-button>
    </el-card>

</template>

<script setup>
import { ref } from 'vue';
import { ProfessionToDeal, DealProfession } from '@/api/profession';  // 引入 api 请求函数
import { GetInfoByID } from '@/api/user';  // 引入 api 请求函数

const card_show = ref(false);//用以点击进入申请信息的详情界面
const list = ref([]); // 定义并初始化 list 变量
const Info = ref([]); // 定义并初始化 Info 变量
const currentCard = ref(-1);//用来记录当前显示的资料卡片（index）

const GetList = () => {
    //将获取列表信息的接口封装在函数中
    ProfessionToDeal()
        .then(function (result) {
            afterGet(result);
        })
        .catch(function (error) {
            console.log(error);
        });
}

const afterGet = (request) => {
    list.value = request.data;//申请信息放入list中
    for (let i = 0; i < list.value.length; i++) {
        (function (index) {
            let params = {
                ID: list.value[index].id,
                type: 1
            }
            GetInfoByID(params)//获取对应申请的用户信息
                .then(function (result) {
                    Info.value[i] = result;
                })
                .catch(function (error) {
                    console.log(error);
                });
        })(i);
    }
}
GetList();//获取列表，给list赋值

const intoCard = (index) => {
    //进入卡片
    if (index == currentCard.value) {
        card_show.value = !card_show.value;
        currentCard.value = -1;
    }
    else {
        card_show.value = true;
        currentCard.value = index;
    }

}

const pass = (requestId) => {
    //审核通过
    let params = {
        request_id: requestId,
        response: 1
    }
    DealProfession(params)
        .then(function (result) {
            /*通过之后的操作 */
            GetList();
            currentCard.value = -1;
            card_show.value = false;
        })
        .catch(function (error) {
            console.log(error);
        });
}
const decline = (requestId) => {
    //审核不通过
    let params = {
        request_id: requestId,
        response: 2
    }
    DealProfession(params)
        .then(function (result) {
            /*不通过之后的操作 */
            GetList();
            currentCard.value = -1;
            card_show.value = false;
        })
        .catch(function (error) {
            console.log(error);
        });
}
</script>

<style scoped>
.profession-list-inactive {
    background-color:#E7F9FD;
    width: 500px;
    height: 70px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 10px;
}

.profession-list-active {
    background-color:#CCFFCA;
    width: 500px;
    height: 70px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 10px;
}

.pass_btn {
    /* 设置按钮风格 */
    width: 35px;
    height: 35px;
    border-radius: 50px;
    background-color: rgb(255, 255, 255);
    color: rgb(45, 45, 45);
    border-color: transparent;
    box-shadow: 0px 4px 4px 0px gray;
}

.close_btn {
    width: 35px;
    height: 35px;
    border-radius: 50px;
    background-color: rgb(32, 32, 32);
    color: rgb(255, 255, 255);
    border-color: transparent;
    box-shadow: 0px 4px 4px 0px gray;
}

.card {
    position: fixed;
    top: 13%;
    right: 3%;
    background-color: #CCFFCA;
    width: 480px;
    height: 700px;
    box-shadow: 4px 4px 4px 2px gray;
    border-radius: 10px;
}

.blank {
    position: absolute;
    font-size: 16px;
    background-color:rgb(255, 255, 255);
    width: 350px;
    height: 50px;
    border-radius: 10px;
}

.blank-evidence{
    position: absolute;
    font-size: 16px;
    background-color:rgb(255, 255, 255);
    width: 350px;
    height: 200px;
    border-radius: 10px;
}
</style>