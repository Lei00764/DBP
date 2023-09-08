<template>
    <div>
        <div class="addArticle">
            <!-- 顶部导航栏 -->
            <div id="header">
                <navTopEdit></navTopEdit>
            </div>
            <div class="background2">
            </div>

            <!-- 展示公告板，添加内容 -->

            <div class="editArticle-form">
                <span class="editArticle">修改文章</span>
                <span class="editArticle-title-word">标题</span>
                <input class="editArticle-title" v-model="formData.title" placeholder="Please input title" disabled>
                <span class="editArticle-tag-word">标签</span>
                <input class="editArticle-tag" v-model="formData.tag" placeholder="Please input title" disabled>
            </div>
            <img v-bind:src="formData.picture" class="image" />
            <textarea class="editArticle-content" v-model="formData.content" placeholder="Type what you want"></textarea>
            <el-button type="primary" class="publish-button" @click="edit">
                <span class="iconfont icon-publish"></span>
                <span>发布</span>
            </el-button>
            <el-button type="primary" class="return-button" @click="doUser">
                <span class="iconfont icon-publish"></span>
                <span>取消</span>
            </el-button>
        </div>
    </div>
</template>

<script setup>
import router from "@/router/index.js"
import { ref, reactive, toRefs, onMounted } from 'vue';
import { useRoute } from 'vue-router'
import { postArticle, GetArticleDetailsAsync, editArticle } from "@/api/article.js"
import { useStore } from 'vuex' // 引入store
import navTopEdit from "@/components/navTopEdit.vue"
onMounted(() => {
    getArticleDetail(route.query.articleId);
});

const store = useStore(); // 使用store必须加上
const route = useRoute();
const articleInfo = ref([]);
const getArticleDetail = async () => {
    const params = {
        article_id: route.query.articleId,
    }
    let result = await GetArticleDetailsAsync(params);
    if (!result) {
        return;
    }

    articleInfo.value = result.data;
    formData.title = articleInfo.value[0].title;
    formData.tag = articleInfo.value[0].tag;
    formData.content = articleInfo.value[0].content;
    formData.picture = articleInfo.value[0].picture;
}
const formData = reactive({
    title: "",
    tag: "",
    content: "",
    picture:"",

});
const edit = async () => {
    let result;
    const params = {
        post_id: route.query.articleId,
        title: formData.title,
        content: formData.content,
    };
    console.log(params);
    result = await editArticle(params);
    if (result.code == 200) {
        window.alert('success');
        doUser();
    }
    else {
        window.alert('error');
    }
};
const doHome = () => {
    //返回主页
    router.push({ name: 'homeUser' });
}
const doUser = () => {
    //返回用户主页
    router.push(`/userHomePage`);
};
const doReturn = () => {
    //返回论坛界面
    router.push({ path: 'userHomePage' })
};
const doLogoff = () => {
    //退出登录
    router.push({ name: 'login' })
};
</script>

<style scoped>
#header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}
.background2 {
    /* Rectangle 4 */
    position: absolute;
    left: 0;
    width: 300px;
    height: 100%;
    /* White */
    background: rgb(218, 244, 250);
    /* Elevation / 06 */
    box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.1);
}

.addArticle {
    background-image: url('@/assets/editArticle.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    background-size: 100% 100%;
    height: 100vh;
    width: 100vw;
}
/* 发布按钮 */
.publish-button {
    position: absolute;
    top: 350px;
    left: 50px;
    background: #000000cb;
}
.return-button{
    position: absolute;
    top: 350px;
    left: 150px;
    background: #000000cb;
}

/* 发布文章界面 */
.editArticle{
    position: absolute;
    top: 15px;
    left: 30px;
    height: 20px;
    width: 170px;
    font-size: 20px;
    color: rgb(90, 90, 90);
}
.editArticle-form {
    position: absolute;
    top: 75px;
    left: 0px;
    height: 750px;
    width: 1150px;
    border-radius: 2px;

}

.editArticle-title {
    position: absolute;
    top: 75px;
    left: 80px;
    height: 20px;
    width: 170px;
    border-radius: 5px;
    background-color: rgba(223, 223, 223, 0.59);
    border-color: rgb(135, 135, 135);
    font-size: 10px;
    color: rgb(135, 135, 135);
}

.editArticle-title-word {
    position: absolute;
    top: 77px;
    left: 30px;
    font-size: 15px;
    color: rgb(135, 135, 135);

}

.editArticle-tag {
    position: absolute;
    top: 155px;
    left: 80px;
    height: 20px;
    width: 170px;
    border-radius: 5px;
    background-color: rgba(223, 223, 223, 0.59);
    border-color: rgb(135, 135, 135);
    font-size: 10px;
    color: rgb(135, 135, 135);
}

.editArticle-tag-word {
    position: absolute;
    top: 157px;
    left: 30px;
    font-size: 15px;
    color: rgb(135, 135, 135);

}

.image {
    position: relative;
    top: 40px;
    left: 330px;
    width: 420px;
    height: 420px;
    border-radius: 20px;
}
.editArticle-content {
    position: absolute;
    top: 140px;
    left: 800px;
    height: 350px;
    width: 310px;
    border-radius: 12px;
    background-color: white;
    border-style: dashed;
    border-color: rgb(135, 135, 135);
    font-size: large;
}


</style>