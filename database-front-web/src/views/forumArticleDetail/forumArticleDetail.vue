<template>
    <div>
        <div class="forum-article-detail-page">
            <div id="header">
                <navTop></navTop>
            </div>
            <!-- 展示帖子详情 -->
            <div class="announ-announcement-form">
                <el-button class="userReportIcon" @click="Report">
                    <font-awesome-icon :icon="['fas', 'triangle-exclamation']" />
                </el-button>
                <el-button class="userShareIcon" @click="Share">
                    <font-awesome-icon :icon="['fas', 'arrow-up-from-bracket']" />
                </el-button>
                <!-- 文章详情展示未完成 -->
                <div class="title">
                    Title:{{ props.title }}
                </div>
                <div class="content">
                    Content:{{ props.content }}
                </div>
                <div class="author">
                    Author:{{ props.author_name }}
                </div>
                <div class="publish_time">
                    Publish_Time:{{ props.publish_time }}
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive, toRefs, onMounted } from 'vue';
import { userLogin } from '@/api/user';
import Message from "@/utils/Message.js"
import { House, Star, User } from '@element-plus/icons-vue'
import { ElPagination } from 'element-plus'
import { useRouter } from 'vue-router'
import navTop from "@/components/navTop.vue"
const router = useRouter()
const props = defineProps({
    title: {
        type: String, // 可以设置传来值的类型
        default: ""
    },
    content: {
        type: String,
        default: ""
    },
    publish_time: {

    },
    author_name: {
        type: String,
        default: ""
    },
})


const Report = () => {
    //举报页
    router.push({ name: 'reportArticle' })
};

const Share=()=>{
    
}



//文章详情
const articleInfo = ref({});


const getArticleDetail = async (PostId) => {
    let result = await proxy.Request({
        url: api.getArticleDetail,
        params: {
            PostId: PostId,
        }
    });
    if (!result) {
        return;
    }
    articleInfo.value = result.data;
}

const formData = reactive({

});
</script>

<style scoped>


#header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

/*背景图相关设置 */
.forum-article-detail-page{
background-image: url('@/assets/forum_bkg.png');
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
    left: 250px;
    height: 1000px;
    width: 850px;
    border-radius: 12px;
    background-color: #e6f0f8;
}

/* 帖子标题 */
.title {
    position: absolute;
    top: 20px;
    left: 18px;
    height: 100px;
    width: 1000px;
}

/* 帖子内容展示 */
.content {
    position: absolute;
    top: 150px;
    left: 18px;
    height: 900px;
    width: 1000px;
}

/* 帖子作者 */
.author {
    position: absolute;
    top: 35px;
    left: 18px;
    height: 900px;
    width: 1000px;
}

/* 帖子发布时间 */
.publish_time {
    position: absolute;
    top: 120px;
    left: 18px;
    height: 900px;
    width: 1000px;
}




</style>