<template>
    <div>
        <div class="announcement-page">
            <!-- 顶部导航栏 -->
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
                        <span>发布</span>
                    </el-button>
                </el-form-item>
            </div>
            <!-- 展示公告板，添加内容 -->
            <div class="announ-announcement-form">
                <span class="announ-announcement-title-word">标题</span>
                <div class="announ-announcement-title">
                </div>
                <span class="announ-announcement-content-word">内容</span>
                <div class="announ-announcement-content">
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js";



const formData = reactive({
    content: '',
});


const doReturnHome = () => {
    //这里进行返回管理员主页（个人信息页）操作
    router.push({ name: 'homeAdmin' });
};


const ReturnAnnoun = () => {
    //这里进行返回公告栏操作
    router.push({ name: 'announcement' })
};


const doLogOut = () => {
    //这里进行登出操作
    router.push({ name: 'login' })
};


</script>

<style scoped>
.announcement-page {
    background-image: url('@/assets/addNewAnnouncement.png');
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



/* 顶部导航按钮1-首页 */
.announ-corner-button1 {
    position: absolute;
    left: 680px;
    width: 70px;
    /* color: green; */
    background-color: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
}


/* 顶部导航按钮2-退出登录 */
.announ-corner-button2 {
    position: absolute;
    left: 730px;
    /* height: 32px; */
    width: 70px;
    /* color: green; */
    background-color: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
}

.announ-button {
    position: sticky;
    top: 2100px;
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
    left: 40px;
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
    left: 40px;
}
</style>