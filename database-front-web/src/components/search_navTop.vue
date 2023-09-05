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
            <div class="search-container">
                <div class="search-panel">
                    <el-input placeholder="Search Key Words" class="custom-input" v-model="formData.keyword"
                        @input="handleInput"  @keydown.enter="enterDown" @click="startsearch">
                            <!-- prefix 前置插入槽 -->
                        <template #prefix>
                            <font-awesome-icon :icon="['fas', 'magnifying-glass']" />
                        </template>
                    </el-input>
                    <ul class="search-history" v-show="showHistory">
                        <li>
                            <h3 class="search-history-title">搜索历史</h3>
                        </li>
                        <li v-for="history in searchHistory" :key="history" @click="loadHistory(history)" class="history-row">
                            <span class="history-item">{{ history }}</span>
                        </li>
                        <li>
                            <button @click="clearSearchHistory">清空搜索历史</button>
                        </li>
                    </ul>
                </div>
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
                <div class="container">
                    <div class="dropdown">
                            <!-- 标题 -->
                        <el-button class="dropdown-title" @click="ToCheckMessage">
                            <font-awesome-icon :icon="['fas', 'comments']" />
                            <span class="button-text">消息</span>
                        </el-button>
                            <!-- xiaoxi -->
                        <div class="dropdown-content">
                            <div class="dropdown-menu">
                                <noticeitem v-for="item in dis_announcementListInfo" :key="item.AnnouncementId" :data="item">
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
// 1111
import noticeitem from "@/components/noticeitem.vue"
import { loadAnnouncement } from "@/api/announcement.js"
import { loadNotice } from "@/api/notice.js"
const announcementListInfo = ref([]);

const dis_announcementListInfo = ref([]);
const fetchData = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            //p_board_id: pBoardId.value,
            //page_num: 1
            user_id: store.state.Info.id,
        };
        result = await loadNotice(params);
    }
    else {
        const params = {
            keyword: stringValue
        };
        result = await forum_searchArticle(params);
    }

    if (!result)
        return;
    console.log(result.data);
    announcementListInfo.value = result.data;
    dis_announcementListInfo.value = announcementListInfo.value.slice(0, 8);

};

// 在组件挂载时获取初始文章数据
onMounted(() => {
    fetchData();
});
//111

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
//1111
const searchHistory = ref([]);
const showHistory = ref(false);
const MAX_HISTORY_LENGTH = 5;

// 保存搜索历史到本地存储
const saveSearchHistory = () => {
  localStorage.setItem('searchHistory', JSON.stringify(searchHistory.value.slice(0, MAX_HISTORY_LENGTH)));
};

// 加载搜索历史
const loadSearchHistory = () => {
  const savedSearchHistory = localStorage.getItem('searchHistory');
  searchHistory.value = savedSearchHistory ? JSON.parse(savedSearchHistory) : [];
};

// 清空搜索历史
const clearSearchHistory = () => {
  searchHistory.value = [];
  localStorage.removeItem('searchHistory');
  showHistory.value = false;
};

const enterDown = (e) => {
  if (e.keyCode === 13 || e.keyCode === 100) {
    console.log("进入搜索页面");
    console.log("搜索关键词：", formData.keyword);
    e.preventDefault();
    
    // 触发搜索历史
    instance.emit('articlesupdated', formData.keyword);
    console.log("触发了搜索历史");
    const keyword = formData.keyword;
    const historyIndex = searchHistory.value.indexOf(keyword);
    if (historyIndex !== -1) {
      searchHistory.value.splice(historyIndex, 1);
    }
    searchHistory.value.unshift(keyword);
    
    // 保存搜索历史到本地存储
    saveSearchHistory();
    showHistory.value = false;
  }
  
  window.removeEventListener("keydown", enterDown, false);
};

const handleInput = () => {
  console.log("触发了搜索框");
  loadSearchHistory();
  if (formData.keyword === "") {
    showHistory.value = false;
  } else {
    showHistory.value = true;
    // 在这里根据输入内容进行搜索历史的自定义逻辑
  }
};

const startsearch = () => {
  console.log("触发了搜索框");
  loadSearchHistory();
  if (formData.keyword === "") {
    showHistory.value = true;
  } else {
    showHistory.value = false;
    // 在这里根据输入内容进行搜索历史的自定义逻辑
  }
};

const loadHistory = (history) => {
  formData.keyword = history;
  showHistory.value = false;
  instance.emit('articlesupdated', formData.keyword);
};

// 页面初始化时加载搜索历史
loadSearchHistory();
// 22222
const ToHome = () => {
    router.push(`/homeUser`);
}

const handle = () => {
    console.log("aaaaaaa");
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

.search-container {
  position: relative;
}

.search-history {
  position: absolute;
  top: 40px;
  left: 0;
  width: 100%;
  background-color: #fff;
  border: 1px solid #ccc;
  padding: 8px;
  list-style-type: none;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  z-index: 9999; /* 设置较高的 z-index 值以确保遮盖其他组件 */
}

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

//.dropdown {
//    margin: 0 20px;
//}

.dropdown .dropdown-title {
    display: inline-block;
    position: relative;
    //height: 30px;
    //line-height: 30px;
    //font-size: 5px;
    //background-color: #fff;
    //color: #000;
    padding: 0 24px;
    border-radius: 4px;
    cursor: pointer;
}

.dropdown .dropdown-content {
    position: absolute;
    visibility: hidden;
    opacity: 0;
    transition: all 0.6s ease-in-out;
}

.dropdown .dropdown-content .dropdown-menu {
    margin-top: 6px;
    padding: 10px 8px 15px;
    background-color: rgba(255, 255, 255, 0.011);
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

.search-history-title {
  font-size: 14px; /* 调整字体大小为较小值，可以根据需要调整 */
}
.history-item:hover {
  background-color: gray; /* 将鼠标指向时的背景色设置为灰色，你可以根据需要自行调整颜色值 */
}

.history-row:hover {
  background-color: gray; /* 将整行的背景色设置为灰色，你可以根据需要自行调整颜色值 */
}
</style>
