<template>
    <div>
        <div class="checkComment-page">
            <el-form style="position: absolute;top:20%;left:7%">
                <el-form-item v-for="(item, index) in list" :key="index">
                    <el-card class="comment-list" @click.native="goComment(index)">
                        <!-- 下面组件的v-if都不能省略，否则刷新页面会出错 -->
                        <b v-if="Info[index]"
                            style="position: absolute;top:20%;left:5%;font-size: 18px;color:rgb(61, 61, 61)">
                            留言者：{{ Info[index].authorName }}
                        </b>
                        <b style="position: absolute;top:55%;left:5%;font-size: 14px;color:rgb(120, 120, 120)">
                            留言ID：{{ item.msgId }}
                        </b>
                        <el-button class="pass_btn" style="position:absolute;top:35%;left:78%;"
                            @click="pass(item.reportId)">
                            <el-icon>
                                <Check />
                            </el-icon>
                        </el-button>
                        <el-button class="close_btn" style="position: absolute;top:35%;left:85%;"
                            @click="decline(item.reportId)">
                            <el-icon>
                                <Close />
                            </el-icon>
                        </el-button>
                    </el-card>
                </el-form-item>
            </el-form>

            <el-card class="card" v-show="card_show" v-if="Info[currentCard]">
                <el-avatar :size="70" :src="Info[currentCard].authorAvatar"
                    style="position: absolute;top:8%;left:7%"></el-avatar>
                <b style="position: absolute;top:9%;left:25%;font-size: 20px;color:rgb(61, 61, 61)">
                    留言者：{{ Info[currentCard].authorName }}
                </b>
                <b style="position: absolute;top:16%;left:25%;font-size: 16px;color:rgb(120, 120, 120)">
                    留言者ID：{{ Info[currentCard].authorId }}
                </b>
                <b style="position: absolute;top:28%;left:9%;font-size: 20px;color:rgb(61, 61, 61)">
                    举报原因：{{ list[currentCard].reason }}
                </b>
                <b style="position: absolute;top:40%;left:9%;font-size: 20px;color:rgb(61, 61, 61)">
                    留言ID：{{ list[currentCard].msgId }}
                </b>
                <b style="position: absolute;top:52%;left:9%;font-size: 20px;color:rgb(61, 61, 61)">
                    留言内容：{{ Info[currentCard].content }}
                </b>
                <el-button class="pass_btn" style="position: absolute;bottom:5%;left:45%;"
                    @click="pass(list[currentCard].reportId)">
                    <el-icon>
                        <Check />
                    </el-icon>
                </el-button>
                <el-button class="close_btn" style="position: absolute;bottom:5%;left:55%;"
                    @click="decline(list[currentCard].reportId)">
                    <el-icon>
                        <Close />
                    </el-icon>
                </el-button>
            </el-card>

            <el-icon class="icon" @click="back">
                <Back />
            </el-icon>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"
import { ReportCommentToDeal, DealMsgReportAync } from '@/api/ReportComment';  // 引入 api 请求函数
import { GetCommentDetailsAsync } from '@/api/comment';
import { useStore } from 'vuex'//引入store
const store = useStore();//使用store必须加上

const card_show = ref(false);//用以点击进入申请信息的详情界面
const currentCard = ref();//用来记录当前显示的资料卡片（index）
const list = ref([]); // 定义并初始化 list 变量
const Info = reactive([]); // 定义并初始化 Info 变量

const GetList = () => {
    //将获取列表信息的接口封装在函数中
    ReportCommentToDeal()
        .then(function (result) {
            afterGet(result);
        })
        .catch(function (error) {
            console.log(error);
        });
}

const afterGet = async (request) => {
    list.value = request.data;//申请信息放入list中
    for (let i = 0; i < list.value.length; i++) {
        (function (index) {
            let params = {
                msg_id: list.value[index].msgId,
            }
            GetCommentDetailsAsync(params)//获取对应举报的留言信息
                .then(function (result) {
                    Info[i] = result.data[0];
                })
                .catch(function (error) {
                    console.log(error);
                });
        })(i);
    }
}
GetList();//获取列表，给list赋值

const goComment = (index) => {
    //进入留言详情页面（卡片）
    if (index == currentCard.value) {
        card_show.value = !card_show.value;
    }
    else {
        card_show.value = true;
    }
    currentCard.value = index;
}

const pass = (reportId) => {
    //审核通过
    let params = {
        report_id: reportId,
        adminId: store.state.Info.id,
        is_true: 1,
        result: "情况属实，已做处理"
    }
    DealMsgReportAync(params)
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
const decline = (reportId) => {
    //审核不通过
    let params = {
        report_id: reportId,
        adminId: store.state.Info.id,
        is_true: 2,
        result: "抱歉，原帖没有问题"
    }
    DealMsgReportAync(params)
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

const back = () => {

}

</script>

<style scoped>
.checkComment-page {
    background-image: url('@/assets/check_comment.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 107vh;
    width: 100vw;
}

.comment-list {
    background-color: #ace5d8;
    width: 450px;
    height: 100px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 30px;
}

.pass_btn {
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
    width: 600px;
    height: 500px;
    box-shadow: 4px 4px 4px 2px gray;
    border-radius: 10px;
}

.icon {
    font-size: 25px;
    position: absolute;
    top: 7%;
    left: 3%
}

.icon:hover {
    color: grey;
}</style>