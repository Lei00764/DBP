<template>
    <div>
        搜索结果
        <div class="article-panel">
            <div class="article-list">
                <articleListItem v-for="item in articleListInfo" :key="item.postId" :data="item">
                </articleListItem>
            </div>
        </div>
    </div>
</template>
  
<script setup>
import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router';

import { searchArticles } from "@/api/article.js"
import articleListItem from "@/components/articleListItem.vue"


const router = useRouter();
const route = useRoute();


// 存储获取的文章数据 搜索
const articleListInfo = ref([]);

const keyword = route.query.keyword; // 在 URL 中以 ? 开头的键值对

const fetchData = async () => {
    let params = {
        keyword: keyword
    };
    let result = await searchArticles(params);
    console.log(result);
    articleListInfo.value = result.data;
}

fetchData();
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
