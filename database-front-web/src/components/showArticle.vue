<template>
    <div>
        <div class="article-list">
            <div v-for="article in articleList" :key="article.PostId" class="article-item">
                <h2>{{ article.Title }}</h2>
                <span>作者名：{{ article.AuthorName }}</span>
                <span>浏览量：{{ article.Views }}</span>
                <span>点赞量：{{ article.LikeNum }}</span>
                <span>收藏量：{{ article.FavouriteNum }}</span>
            </div>
        </div>
        <div class="pagination">
            <el-pagination
                :current-page="currentPage"
                :page-size="pageSize"
                :total="totalCount"
                @current-change="handlePageChange"
            ></el-pagination>
        </div>
    </div>
</template>
  
<script setup>
import { loadArticle } from '@/api/user';
import { ref,onMounted } from 'vue';
import selecttag from './navTop.vue';
const articleList = ref([]);
const currentPage = ref(1);
const pageSize = 6;
const totalCount = ref(0);
  
const loadArticles = () => {
    const params = {
      pageNo: currentPage.value,
      pageTAG: selecttag.value
    };
  
    loadArticle(params).then((data) => {
      if (data) {
        articleList.value = data.data;
        totalCount.value = data.totalCount;
      }
    });
};
  
const handlePageChange = (page) => {
    currentPage.value = page;
    loadArticles();
};

</script>
  
<style>

.article-list {
  position: relative;
}

.article-item {
  position: absolute;
  top: 60px;
  left: 60px;
  height: 80px;
  width: 40px;
  background: #e5e5e5;
  border-color: rgb(255, 255, 255);
  border-radius: 12px;
}

.pagination {
  position: absolute;
  top: 1350px;
  left: 560px;
}
</style>
