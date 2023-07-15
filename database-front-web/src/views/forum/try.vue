<!-- 本文件没用，可以删除 -->
<template>
    <div>
        <div class="forum-page">
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
            <div class="button">
                <el-form :inline=true>

                    <el-icon class="homepageIcon" @click="doHome">
                        <HomeFilled />
                    </el-icon>
                    <el-icon class="userpageIcon" @click="doUser">
                        <UserFilled />
                    </el-icon>

                    <el-form-item>
                        <el-button type="primary" class="addArticle-button" @click="doAddArticle">
                            <span>发布文章</span>
                        </el-button>
                    </el-form-item>

                    <el-form-item>
                        <el-button type="primary" class="forum-logoff-button" @click="doLogoff">
                            <span>退出登录</span>
                        </el-button>
                    </el-form-item>

                    <!-- 导航栏 -->
                    <navTop></navTop>
                    <!-- 文章显示区 -->
                    <!-- <showArticle></showArticle> -->


                </el-form>

            </div>
            <div>
                <!-- <imageArticle></imageArticle> -->
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"
import navTop from "@/components/navTop.vue"
import showArticle from "@/components/showArticle.vue"
import imageArticle from "@/components/imageArticle.vue"

const formData = reactive({
    keywords: '',
});

const doSearch = () => { };

const doHome = () => {
    //返回主页
    router.push({ name: 'homeUser' })
};

const doUser = () => {
    //返回用户主页
    router.push({ path: '../userHomePage' });
};


const doAddArticle = () => {
    //发布新文章
    router.push({ name: 'addArticle' })
};

const doLogoff = () => {
    //退出登录
    router.push({ name: 'login' })
};

</script>

<style scoped>
/*背景图相关设置 */
.forum-page {
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
    left: 33%;
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
    left: 1230px;
    top: 77px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.homepageIcon:hover {
    opacity: 0.8;
}

.userpageIcon {
    position: absolute;
    left: 1290px;
    top: 77px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.userpageIcon:hover {
    opacity: 0.8;
}

.addArticle-button {
    /*发布文章按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1360px;
    height: 33px;
    width: 80px;
    background: #08664B;
    border-color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.addArticle-button:hover {
    opacity: 0.8;
}

.forum-logoff-button {
    /*退出登录按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1440px;
    height: 33px;
    width: 80px;
    background: #08664B;
    border-color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.forum-logoff-button:hover {
    opacity: 0.8;
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