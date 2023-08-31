<template>
    <div>
        <div class="header-content">
            <!-- 左侧 logo -->
            <router-link to="/layout" class="logo" @click="ToHome">
                <span>
                    Foodieland
                </span>
            </router-link>

            <!-- 中间 搜索栏 -->
            <!--搜素事件，回车触发   -->
            <div class="search-panel">
                <el-input placeholder= "Search Key Words" class="custom-input" v-model="formData.keyword"
                    @keyup.enter="enterDown">
                    <!-- prefix 前置插入槽 -->
                    <template #prefix>
                        <font-awesome-icon :icon="['fas', 'magnifying-glass']" />
                    </template>
                </el-input>
            </div>

            <!-- 右侧 个人信息 -->
            <div class="user-info-panel">
                <el-button class="icon-button" @click="ToHome">
                    <font-awesome-icon :icon="['fas', 'house']" />
                    <span class="button-text">首页</span>
                </el-button>
                <el-button class="icon-button" @click="ToMy">
                    <font-awesome-icon :icon="['fas', 'circle-user']" />
                    <span class="button-text">我的主页</span>
                </el-button>
                <el-button class="icon-button" @click="ToAnnouncement">
                    <font-awesome-icon :icon="['fas', 'paperclip']" />
                    <span class="button-text">公告栏</span>
                </el-button>
                <el-button class="icon-button" @click="ToLogOut">
                    <font-awesome-icon :icon="['fas', 'right-to-bracket']" />
                    <span class="button-text">退出登录</span>
                </el-button>
                <el-button class="icon-button" @click="ToCheckMessage">
                    <font-awesome-icon :icon="['fas', 'comments']" />
                    <span class="button-text">消息</span>
                </el-button>
            </div>
            <userAvatar :userId=store.state.Info.id :width=50 :addLink="false"></userAvatar>
            <avatarUploader></avatarUploader>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive, onMounted, onUnmounted, nextTick } from 'vue';
import router from "@/router/index.js"
import { useStore } from 'vuex' // 引入store
// 1111
import { searchArticles } from "@/api/article.js"
import { getCurrentInstance } from "vue";

const instance = getCurrentInstance();

const store = useStore(); // 使用store必须加上

const formData = reactive({  // 用 reactive，而不用 ref
    keyword: '',
});

onMounted(() => {
    // 绑定监听事件
    nextTick(() => {
        window.addEventListener("keydown", enterDown);
    });
});

const enterDown = async (e) => {
    if (e.keyCode == 13 || e.keyCode == 100) {
        console.log("进入搜索页面");
        console.log("搜索关键词：", formData.keyword);
        e.preventDefault(); // 阻止默认提交动作
        // 将 keyword 作为查询参数传递给 /search 路由
        // 导航到一个新的路由，同时还传递了一个查询参数 keyword
        //router.push({ path: '/search', query: { keyword: formData.keyword } });
    }
    
    // 123
    /*proxy.globalInfo.search_keyword = formData.keyword;
    let params = {
        keyword: formData.keyword
    };
    let result = await searchArticles(params);
    console.log(result);
    //proxy.globalInfo.search_result.value = result.data;
    console.log("进入搜索页面");
    //console.log(proxy.globalInfo.search_result.value);*/
    instance.emit('articlesupdated', formData.keyword);
    
    // 销毁事件
    window.removeEventListener("keydown", enterDown, false);
}


const ToHome = () => {
    router.push(`/homeUser`);
}

const ToMy = () => {
    if (store.state.type == 1) { //管理员身份
        router.push(`/userHomePage`);
    }
    else if (store.state.type == 0) {  //用户身份
        router.push(`/homeAdmin`);
    }
}

const ToAnnouncement = () => {
    if (store.state.type == 1) { //用户身份
        router.push(`/announcementUser`);
    }
    else if (store.state.type == 0) {  //管理员身份
        router.push(`/announcementAdmin`);
    }
}

const ToLogOut = () => {
    router.push(`/login`);
}

const ToCheckMessage = () => {
    // 跳转到消息界面（管理员可以给用户发送消息）
}
</script>

<style>
.header-content {
    margin: 0 auto;
    align-items: center;
    /* 通过将高度设置成外层容器一致，达到居中效果 */
    height: 10vh;
    width: 80vw;
    display: flex;
}

.logo {
    /* 取消下划线样式 */
    text-decoration: none;
    font-size: 24px;
    color: rgb(96, 98, 102);
}

.search-panel {
    flex: 1;
    margin-left: 10vw;
    margin-right: 10vw;
}

.custom-input>.el-input__wrapper {
    height: 40px;
    border-radius: 10px;
    border: 1px solid rgb(96, 98, 102);
    box-shadow: none;
}

.user-info-panel {
    display: flex;
}

.user-info-panel .el-button {
    border: none;
    /* 去掉按钮的背景颜色 */
    background-color: transparent;
    padding: 0;
    /* margin: 0; */
    margin-right: 1vw;
}

.user-info-panel .el-button:hover {
    background-color: transparent;
}

.button-text {
    margin-left: 5px;
}
</style>
