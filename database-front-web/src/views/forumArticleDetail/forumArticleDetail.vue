<template>
    <div>
        <div class="forum-article-detail-page" v-if="Object.keys(articleInfo).length > 0">
            <div id="header">
                <navTop></navTop>
            </div>
            <!-- 展示帖子详情 -->
            <div class="Content-tag">
                {{ "Content" }}
            </div>
            <el-form class="announ-announcement-form" :style="{ height: formHeight }">
                <!-- 举报按钮 点击弹窗 -->
                <el-button class="userReportIcon" text @click="centerDialogVisible = true">
                    <font-awesome-icon :icon="['fas', 'triangle-exclamation']" />
                </el-button>

                <el-dialog v-model="centerDialogVisible" title="举报反馈" width="30%" align-center>
                    <span>举报原因</span>
                    <el-input type="textarea" autosize placeholder="Reason" v-model="form.reportReason">
                    </el-input>
                    <template #footer>
                        <span class="dialog-footer">
                            <el-button @click="centerDialogVisible = false">取消</el-button>
                            <el-button type="primary" @click="centerDialogVisible = false, reportConfirm(router.currentRoute.value.params.articleId)">确认</el-button>
                        </span>
                    </template>
                </el-dialog>

                <OnlineModal :controlVisible="visibleIt" @closeModal="visibleIt = false" />
                <el-button class="userShareIcon" text @click="Share">
                    <font-awesome-icon :icon="['fas', 'arrow-up-from-bracket']" />
                </el-button>
                <!-- 文章详情展示未完成 -->
                <div class="title"> {{ articleInfo[0].title }} </div>
                <!-- 需增加路径到作者个人主页 -->
                <!-- <router-link :to="`/layout`"> -->
                <userAvatar :userId="authorInfo.id" :width="50" :addLink="false"></userAvatar>
                <!-- isFollowing ? '已关注' : '关注' -->
                <div class="author">{{ articleInfo[0].authorName }}
                    <el-button @click="Follow(articleInfo[0].authorId)">{{ isFollowing ? '已关注' : '关注' }}</el-button>
                </div>
                <!-- </router-link> -->
                <div class="publish_time" v-if="articleInfo[0].releaseTime">
                    发布于 {{ articleInfo[0].releaseTime.split('T')[0] }} {{ articleInfo[0].releaseTime.split('T')[1] }}
                </div>
                <div class="content" ref="innerContent" v-html="articleInfo[0].content"></div>
            </el-form>
            <div class="Comment-tag">
                {{ "Comment" }}
            </div>
            <el-form class="comment-form">
                <div class="comment" v-if="Object.keys(articleInfo).length > 0">
                    <commentList :articleId="router.currentRoute.value.params.articleId"></commentList>
                </div>
            </el-form>
        </div>
        <!--
        <div v-if="Object.keys(articleInfo).length > 0">
            <commentList :articleId="router.currentRoute.value.params.articleId"></commentList>
        </div>
        -->
    </div>
</template>

<script setup>
import { ref, reactive, toRefs, onMounted, watch, computed } from 'vue';
import { useRouter } from 'vue-router'
import { GetInfoByID } from '@/api/user';
import { GetArticleDetailsAsync } from '@/api/article';
import { followAuthor, isfollowAuthor } from '@/api/follow';
import navTop from "@/components/navTop.vue"
import commentList from "./commentList.vue"

import { ReportArticle } from '@/api/report';//引入举报api

import { useStore } from 'vuex' // 引入store

const store = useStore(); // 使用store必须加上

const router = useRouter()

//举报弹窗
const centerDialogVisible = ref(false)
const form = ref({
    reportReason: '',
});


const Share = () => {

}

//文章详情
const articleInfo = ref({});
const authorInfo = ref({});
const userId = ref();

const getArticleDetail = async (articleId) => {
    const params = {
        article_id: articleId,
    }
    let result = await GetArticleDetailsAsync(params);
    if (!result) {
        return;
    }
    articleInfo.value = result.data;
    getAuthor(articleInfo.value[0].authorId);
    isFollow(articleInfo.value[0].authorId);
}
const getAuthor = async (userId) => {
    const params = {
        ID: userId,
        type: 1
    }
    let result = await GetInfoByID(params);
    if (!result) {
        return;
    }
    authorInfo.value = result.data;
}
// 在组件挂载时获取初始文章数据
onMounted(() => {
    getArticleDetail(router.currentRoute.value.params.articleId);
});

