<template>
    <div>
        <div class="profession-page">
            <el-form style="position: absolute;top:20%;left:7%">
                <el-form-item v-for="(item, index) in list">
                    <!-- 每条申请对应一条卡片 -->
                    <el-card class="profession-list" @click.native="intoCard(index)">
                        <!-- 下面组件的v-if都不能省略，否则刷新页面会出错 -->
                        <el-avatar v-if="Info[index]" :size="70" :src="Info[index].avatar"
                            style="position: absolute;top:15%;left:5%"></el-avatar>
                        <b v-if="Info[index]"
                            style="position: absolute;top:2 0%;left:25%;font-size: 18px;color:rgb(61, 61, 61)">
                            用户名：{{ Info[index].name }}
                        </b>
                        <b style="position: absolute;top:55%;left:25%;font-size: 14px;color:rgb(120, 120, 120)">
                            ID：{{ item.id }}
                        </b>
                        <el-button class="pass_btn" style="position:absolute;top:35%;left:78%;"
                            @click.stop="pass(item.requestId)">
                            <el-icon>
                                <Check />
                            </el-icon>
                        </el-button>
                        <el-button class="close_btn" style="position: absolute;top:35%;left:85%;"
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
                <el-avatar :size="90" :src="Info[currentCard].avatar" style="position: absolute;top:8%;left:7%"></el-avatar>
                <b style="position: absolute;top:10%;left:25%;font-size: 20px;color:rgb(61, 61, 61)">
                    用户名：{{ Info[currentCard].name }}
                </b>
                <b style="position: absolute;top:16%;left:25%;font-size: 16px;color:rgb(120, 120, 120)">
                    ID：{{ list[currentCard].id }}
                </b>
                <b style="position: absolute;top:30%;left:10%;font-size: 20px;color:rgb(61, 61, 61)">
                    邮箱：{{ Info[currentCard].email }}
                </b>
                <b style="position: absolute;top:40%;left:10%;font-size: 20px;color:rgb(61, 61, 61)">
                    手机号：{{ Info[currentCard].tel }}
                </b>
                <b style="position: absolute;top:50%;left:10%;font-size: 20px;color:rgb(61, 61, 61)">
                    阐述：{{ list[currentCard].illustrate }}
                </b>
                <b style="position: absolute;top:60%;left:10%;font-size: 20px;color:rgb(61, 61, 61)">
                    上传资料：{{ list[currentCard].evidence }}
                </b>
                <el-button class="pass_btn" style="position: absolute;bottom:5%;left:45%;"
                    @click="pass(list[currentCard].requestId)">
                    <el-icon>
                        <Check />
                    </el-icon>
                </el-button>
                <el-button class="close_btn" style="position: absolute;bottom:5%;left:55%;"
                    @click="decline(list[currentCard].requestId)">
                    <el-icon>
                        <Close />
                    </el-icon>
                </el-button>
            </el-card>
            <el-icon size="30px" style="position:absolute;top: 8%;left:3%" @click="backToHome">
                <Back />
            </el-icon>
        </div>
    </div>
</template>
  
<script setup>
import { ref } from 'vue';
import router from "@/router/index.js"
import { ProfessionToDeal, DealProfession } from '@/api/profession';  // 引入 api 请求函数
import { GetInfoByID } from '@/api/user';

const card_show = ref(false);//用以点击进入申请信息的详情界面
const list = ref([]); // 定义并初始化 list 变量
const Info = ref([]); // 定义并初始化 Info 变量
const currentCard = ref();//用来记录当前显示的资料卡片（index）

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
    }
    else {
        card_show.value = true;
    }
    currentCard.value = index;
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
const backToHome = () => {
    router.push({ name: 'homeAdmin' })
}
</script>
  
<style scoped>
.profession-page {
    background-image: url('@/assets/profession-background.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 98vh;
    width: 99vw;
}

.profession-list {
    background-color: #ace5d8;
    width: 500px;
    height: 100px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 30px;
}

.pass_btn {
    /* 设置按钮风格 */
    width: 35px;
    height: 35px;
    border-radius: 50px;
    background-color: rgb(104, 230, 121);
    color: rgb(255, 255, 255);
    border-color: transparent;
    box-shadow: 0px 4px 4px 0px gray;
}

.close_btn {
    width: 35px;
    height: 35px;
    border-radius: 50px;
    background-color: rgb(230, 104, 104);
    color: rgb(255, 255, 255);
    border-color: transparent;
    box-shadow: 0px 4px 4px 0px gray;
}

.card {
    position: fixed;
    top: 13%;
    left: 48%;
    background-color: #ffffff;
    width: 700px;
    height: 650px;
    box-shadow: 4px 4px 4px 2px gray;
    border-radius: 10px;
}</style>
  