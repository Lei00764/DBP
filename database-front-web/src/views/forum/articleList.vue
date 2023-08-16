<!-- 文章缩略图 -->
<template>
    <div>
        <div class="article-panel">
            <div class="article-list">
                <articleListItem v-for="item in articleListInfo" :key="item.postId" :data="item">
                </articleListItem>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import router from "@/router/index.js"
import { useStore } from 'vuex'; // ！！！
import { loadArticle } from "@/api/article.js"
import articleListItem from "@/components/articleListItem.vue"

const store = useStore();
// 子组件接收父组件的传值 和 子路由接收父路由的传值不同
// 前者：defineProps()
// 后者：router.currentRoute.value.params.pBoardId

const pBoardId = ref(router.currentRoute.value.params.pBoardId);

// 存储获取的文章数据
const articleListInfo = ref([]);

// 获取文章数据
const fetchData = async () => {
    const params = {
        p_board_id: pBoardId.value,
        page_num: 1,  // 分页的前端还没有写
        page_size: 20
    };
    let result = await loadArticle(params);

    articleListInfo.value = result.data;
    // console.log(articleListInfo.value);

    // stringValue = "0"

    // else {
    //     const params = {
    //         keyword: stringValue
    //     };
    //     result = await forum_searchArticle(params);
    // }
    // store.commit('setArticles', result.data);
    // console.log(store.state.articles);
    // articleListInfo.value = store.state.articles;
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
</script>

<style>
.article-list {
    width: 80vw;
    /* display: flex; */
    justify-content: center;
    align-items: center;
    margin: 0px auto;
}
</style>

