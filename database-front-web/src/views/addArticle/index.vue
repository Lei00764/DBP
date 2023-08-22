<!--该界面尚未完全完成-->
<!--标签添加功能尚未完善（计划做成让用户选择/输入后添加到标签栏）-->
<!--发布新文章还需要添加图片，目前尚未添加-->


<template>
    <div>
        <div class="addArticle">
            <!-- 顶部导航栏 -->
            <div class="addArticle-head">
                <el-form :inline=true>
                    <el-icon class="homepageIcon" @click="doHome">
                        <HomeFilled />
                    </el-icon>

                    <el-icon class="userpageIcon" @click="doUser">
                        <UserFilled />
                    </el-icon>

                    <el-form-item>
                        <el-button type="primary" class="logoff-button" @click="doLogoff">
                            <span>退出登录</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <!-- 按钮 -->
            <div class="addArticle-button">
                <!-- 返回按钮 -->
                <el-form-item>
                    <el-button type="primary" class="back-button" @click="doReturn">
                        <span class="iconfont icon-Back"></span>
                        <span>返回</span>
                    </el-button>
                </el-form-item>
                <!-- 发布按钮 -->
                <el-form-item>
                    <el-button type="primary" class="publish-button" @click="Publish">
                        <span class="iconfont icon-publish"></span>
                        <span>发布</span>
                    </el-button>
                </el-form-item>
            </div>
            <!-- 展示公告板，添加内容 -->
            <div class="editArticle-form">
                 <!-- <span class="editArticle-title-word">标题</span>
                <div class="editArticle-title"></div>
                <span class="editArticle-tag-word">标签</span>
                <div class="editArticle-tag"></div>
                <span class="editArticle-content-word">内容</span>
                <div class="editArticle-content"></div> -->
                <span class="editArticle-title-word">标题</span>
                <input class="editArticle-title" type="text" >
                <span class="editArticle-tag-word">标签</span>
                <input class="editArticle-tag" type="text" >
                <span class="editArticle-content-word">内容</span>
                <textarea class="editArticle-content" ></textarea>
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
    //返回主页
    router.push({ name: 'homeUser' });
}

const doUser = () => {
    //返回用户主页
    router.push({ path: 'userHomePage' });
};


const doReturn = () => {
    //返回论坛界面
    router.push({ path: 'forum/:tag' })
};


const doLogoff = () => {
    //退出登录
    router.push({ name: 'login' })
};


</script>

<style scoped>
.addArticle {
    background-image: url('@/assets/addArticle_bkg.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 100vh;
    width: 100vw;
}

.homepageIcon {
    /* 返回主页图标相关设置 */
    position: absolute;
    left: 1400px;
    top: 77px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.homepageIcon:hover {
    opacity: 0.8;
}

.userpageIcon {
    /* 返回用户主页图标相关设置 */
    position: absolute;
    left: 1450px;
    top: 77px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.userpageIcon:hover {
    opacity: 0.8;
}

.logoff-button {
    /* 退出登录按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1510px;
    height: 33px;
    width: 80px;
    background: #08664B;
    border-color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.logoff-button:hover {
    opacity: 0.8;
}

.addArticle-button {
    position: sticky;
    top: 2100px;
    left: 90%;
    width: 30%;
}

/* 返回按钮 */
.back-button {
    position: sticky;
    top: 70px;
    left: 91%;
    height: 69%;
    background: #08664B;
}

/* 发布按钮 */
.publish-button {
    position: sticky;
    top: 70px;
    left: 91%;
    height: 69%;
    background: #08664B;
}


/* 发布文章界面 */
.editArticle-form {
    position: absolute;
    top: 150px;
    left: 300px;
    height: 750px;
    width: 1150px;
    border-radius: 12px;
    background-color: #ccd1cf;
}

.editArticle-title {
    position: absolute;
    top: 60px;
    left: 53px;
    height: 55px;
    width: 1040px;
    border-radius: 12px;
    background-color: white;
}

.editArticle-title-word {
    position: absolute;
    top: 20px;
    left: 545px;
}

.editArticle-tag {
    position: absolute;
    top: 167px;
    left: 53px;
    height: 55px;
    width: 1040px;
    border-radius: 12px;
    background-color: white;
}

.editArticle-tag-word {
    position: absolute;
    top: 130px;
    left: 545px;
}

.editArticle-content {
    position: absolute;
    top: 272px;
    left: 53px;
    height: 430px;
    width: 1040px;
    border-radius: 12px;
    background-color: white;
}

.editArticle-content-word {
    position: absolute;
    top: 235px;
    left: 545px;
}
</style>