<!-- 文章缩略图 -->
<template>
    <div>
        <div class="header">
            <navTop></navTop>
        </div>
        <div id="announcement-panel">
            <div class="announcement-list">
                <announcementListItemUser v-for="item in dis_announcementListInfo" :key="item.ID" :data="item">
                </announcementListItemUser>
            </div>
            <div class="pagination">
                <el-pagination :current-page="currentPage" :page-size="pageSize" :total="totalCount" layout="->,prev, pager, next,jumper"
                @current-change="handlePageChange"></el-pagination>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
// import { useRouter } from 'vue-router';

import navTop from "@/components/navTop.vue"
import announcementListItemUser from "@/components/announcementListItemUser.vue"
import { loadAnnouncement } from "@/api/announcement.js"
//import { forum_searchArticle } from "@/api/article.js"
import router from "@/router/index.js"
import { useStore } from 'vuex' // 引入store

// 子组件接收父组件的传值 和 子路由接收父路由的传值不同
// 前者：defineProps()
// 后者：router.currentRoute.value.params.pBoardId

const pBoardId = ref(router.currentRoute.value.params.pBoardId);

// 存储获取的文章数据
const announcementListInfo = ref([]);

const dis_announcementListInfo = ref([]);
const currentPage = ref(1);   // 当前页码
const pageSize = 8;          // 每页元素数量
const totalpagenumber = ref(1);  //总页数
const totalCount = ref(0);

// 获取文章数据
const fetchData = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            p_board_id: pBoardId.value,
            page_num: 1
        };
        result = await loadAnnouncement(params);
    }
    else {
        const params = {
            keyword: stringValue
        };
        result = await forum_searchArticle(params);
    }

    if (!result)
        return;
    announcementListInfo.value = result.data;
    totalpagenumber.value = Math.ceil(announcementListInfo.value.length / pageSize);   //计算总页数
    totalCount.value = announcementListInfo.value.length;
    dis_announcementListInfo.value = announcementListInfo.value.slice(0, 8);
};

// 在组件挂载时获取初始文章数据
onMounted(() => {
    fetchData();
});

// 使用 watch 监听父级路由参数的变化
watch(() => router.currentRoute.value.params.pBoardId, (newValue) => {
    // console.log(newValue);
    pBoardId.value = newValue;
    fetchData();
});

//分页
const handlePageChange = (page) => {
  if (page >= 1 && page <= totalpagenumber.value) {
    currentPage.value = page;
    const start = (currentPage.value - 1) * pageSize;
    const end = start + pageSize;
    dis_announcementListInfo.value = announcementListInfo.value.slice(start, end);
  }
}
</script>

<style>
.header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

#announcement-panel {
  height: 128vh;
  width: 85vw;
  margin-left: auto;
  margin-right: auto;
  margin-top: 2vh;
  box-shadow: 0 2px 6px 0 #ddd;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
}

.announcement-list {
    width: 80vw;
    /* display: flex; */
    justify-content: center;
    align-items: center;
    margin: 0px auto;
}

.pagination {
    display: flex;
    justify-content: center;
    margin-left: auto;
    margin-right: auto;
    margin-bottom: 1.5vh;
    order: 1;
}
</style>

