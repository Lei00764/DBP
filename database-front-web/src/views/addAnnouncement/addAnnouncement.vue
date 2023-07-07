<template>
    <div>
        <div class="announcement-page">
            <!-- 顶部导航栏 -->
            <div class="announ-head-form" :class="navBarFixed == true ? 'navBarWrap' :''">
                <el-form :inline=true>
                 <!-- 表单 -->
                    <el-form-item>
                    <!-- 返回管理员主页 -->
                        <el-button type="primary" class="announ-corner-button1" @click="doReturnHome">
                            <span>首页</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                    <!-- 返回管理员个人主页 -->
                        <el-button type="primary" class="announ-corner-button2" @click="doLogOut">
                            <span>个人主页</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <!-- 返回按钮 -->
            <div class="announ-back" :class="navBarFixed == true ? 'navBarWrap' :''">
                <el-form-item>
                    <el-button type="primary" class="announ-back-button" @click="ReturnAnnoun">
                        <span class="iconfont icon-Back"></span>
                        <span>返回</span>
                    </el-button>
                </el-form-item>
            </div>
            <!-- 展示公告 -->
            <div class="announ-announcement-form">
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
    background-image: url('@/assets/announcement-detail-bkg.png');
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


/* 顶部导航按钮2-个人主页 */
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

.announ-back{
    position: sticky;
    top: 90%;
    left: 90%;
    width: 30%;
}

/* 返回按钮 */
.announ-back-button{
    position: sticky;
    top: 2000px;
    left: 85%;
    height: 69%;
    width: 30%;
    background: #08664B;
}


/* 该版式为公告版式 */
.announ-announcement-form{
    position: absolute;
    top: 230px;
    left: 50px;
    height: 400px; 
    width: 1200px;
    border-radius: 12px;
    background-color: #ccd1cf;
}




</style>