<template>
    <div>
        <div class="forum-page">
            <!-- 表单 -->
            <div class="forum-search">
                <el-form :inline=true>
                    <!-- 搜索框及搜索按钮控件 -->
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
            <div class = "button">
                <!--按钮控件-->
                <el-form :inline=true>
                    <!--返回主界面图标控件-->
                    <el-icon class="homepageIcon" @click="doHome">
                        <HomeFilled />
                    </el-icon>
                    <!--返回用户主页图标控件-->
                    <el-icon class="userpageIcon" @click="doUser">
                        <UserFilled />
                    </el-icon>
                    <!--登录注册按钮控件-->
                    <el-form-item>
                        <el-button type="primary" class="forum-login-button" @click="doLogin">
                            <span>登录</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" class="forum-register-button" @click="doRegister">
                            <span>注册</span>
                        </el-button>
                    </el-form-item>
                    <!--四种餐品专栏按钮控件-->
                    <el-form-item>
                        <el-button type="primary" class="forum-button1" @click="doChineseFood">
                            <span>中餐</span>
                        </el-button>
                    </el-form-item>

                    <el-form-item>
                        <el-button type="primary" class="forum-button2" @click="doWesternFood">
                            <span>西餐</span>
                        </el-button>
                    </el-form-item>

                    <el-form-item>
                        <el-button type="primary" class="forum-button3" @click="doDessert">
                            <span>甜点</span>
                        </el-button>
                    </el-form-item>

                    <el-form-item>
                        <el-button type="primary" class="forum-button4" @click="doOthers">
                            <span>其他</span>
                        </el-button>
                    </el-form-item>
                    <!--文章栏按钮控件-->
                    <el-form-item>
                        <el-button type="primary" class="forum-article1" @click="doArticle"></el-button>
                    </el-form-item>

                    <el-form-item>
                        <el-button type="primary" class="forum-article2" @click="doArticle"></el-button>
                    </el-form-item>

                    <el-form-item>
                        <el-button type="primary" class="forum-article3" @click="doArticle"></el-button>
                    </el-form-item>

                    <el-form-item>
                        <el-button type="primary" class="forum-article4" @click="doArticle"></el-button>
                    </el-form-item>
                </el-form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';

const doSearch = () => { };

const doHome = () => { };

const doUser = () => { };

const doLogin = () => { };

const doRegister = () => { };

const doChineseFood = () => { };

const doWesternFood = () => { };

const doDessert = () => { };

const doOthers = () => { };

const doArticle = () => { };

const formData = reactive({
    keywords: '',
});
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
        /*返回主页面图标相关设置 */
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
        /*返回用户主页图标相关设置 */
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
    /*登录按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1400px;
    height: 33px;
    width: 70px;
    background: #08664B;
    border-color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.forum-login-button:hover {
    opacity: 0.8;
}

.forum-register-button {
    /*注册按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1470px;
    height: 33px;
    width: 70px;
    background: #08664B;
    border-color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.forum-register-button:hover {
    opacity: 0.8;
}

.forum-button1 {
    /*菜品种类第一个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 100px;
    height: 50px;
    width: 250px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button1:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}

.forum-button2 {
    /*菜品种类第二个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 420px;
    height: 50px;
    width: 250px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button2:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}
.forum-button3 {
    /*菜品种类第三个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 740px;
    height: 50px;
    width: 250px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button3:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}

.forum-button4 {
    /*菜品种类第四个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 1060px;
    height: 50px;
    width: 250px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button4:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}

.forum-article1 {
    /*文章栏文章框相关设置 */
    position: absolute;
    top: 360px;
    left: -60px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}
.forum-article1:hover {
    opacity: 0.7;
}

.forum-article2 {
    /*文章栏文章框相关设置 */
    position: absolute;
    top: 360px;
    left: 635px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}
.forum-article2:hover {
    opacity: 0.7;
}

.forum-article3 {
    /*文章栏文章框相关设置 */
    position: absolute;
    top: 600px;
    left: -125px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}
.forum-article3:hover {
    opacity: 0.7;
}
.forum-article4 {
    /*文章栏文章框相关设置 */
    position: absolute;
    top: 600px;
    left: 573px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}
.forum-article4:hover {
    opacity: 0.7;
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