// 使用 watch 监听父级路由参数的变化
watch(() => router.currentRoute.value.params.pBoardId, (newValue) => {
    // console.log(newValue);
    getArticleDetail(router.currentRoute.value.params.articleId);
});

const isFollowing = ref()
//页面加载时判断是否关注
const isFollow = async (userId) => {
    const params = {
        user_id: store.state.Info.id,
        author_id: userId,
    }
    let result = await isfollowAuthor(params);
    if (result.data == true) {
        isFollowing.value = 1;
    }
    else {
        isFollowing.value = 0;
    }
}
//处理关注逻辑
const Follow = async (userId = '') => {
    const params = {
        user_id: store.state.Info.id,
        author_id: userId,
    }
    let result = await followAuthor(params);
    if (!result) {
        return;
    }
    isFollowing.value = !isFollowing.value;
}

const formData = reactive({

});

const innerContent = ref(null);
// 一个计算属性 ref
const formHeight = computed(() => {
    // 获取内部组件的高度
    if (innerContent.value) {
        const innerHeight = innerContent.value.offsetHeight;
        // 添加额外的高度，可以根据需要调整
        const extraHeight = 180;
        // 计算表单的高度
        const height = innerHeight + extraHeight;
        // 返回计算出的高度
        return height + 'px';
    }
    else {
        return 200 + 'px'
    }
})


//举报信息：作者名，作者id，举报原因，帖子标题，帖子内容

const reportConfirm = async (articleId) => {
    // 提交举报
    const params = {
        user_id: store.state.Info.id,
        article_id: articleId,
        reason: form.value.reportReason
    };
    console.log(params);
    ReportArticle(params);
};

</script>

<style scoped>
#header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

/*背景图相关设置 */

.forum-article-detail-page {

    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 100vh;
    width: 100vw;
}

.userReportIcon:hover {
    opacity: 0.8;
}

.userReportIcon {
    position: absolute;
    top: 20px;
    left: 85%;
}

.userShareIcon:hover {
    opacity: 0.8;
}

.userShareIcon {
    position: absolute;
    top: 20px;
    left: 90%;
}

/* 该版式为帖子详情页版式 */
.announ-announcement-form {
    position: absolute;
    top: 150px;
    left: 150px;
    width: 840px;
    border-radius: 12px;
    background-color: #e6f0f8;
}

.comment-form {
    position: absolute;
    top: 150px;
    left: 1050px;
    width: 500px;
    border-radius: 12px;
    background-color: #e6f0f8;
}

/*标签样式*/
.Content-tag {
    position: absolute;
    top: 110px;
    left: 130px;
    width: 65px;
    padding: 5px;
    background-color: rgba(122, 164, 255, 0.756);
    border-radius: 5px;
}

.Comment-tag {
    position: absolute;
    top: 110px;
    left: 1030px;
    width: 78px;
    padding: 5px;
    background-color: rgba(122, 164, 255, 0.756);
    border-radius: 5px;
}

/* 帖子标题 */
.title {
    font-weight: bolder;
    font-size: larger;
    position: absolute;
    top: 15px;
    left: 20px;
    max-width: 800px;
}

/* 帖子内容展示 */
.content {
    position: absolute;
    top: 150px;
    left: 20px;
    max-width: 790px;
    letter-spacing: 1px;
    line-height: 22px;
}

/* 帖子作者 */
.author {
    position: absolute;
    top: 65px;
    left: 100px;
}

/*头像 */
.avatar {
    position: absolute;
    top: 60px;
    left: 20px;
}

/* 帖子发布时间 */
.publish_time {
    position: absolute;
    top: 95px;
    left: 100px;
    color: #5e5e5e;
    font-size: small;
}


.dialog-footer button:first-child {
    margin-right: 10px;
}

.comment {
    position: absolute;
    top: 20px;
    left: 5%;
}
</style>