<template>
    <div>
        <div id="header">
            <search_navTop @articlesupdated="handleArticlesUpdated"></search_navTop>
        </div>
        <div id="article-panel">
            <div class="article-list" v-if="dis_articleListInfo.length > 0">
                <articleListItem v-for="item in dis_articleListInfo" :key="item.postId" :data="item">
                </articleListItem>
            </div>
            <div v-else>
                没有相关文章
            </div>
            <div class="pagination">
                <el-pagination :current-page="currentPage" :page-size="pageSize" :total="totalCount" layout="->,prev, pager, next,jumper"
                @current-change="handlePageChange"></el-pagination>
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
const dis_articleListInfo = ref([]);
const currentPage = ref(1);   // 当前页码
const pageSize = 8;          // 每页元素数量
const totalpagenumber = ref(1);  //总页数
const totalCount = ref(0);

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
    totalpagenumber.value = Math.ceil(articleListInfo.value.length / pageSize);   //计算总页数
    totalCount.value = articleListInfo.value.length;
    dis_articleListInfo.value = articleListInfo.value.slice(0, 8);
    console.log(dis_articleListInfo.value);
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
        totalpagenumber.value = Math.ceil(articleListInfo.value.length / pageSize); //计算总页数
        totalCount.value = articleListInfo.value.length;
        dis_articleListInfo.value = articleListInfo.value.slice(0, 8);
    }
    
}

const handlePageChange = (page) => {
  if (page >= 1 && page <= totalpagenumber.value) {
    currentPage.value = page;
    const start = (currentPage.value - 1) * pageSize;
    const end = start + pageSize;
    dis_articleListInfo.value = articleListInfo.value.slice(start, end);
  }
}


// 在组件挂载时获取初始文章数据
onMounted(() => {
    fetchData();
});

</script>
  

<style>
#header {
  /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
  height: 10vh;
  width: 100vw;
  margin-left: auto;
  margin-right: auto;
  box-shadow: 0 2px 6px 0 #ddd;
}

#article-panel {
  height: 166vh;
  width: 85vw;
  margin-left: auto;
  margin-right: auto;
  margin-top: 2vh;
  box-shadow: 0 2px 6px 0 #ddd;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
}

.article-list {
  width: 80vw;
  /* display: flex; */
  justify-content: center;
  align-items: center;
  margin: 0px auto;
  flex: 1;
}

.pagination {
  width: 30vw;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 1.5vh;
  order: 1;
}
</style>
