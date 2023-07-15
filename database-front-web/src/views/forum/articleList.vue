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
import { ref, onMounted } from 'vue';
import articleListItem from "@/components/articleListItem.vue"
import { loadArticle } from "@/api/article.js"

// 存储获取的文章数据
const articleListInfo = ref([]);

// 获取文章数据
const fetchData = async () => {
    const params = {
        tag: "中餐",
        page_num: 1
    };

    const result = await loadArticle(params);
    if (!result)
        return;
    // 将结果存放在 articleListInfo 变量中
    articleListInfo.value = result.data;
    // console.log(articleListInfo.value);
};
fetchData();  // 执行
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
