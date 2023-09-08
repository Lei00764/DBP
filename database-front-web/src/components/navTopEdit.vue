<template>
    <div>
        <div class="header-content">
            <!-- 左侧 logo -->
            <router-link to="/layout" class="logo" @click="ToHome">
                <span>
                    Foodieland
                </span>
            </router-link>

           

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

                <div class="container">
                    <div class="dropdown">
                        <!-- 消息 -->
                        <el-button class="dropdown-title" @click="ToCheckMessage">
                            <font-awesome-icon :icon="['fas', 'comments']" />
                            <span class="button-text">消息</span>
                        </el-button>
                        <!-- xiaoxi -->
                        <div class="dropdown-content">
                            <div class="dropdown-menu">
                                <noticeitem v-for="item in dis_noticeListInfo" :key="item.AnnouncementId"
                                    :data="item">
                                </noticeitem>
                            </div>
                        </div>
                    </div>
                </div>

                <el-button class="icon-button" @click="ToLogOut">
                    <font-awesome-icon :icon="['fas', 'right-to-bracket']" />
                    <span class="button-text">退出登录</span>
                </el-button>

            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive, onMounted, onUnmounted, nextTick } from 'vue';
import router from "@/router/index.js"
import { useStore } from 'vuex' // 引入store
import tiptapEditor from "@/components/tiptapEditor.vue"

const store = useStore(); // 使用store必须加上

const formData = reactive({  // 用 reactive，而不用 ref
    keyword: '',
});
// 1111
import noticeitem from "@/components/noticeitem.vue"
import { loadAnnouncement } from "@/api/announcement.js"
import { loadNotice } from "@/api/notice.js"

const noticeListInfo = ref([]);

const dis_noticeListInfo = ref([]);

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

<style lang="scss" scoped>
.header-content {
    margin: 0 auto;
    align-items: center;
    /* 通过将高度设置成外层容器一致，达到居中效果 */
    height: 11vh;
    width: 100%;
    display: flex;
    position:fixed;
    top:0;
    left:0;
    z-index: 99;
    background-color: white;
    box-shadow: 0px 0px 4px 0px gray;
}

.logo {
    /* 取消下划线样式 */
    text-decoration: none;
    font-size: 24px;
    color: rgb(96, 98, 102);
    position:absolute;
    left:5%;
    top:40%
}


.user-info-panel {
    display: flex;
    position:absolute;
    left:65%;
    top:40%
}

.user-info-panel .el-button {
    border: none;
    background-color: transparent;
    padding: 0;
    margin-right: 1vw;
}

.user-info-panel .el-button:hover {
    background-color: transparent;
}

.button-text {
    margin-left: 5px;
}



.dropdown .dropdown-title {
    display: inline-block;
    position: relative;
    padding: 0 24px;
    border-radius: 4px;
    cursor: pointer;
}

.dropdown .dropdown-content {
    position: absolute;
    visibility: hidden;
    opacity: 0;
    transition: all 0.6s ease-in-out;
    z-index: 2;
}

.dropdown .dropdown-content .dropdown-menu {
    margin-top: 6px;
    padding: 10px 8px 15px;
    background-color: rgba(7, 1, 1, 0.011);
    color: black;
    border-radius: 4px;
}

.dropdown .dropdown-content .dropdown-menu .menuItem {
    width: 100%;
    white-space: nowrap;
    padding: 10px 16px;
    font-size: 16px;
    color: white;
    cursor: pointer;
    border-radius: 4px;
}

.dropdown .dropdown-content .dropdown-menu .menuItem:hover {
    background-color: #ccc;
}

.dropdown:hover .dropdown-content {
    visibility: visible;
    opacity: 1;
}
</style>

