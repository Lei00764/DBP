<template>
    <div>
        <div class="forum-article-detail-page">
            <!-- 表单 -->
            <div class="forum-search">
                <el-form :inline=true>
                    <!-- 搜索栏及按钮设置 -->
                    <el-form-item>
                        <el-input placeholder="请输入关键词" class="forum-searchbox" v-model="formData.keywords">
                            <template #prefix>
                                <el-icon>
                                    <Search />
                                </el-icon>
                            </template>
                        </el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" class="forum-searchbutton" @click="doSearch">
                            <span>搜索</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <div>
                <el-form :inline=true>
                    <el-icon class="homepageIcon" @click="doHome">
                        <HomeFilled />
                    </el-icon>
                    <el-icon class="userpageIcon" @click="doUser">
                        <UserFilled />
                    </el-icon>

                    <el-form-item>
                        <el-button type="primary" class="forum-logout-button" @click="doLogOut">
                            <span>退出登录</span>
                        </el-button>
                    </el-form-item>

                </el-form>
            </div>
            <!-- 展示帖子详情 -->
            <div class="announ-announcement-form">
                <!-- 计划做个弹窗举报按钮，还未实现 -->
                <el-icon  class="userReportIcon"  @click="Report">
                        <MoreFilled />
                </el-icon>
                <!-- 文章详情展示未完成 -->
                <div class="title">
                    Title:{{ props.title }}       
                </div>
                <div class="content">
                    Content:{{ props.content }}       
                </div>
                <div class="author">
                    Author:{{ props.author_name }}      
                </div>
                <div class="publish_time">
                    Publish_Time:{{ props.publish_time }}      
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive,toRefs,onMounted } from 'vue';
import { getArticleDetail } from '@/api/user';
import Message from "@/utils/Message.js"
import { useRouter } from 'vue-router'
const router = useRouter()
const props = defineProps({
    title: {
        type:String, // 可以设置传来值的类型
        default:""
    },
    content:{
        type:String,
        default:""
    },
    publish_time:{
        
    },
    author_name:{
        type:String,
        default:""
    },
})

const doSearch = () => { 
    //进行搜索操作
};

const doHome = () => { 
    //返回用户主页
    router.push({ name: 'homeUser' })
};

const doUser = () => {
    //返回用户个人信息页
    router.push({ name: 'userHomePage' })
 };

const doLogOut = () => { 
    //登出
    router.push({ name: 'login' })
};

const Report = () => {
    //举报页
    router.push({name:'reportArticle'})
};



//文章详情
const articleInfo =ref({});


const formData = reactive({
    articleId:"",
});
</script>

<style scoped>
/*背景图相关设置 */
.forum-article-detail-page {
    background-image: url('@/assets/forum_bkg.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 100vh;
    width: 100vw;
}

.forum-search {
    /*搜索输入框相关设置 */
    position: absolute;
    top: 8%;
    left: 27%;
    height: 10%;
    width: 50%;
}

.forum-searchbox {
    position: absolute;
    top: 0px;
    left: -100px;
    /* height: 32px; */
    width: 600px;
}

.forum-searchbutton {
    /*搜索按钮相关设置 */
    position: absolute;
    top: 0px;
    left: 490px;
    height: 32px;
    width: 70px;
    border-radius: 14px;
    background-color: white;
    border-color: black;
    color: black;
    box-shadow: 0px 4px 4px 0px gray;
}

.forum-searchbutton:hover {
    border-radius: 14px;
    background-color: #ebebeb;
    color: rgb(0, 0, 0);
    border-color: rgb(78, 78, 78);
    box-shadow: 0px 4px 4px 0px gray;
}

.homepageIcon {
    position: absolute;
    left: 73%;
    top: 70px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.homepageIcon:hover {
    opacity: 0.8;
}

.userpageIcon {
    position: absolute;
    left: 78%;
    top: 70px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.userpageIcon:hover {
    opacity: 0.8;
}

.forum-logout-button {
    /*登出按钮相关设置 */
    position: absolute;
    top: 59px;
    left: 1230px;
    height: 33px;
    width: 70px;
    background: #08664B;
    border-color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.userReportIcon:hover{
    opacity: 0.8;
}

.userReportIcon{
    position: absolute;
    top: 20px;
    left: 960px;
    
}

/* 该版式为帖子详情页版式 */
.announ-announcement-form{
    position: absolute;
    top: 200px;
    left: 180px;
    height: 1000px; 
    width: 1000px;
    border-radius: 12px;
    background-color: #ccd1cf;
}
/* 帖子标题 */
.title{
    position: absolute;
    top: 20px;
    left: 18px;
    height: 100px; 
    width: 1000px;
}
/* 帖子内容展示 */
.content{
    position: absolute;
    top: 150px;
    left: 18px;
    height: 900px; 
    width: 1000px;
}
/* 帖子作者 */
.author{
    position: absolute;
    top: 20px;
    left: 700px;
    height: 900px; 
    width: 1000px;
}
/* 帖子发布时间 */
.publish_time{
    position: absolute;
    top: 120px;
    left: 18px;
    height: 900px; 
    width: 1000px;
}



:deep(.el-input__wrapper) {
    /* 搜索输入框背景、圆角、字体颜色设置 */
    background: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
    color: rgb(235, 235, 235);
}

:deep(.el-input__inner) {
    color: rgb(235, 235, 235);
}

</style>