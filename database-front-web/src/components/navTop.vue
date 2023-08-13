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
                <el-input placeholder="Search Key Words" class="custom-input" v-model="formData.keywords"
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
                <el-button class="icon-button" @click="ToLogOut">
                    <font-awesome-icon :icon="['fas', 'right-to-bracket']" />
                    <span class="button-text">退出登录</span>
                </el-button>
                <el-button class="icon-button" @click="ToCheckMessage">
                    <font-awesome-icon :icon="['fas', 'comments']" />
                    <span class="button-text">消息</span>
                </el-button>
            </div>
            <!-- 现在用户头像是写死的 lx -->
            <avatar :userId="1" :width="50" :addLink="false"></avatar>
        </div>
    </div>
</template>

<script setup>
// 修改当前页面的 element-plus 主题色
import { changeTheme } from '@/utils/changeTheme';
import { ref, reactive, onMounted, onUnmounted, nextTick } from 'vue';
import router from "@/router/index.js"
import { searchPost } from '@/api/search';  // 引入 api 请求函数 searchPost
import { forum_searchArticle } from "@/api/article.js"
import { useStore } from 'vuex' // 引入store
//import { fetchData } from '@/views/forum/articleList'


changeTheme("#FFD700");  // 目前为红色，可以修改


const store = useStore(); // 使用store必须加上

const formData = reactive({
    keywords: '',
});

onMounted(() => {
    // 绑定监听事件
    nextTick(() => {
        window.addEventListener("keydown", enterDown);
    });
});
const enterDown = async(e) => {
    console.log("chufa");
    let result;
    if (e.keyCode == 13 || e.keyCode == 100) {
        e.preventDefault(); // 阻止默认提交动作
        //doSearch(); // 定义的登录方法
        const params = {
            keyword: formData.keywords
        };
        result = await forum_searchArticle(params);
        store.commit('setArticles', result.data);
        console.log(result.data);
        console.log(store.state.articles);
        //fetchData(formData.keywords);
    }
}
onUnmounted(() => {
    // 销毁事件
    window.removeEventListener("keydown", enterDown, false);
});

const doSearch = () => {
    let params = {
        keyword: formData.keywords
    }
    searchPost(params)
        .then(function (result) {
            console.log(result.data);
        })
        .catch(function (error) {
            console.log(error);
        });
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
const ToLogOut = () => {
    router.push(`/login`);
}
const ToCheckMessage = () => {
    //跳转到消息界面（管理员可以给用户发送消息）
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
