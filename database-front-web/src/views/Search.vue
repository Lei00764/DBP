<template>
    <div>
        搜索结果
        <div class="article-panel">
            <search_navTop @articlesupdated="handleArticlesUpdated"></search_navTop>
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
import search_navTop from "@/components/search_navTop.vue"
// 123
import { getCurrentInstance } from "vue"; 
const { proxy } = getCurrentInstance();


const router = useRouter();
const route = useRoute();


// 存储获取的文章数据 搜索
const articleListInfo = ref([]);

const keyword = route.query.keyword; // 在 URL 中以 ? 开头的键值对
const keywords = ref([]);
keywords.value=keyword;

const fetchData = async () => {
    let params = {
        keyword: keyword
    };
    let result = await searchArticles(params);
    console.log("666");
    console.log(result);
    articleListInfo.value = result.data;
}

const handleArticlesUpdated = async (keywords) => {
    if(keywords){
        let params = {
            keyword: keywords
        };
        let result1 = await searchArticles(params);
        console.log(result1);
        if(result1.code == 200){
            console.log(result1);
            articleListInfo.value = result1.data;
        }

    }
    
}


// 在组件挂载时获取初始文章数据
onMounted(() => {
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
