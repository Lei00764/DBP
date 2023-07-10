<!-- 此处为举报帖子的举报界面 -->
<template>
    <div>
        <div class="announcement-page">
            <!-- 顶部导航栏 -->
<<<<<<< HEAD
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
=======
            <div class="announ-head-form" :class="navBarFixed == true ? 'navBarWrap' : ''">
                <el-form :inline=true>
                    <!-- 表单 -->
                    <el-form-item>
                        <!-- 返回管理员主页 -->
                        <el-button type="primary" class="announ-corner-button1" @click="doReturnHome">
                            <span>首页</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <!-- 退出登录 -->
                        <el-button type="primary" class="announ-corner-button2" @click="doLogOut">
>>>>>>> main
                            <span>退出登录</span>
                        </el-button>
                    </el-form-item>

                </el-form>
            </div>
            <!-- 按钮 -->
            <div class="announ-button" :class="navBarFixed == true ? 'navBarWrap' : ''">
                <!-- 返回按钮 -->
                <el-form-item>
                    <el-button type="primary" class="announ-back-button" @click="ReturnAnnoun">
                        <span class="iconfont icon-Back"></span>
                        <span>返回</span>
                    </el-button>
                </el-form-item>
                <!-- 发布按钮 -->
                <el-form-item>
                    <el-button type="primary" class="announ-publish-button" @click="Publish">
                        <span class="iconfont icon-publish"></span>
                        <span>提交</span>
                    </el-button>
                </el-form-item>
            </div>
            <!-- 展示举报页 -->
            <div class="announ-announcement-form">
                <span class="announ-announcement-title-word">举报原因</span>
                <div class="announ-announcement-title">
                </div>
                <span class="announ-announcement-content-word">详细说明</span>
                <div class="announ-announcement-content">
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"



const formData = reactive({
    keywords: '',
});


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


const ReturnAnnoun = () => {
    //这里进行返回帖子详情操作
    //这里还不确定，因为每个帖子举报的回退要退回对应帖子
    router.push({ name: 'announcementDetail' })
};





</script>

<style scoped>
.announcement-page {
    background-image: url('@/assets/report.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 98vh;
    width: 99vw;
}

.announ-head-form {
    position: sticky;
    top: 5%;
    left: 35%;
    /*transform: translate(-50%, -50%);*/
    height: 10%;
    width: 50%;
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


/* 顶部导航按钮-退出登录 */
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

.announ-button {
    position: sticky;
    top: 800px;
    left: 90%;
    width: 30%;
}

/* 返回按钮 */
.announ-back-button {
    position: sticky;
    top: 70px;
    left: 91%;
    height: 69%;
    background: #08664B;
}

/* 发布按钮 */
.announ-publish-button {
    position: sticky;
    top: 70px;
    left: 91%;
    height: 69%;
    background: #08664B;
}


/* 该版式为公告版式 */
.announ-announcement-form {
    position: absolute;
    top: 230px;
    left: 50px;
    height: 400px;
    width: 1200px;
    border-radius: 12px;
    background-color: #ccd1cf;
}

.announ-announcement-title {
    position: absolute;
    top: 28px;
    left: 80px;
    height: 55px;
    width: 1040px;
    border-radius: 12px;
    background-color: white;
}

.announ-announcement-title-word {
    position: absolute;
    top: 40px;
    left: 10px;
}

.announ-announcement-content {
    position: absolute;
    top: 120px;
    left: 80px;
    height: 245px;
    width: 1040px;
    border-radius: 12px;
    background-color: white;
}

.announ-announcement-content-word {
    position: absolute;
    top: 130px;
    left: 10px;
}
</style>