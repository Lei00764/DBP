<template>
    <el-form style="position: absolute;top:13%;left:28%">
        <el-form-item v-for="(item, index) in list" :key="index">
            <el-card :class='index==currentCard?"article-list-active":"article-list-inactive"' @click.native="goArticle(index)">
                <!-- 下面组件的v-if都不能省略，否则刷新页面会出错 -->
                <b v-if="Info[index]"
                style="position: absolute;top:10%;left:5%;font-size: 18px;color:rgb(61, 61, 61)">
                        {{ Info[index].title }}
                </b>
                <b style="position: absolute;top:50%;left:5%;font-size: 14px;color:rgb(120, 120, 120)">
                    帖子ID：{{ item.postId }}
                </b>
                <el-button class="pass_btn" style="position:absolute;top:25%;left:78%;"
                    @click="pass(item.reportId)">
                        <el-icon>
                            <Check />
                        </el-icon>
                </el-button>
                <el-button class="close_btn" style="position: absolute;top:25%;left:85%;"
                    @click="decline(item.reportId)">
                        <el-icon>
                            <Close />
                        </el-icon>
                </el-button>
            </el-card>
        </el-form-item>
    </el-form>

    <el-card class="card" v-show="card_show" v-if="Info[currentCard]">
        <b style="position:absolute;top:8%;left:5%;">举报原因</b>
        <div class="blank" style="top:6%;left:20%;">
            <div style="position:absolute;top:28%;left:5%">{{ list[currentCard].reason }} </div>
        </div>

        <b style="position:absolute;top:20%;left:5%;">帖子标题</b>
        <div class="blank" style="top:18%;left:20%;">
            <div style="position:absolute;top:28%;left:5%">{{ Info[currentCard].title }} </div>
        </div>
        
        <b style="position:absolute;top:32%;left:5%;">帖子内容</b>
        <div class="blank" style="top:30%;left:20%;height:400px;">
            <div style="position:absolute;top:4%;left:4%">{{ Info[currentCard].content }} </div>
        </div>

                <el-button class="pass_btn" style="position: absolute;bottom:5%;left:42%;"
                    @click="pass(list[currentCard].reportId)">
                    <el-icon>
                        <Check />
                    </el-icon>
                </el-button>
                <el-button class="close_btn" style="position: absolute;bottom:5%;left:52%;"
                    @click="decline(list[currentCard].reportId)">
                    <el-icon>
                        <Close />
                    </el-icon>
                </el-button>
            </el-card>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"
import { ReportPostToDeal, DealReportAync } from '@/api/report';  // 引入 api 请求函数
import { GetArticleDetailsAsync } from '@/api/article';
import { useStore } from 'vuex'//引入store
const store = useStore();//使用store必须加上

const card_show = ref(false);//用以点击进入申请信息的详情界面
const currentCard = ref(-1);//用来记录当前显示的资料卡片（index）
const list = ref([]); // 定义并初始化 list 变量
const Info = reactive([]); // 定义并初始化 Info 变量

const GetList = () => {
    //将获取列表信息的接口封装在函数中
    ReportPostToDeal()
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
                article_id: list.value[index].postId,
            }
            GetArticleDetailsAsync(params)//获取对应举报的帖子信息
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

const goArticle = (index) => {
    //进入文章详情页面（卡片）
    if (index == currentCard.value) {
        card_show.value = !card_show.value;
        currentCard.value = -1;
    }
    else {
        card_show.value = true;
        currentCard.value = index;
    }
}

const pass = (reportId) => {
    //审核通过
    let params = {
        report_id: reportId,
        adminId: store.state.Info.id,
        is_true: 1,
        result: "情况属实，已做处理"
    }
    DealReportAync(params)
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
    DealReportAync(params)
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
.checkArticle-page {
    background-image: url('@/assets/check_article.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 107vh;
    width: 100vw;
}

.article-list-inactive {
    background-color:#E7F9FD;
    width: 500px;
    height: 70px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 10px;
}

.article-list-active {
    background-color:#CCFFCA;
    width: 500px;
    height: 70px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 10px;
}

.pass_btn {
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
.blank {
    position: absolute;
    font-size: 16px;
    background-color:rgb(255, 255, 255);
    width: 350px;
    height: 50px;
    border-radius: 10px;
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

.icon {
    font-size: 25px;
    position: absolute;
    top: 7%;
    left: 3%
}

.icon:hover {
    color: grey;
}</style>