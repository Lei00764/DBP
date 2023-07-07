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
                        <el-button type="primary" class="forum-login-button" @click="doLogoff">
                            <span>退出登录</span>
                        </el-button>
                    </el-form-item>

                    <!-- 导航栏 -->
                    <navTop></navTop>
                    <!-- 文章显示区 -->
                    <showArticle></showArticle>

                    <!-- 导航栏设置，暂时先这样，后续会改成element plus -->
                    <div class="elui">
                        <el-pagination background page-size="8" layout="prev, pager, next, jumper" :total="1000">
                        </el-pagination>
                    </div>
                </el-form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"
import navTop from "@/components/navTop.vue"
import showArticle from "@/components/showArticle.vue"

const formData = reactive({
    keywords: '',
});

const doSearch = () => { };

const doHome = () => {
    router.push({ name: 'homeUser' })
};

const doUser = () => {
    router.push({ name: 'userHomePage' })
};

const doLogoff = () => {
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
    left: 76%;
    top: 77px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.homepageIcon:hover {
    opacity: 0.8;
}

.userpageIcon {
    position: absolute;
    left: 80%;
    top: 77px;
    font-size: 33px;
    color: rgb(55, 192, 255)
}

.userpageIcon:hover {
    opacity: 0.8;
}

.forum-login-button {
    /*退出登录按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1400px;
    height: 33px;
    width: 80px;
    background: #08664B;
    border-color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.forum-login-button:hover {
    opacity: 0.8;
}
</style>