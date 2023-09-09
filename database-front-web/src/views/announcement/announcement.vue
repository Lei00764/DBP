<!-- 文章缩略图 -->
<template>
    <div>
        <div class="addAnnouncement">
            <el-button @click="addAnnouncement">发布公告</el-button>
        </div>
        <div class="announcement-panel">
            <div class="header">
                <navTop></navTop>
            </div>
            <div class="announcement-list">
                <announcementListItem v-for="item in announcementListInfo" :key="item.ID" :data="item">
                </announcementListItem>
            </div>
        </div>
        <!-- START 用户申请专业认证弹窗 -->
        <el-dialog v-model="dialogVisible" title="发布一条新公告" width="50%" >
            <el-form @submit.native.prevent="submitAnnouncement">
                <el-form-item label="Content:">
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
        <!-- END 用户申请专业认证弹窗 -->
    </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
// import { useRouter } from 'vue-router';

import navTop from "@/components/navTop.vue"
import announcementListItem from "@/components/announcementListItem.vue"
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
        announcementContent: form.value.announcementContent,
    }
    console.log(params);
    postAnnouncement(params);
    fetchdata();//上传新公告后，更新一下前端显示公告
    location.reload();
};
</script>

<style>
.header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

.announcement-list {
    width: 80vw;
    /* display: flex; */
    justify-content: center;
    align-items: center;
    margin: 0px auto;
}

.addAnnouncement {
    position: absolute;
    left: 8px;
    top: 110px;
}
</style>

