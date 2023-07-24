<!-- 文章缩略图 -->
<template>
    <div>
        <div class="article-panel">
            <div class="article-list">
                <articleListItem v-for="item in articleListInfo" :key="item.id" :data="item">
                </articleListItem>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
// import { useRouter } from 'vue-router';

import articleListItem from "@/components/articleListItem.vue"
import { loadArticle } from "@/api/article.js"
import { forum_searchArticle } from "@/api/article.js"
import router from "@/router/index.js"

// 子组件接收父组件的传值 和 子路由接收父路由的传值不同
// 前者：defineProps()
// 后者：router.currentRoute.value.params.pBoardId

const pBoardId = ref(router.currentRoute.value.params.pBoardId);

// 存储获取的文章数据
const articleListInfo = ref([]);

// 获取文章数据
const fetchData = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            p_board_id: pBoardId.value,
            page_num:1
        };
        result = await loadArticle(params);
    } 
    else {
        const params = {
            keyword: stringValue
        };
        result = await forum_searchArticle(params);
    }

    if (!result)
        return;
    articleListInfo.value = result.data;
 
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
