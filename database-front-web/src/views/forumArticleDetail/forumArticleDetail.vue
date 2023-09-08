<template>
    <div>
        <div id="header">
            <navTop></navTop>
        </div>
        <div class="forum-article-detail-page" v-if="Object.keys(articleInfo).length > 0">
            <!-- 展示帖子详情 -->
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
                    <el-button class="userShareIcon" text @click="copyPageURL" data-clipboard-text="URL_TO_COPY">
                        <font-awesome-icon :icon="['fas', 'arrow-up-from-bracket']" />
                    </el-button>

                <div class="info-container">
                    <!-- 文章标题 -->
                    <div class="title"> {{ articleInfo[0].title }} </div>
                    <!-- 作者信息 -->
                    <div class="user-avatar">
                        <userAvatar :userId="authorInfo.id" :width="50" :addLink="false"></userAvatar>
                    </div>
                    <div>
                        <!-- 文章详情展示未完成 -->
                        <!-- 需增加路径到作者个人主页 -->
                        <!-- <router-link :to="`/layout`"> -->
                        <!-- isFollowing ? '已关注' : '关注' -->
                        <b class="author">{{ articleInfo[0].authorName }}
                            <el-button class="button" @click="Follow(articleInfo[0].authorId)">{{ isFollowing ? '已关注' : '关注' }}</el-button>
                        </b>
                        <!-- </router-link> -->
                        <div class="publish_time" v-if="articleInfo[0].releaseTime">
                            发布于 {{ articleInfo[0].releaseTime.split('T')[0] }} {{ articleInfo[0].releaseTime.split('T')[1]
                            }}
                        </div>
                    </div>
                    <div class="vertical-line"></div>

                    <el-form class="article-stats" :inline="true">
                        <el-form-item class="views">
                            <span class="iconfont icon-eye" style="display: inline-block;">
                            </span>
                            <div style="display: inline-block; width: 40px; text-align: center; font-size:12px;">
                                浏览<br>
                                <div>{{ articleInfo[0].views }}</div>
                            </div>
                        </el-form-item>
                        <el-form-item class="button-like">
                            <span class="iconfont icon-heart" style="display: inline-block; color: color_like;" 
                                @click="clickToLike">
                            </span>
                            <div style="display: inline-block; width: 40px; text-align: center; font-size:12px;">
                                点赞<br>
                                <div> {{ articleInfo[0].likeNum }}</div>
                            </div>
                        </el-form-item>
                        <el-form-item class="button-favourite">
                            <span class="iconfont icon-star" style="display: inline-block; color: color_like;" 
                                @click="clickToFavourite">
                            </span>
                            <div style="display: inline-block; width: 40px; text-align: center; font-size:12px;">
                                收藏<br>
                                <div> {{ articleInfo[0].favouriteNum }}</div>
                            </div>
                        </el-form-item>
                    </el-form>

                </div>

                <div class="content" ref="innerContent" v-html="articleInfo[0].content"></div>

            </el-form>
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

import ClipboardJS from 'clipboard'

import navTop from "@/components/navTop.vue"
import commentList from "./commentList.vue"

import { like } from '@/api/like';
import { GetInfoByID } from '@/api/user';
import { favourite } from '@/api/favourite';
import { ReportArticle } from '@/api/report'; // 引入举报api

import { GetArticleDetailsAsync } from '@/api/article';
import { followAuthor, isfollowAuthor } from '@/api/follow';
import Message from "@/utils/Message.js"

import { useStore } from 'vuex' // 引入store

const store = useStore(); // 使用store必须加上

const router = useRouter()

let color_like = ref('red');
// 举报弹窗
const centerDialogVisible = ref(false)
const form = ref({
    reportReason: '',
});


// 文章详情
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

// 给文章点赞或取消点赞
const clickToLike = async () => {
    const params = {
        user_id: store.state.Info.id,
        post_id: router.currentRoute.value.params.articleId,
    }
    await like(params);
    refreshArticle(); // 点赞完成后刷新文章
}

// 收藏/取消收藏文章
const clickToFavourite = async () => {
    const params = {
        user_id: store.state.Info.id,
        post_id: router.currentRoute.value.params.articleId,
    }
    await favourite(params);
    refreshArticle(); // 收藏完成后刷新文章
}


const copyPageURL = () => {
    const currentURL = window.location.href; // 获取当前页面的 URL
    const clipboard = new ClipboardJS('.userShareIcon', {
        text: () => currentURL, // 设置要复制的文本为当前页面的 URL
    });

    clipboard.on('success', () => {
        Message.success('已将本页面链接粘贴到剪贴板');
        clipboard.destroy(); // 销毁 ClipboardJS 实例，避免重复绑定
    });

    clipboard.onClick({
        currentTarget: {
            dispatchEvent: (event) => {
                clipboard.emit('beforeCopy', { clearSelection: () => { } });
                clipboard.emit('copy', { text: clipboard.options.text() });
                clipboard.emit('afterCopy', { clearSelection: () => { } });
            },
        },
    });
}

const refreshArticle = () => {
    getArticleDetail(router.currentRoute.value.params.articleId);
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

// 页面加载时判断是否关注
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

// 处理关注逻辑
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

<style scoped lang="scss">
#header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

/*背景图相关设置 */
.user-avatar {
    position: absolute;
    top:15px;
}

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
    top: 40px;
    left: 86%;
    font-size: 18px;
}

.userShareIcon:hover {
    opacity: 0.8;
}

.userShareIcon {
    position: absolute;
    top: 75px;
    left: 86.15%;
    font-size: 18px;
}

/* 该版式为帖子详情页版式 */
.announ-announcement-form {
    position: absolute;
    top: 150px;
    left: 140px;
    width: 840px;
    border-radius: 12px;
}

.comment-form {
    position: absolute;
    top: 100px;
    left: 1000px;
    width: 500px;
    border-radius: 12px;
    background-color: #e6f0f8;
}

/* 帖子标题 */
.title {
    font-weight: bolder;
    font-size: 26px;
    position: absolute;
    left: 20px;
    max-width: 800px;
}

/* 帖子内容展示 */
.content {
    position: absolute;
    top: 130px;
    left: 5px;
    border-radius: 10px;
    max-width: 750px;
    min-width: 750px;
    min-height: 450px;
    letter-spacing: 1px;
    line-height: 22px;
    padding:20px;
    padding-left:30px;
    box-shadow: 4px 4px 4px 4px gray;
}

/* 帖子作者 */
.author {
    position: absolute;
    top: 55px;
    left: 80px;
    
}

.button {
    width:50px;
    height:20px;
    position:absolute;
    left:50px;
    border-radius: 5px;

    border: 1px solid rgba(0, 0, 0, 0.9);
}

/*头像 */
.avatar {
    position: absolute;
    top: 35px;
    left: 20px;
}

/* 帖子发布时间 */
.publish_time {
    position: absolute;
    top: 85px;
    left: 80px;
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

.vertical-line {
    position: absolute;
    top: 19px;
    left: 300px;
    width: 2px;
    height: 55px;
    background: #ccc;
    display: inline-block;
    margin-top: 31px;
    vertical-align: top;
}

.info-container {
    display: flex;
    align-items: center;
}

.article-stats {
    position: absolute;
    top: 45px;
    left: 320px;

    .views {
        margin-left: 25px;
    }

    .button-like {
        margin-left: 25px;
    }

    .button-favourite {
        margin-left: 25px;
    }
}
</style>