<!-- 文章缩略图 -->
<template>
    <div>
        <div class="addAnnouncement">
            <el-button @click="addAnnouncement">发布公告</el-button>
        </div>
        <div id="header">
            <navTop></navTop>
        </div>
        <div id="announcement-panel">
            
            <div class="announcement-list">
                <announcementListItemAdmin v-for="item in dis_announcementListInfo" :key="item.AnnouncementId" :data="item">
                </announcementListItemAdmin>
            </div>
            <div class="pagination">
                <el-pagination :current-page="currentPage" :page-size="pageSize" :total="totalCount" layout="->,prev, pager, next,jumper"
                @current-change="handlePageChange"></el-pagination>
            </div>
        </div>
        <!-- START 发布公告弹窗 -->
        <el-dialog v-model="dialogVisible" title="发布一条新公告" width="50%" :before-close="handleClose">
            <el-form @submit.native.prevent="submitAnnouncement">
                <el-form-item label="标题：">
                    <el-input type="textarea" v-model="form.title" />
                </el-form-item>
                <el-form-item label="内容：">
                    <el-input type="textarea" v-model="form.announcementContent" />
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="dialogVisible = false">Cancel</el-button>
                    <el-button type="primary" @click="submitAnnouncement">Submit</el-button>
                </span>
            </template>
        </el-dialog>
        <!-- END 发布公告弹窗 -->
    </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
// import { useRouter } from 'vue-router';

import navTop from "@/components/navTop.vue"
import announcementListItemAdmin from "@/components/announcementListItemAdmin.vue"
import { loadAnnouncement } from "@/api/announcement.js"
//import { forum_searchArticle } from "@/api/article.js"
import router from "@/router/index.js"
import { useStore } from 'vuex' // 引入store
import { postAnnouncement } from "@/api/announcement.js"

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

const store = useStore(); // 使用store必须加上

const dialogVisible = ref(false)

const form = ref({
    announcementContent: '',
});

const addAnnouncement = () => {
    dialogVisible.value = true;
}

const submitAnnouncement = () => {
    let params = {
        adminId: store.state.Info.id,
        title: form.value.title,
        announcementContent: form.value.announcementContent,
    }
    //console.log(params);
    postAnnouncement(params);
    dialogVisible.value = false;
    fetchData();//上传新公告后，更新一下前端显示公告
    //console.log("success");
    location.reload();
};

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
#header {
  /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
  height: 10vh;
  width: 100vw;
  margin-left: auto;
  margin-right: auto;
  box-shadow: 0 2px 6px 0 #ddd;
}

#announcement-panel {
  height: 1210px;
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
  flex: 1;
}

.pagination {
  display: flex;
  justify-content: center;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10px;
  order: 1;
}
/*.header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 *
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

.announcement-list {
    width: 80vw;
    /* display: flex; 
    justify-content: center;
    align-items: center;
    margin: 0px auto;
}*/

.addAnnouncement {
    position: absolute;
    left: 8px;
    top: 110px;
}

</style>

