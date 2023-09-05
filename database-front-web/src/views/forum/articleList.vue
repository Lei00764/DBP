<!-- 文章缩略图 -->
<template>
  <div>
    <div id="article-panel">
      <div class="article-list">
        <articleListItem v-for="item in dis_articleListInfo" :key="item.postId" :data="item">
        </articleListItem>
      </div>
      <div class="pagination">
        <el-pagination :current-page="currentPage" :page-size="pageSize" :total="totalCount"
          layout="->,prev, pager, next,jumper" @current-change="handlePageChange"></el-pagination>
      </div>
    </div>

  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import router from "@/router/index.js"
import { useStore } from 'vuex'; // ！！！
import { loadArticle, recommendArticle } from "@/api/article.js"
import articleListItem from "@/components/articleListItem.vue"

const store = useStore();
// 子组件接收父组件的传值 和 子路由接收父路由的传值不同
// 前者：defineProps()
// 后者：router.currentRoute.value.params.pBoardId

const pBoardId = ref(router.currentRoute.value.params.pBoardId);

// 存储获取的文章数据
const articleListInfo = ref([]);

const dis_articleListInfo = ref([]);
const currentPage = ref(1);   // 当前页码
const pageSize = 8;          // 每页元素数量
const totalpagenumber = ref(1);  //总页数
const totalCount = ref(0);

// 获取文章数据
const fetchData = async () => {
  const p_board_id = pBoardId.value;
  let result;
  console.log(p_board_id);
  if (p_board_id == 0) {
    const params = {
      user_id: store.state.Info.id
    };
    result = await recommendArticle(params)
  }
  else {
    const params = {
      p_board_id: p_board_id,
    };
    result = await loadArticle(params);
  }

  articleListInfo.value = result.data;
  currentPage.value = 1;
  totalpagenumber.value = Math.ceil(articleListInfo.value.length / pageSize);   //计算总页数
  totalCount.value = articleListInfo.value.length;
  dis_articleListInfo.value = articleListInfo.value.slice(0, 8);
};

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

// 使用 watch 监听父级路由参数的变化
watch(() => router.currentRoute.value.params.pBoardId, (newValue) => {
  // console.log(newValue);
  pBoardId.value = newValue;
  fetchData();
});
</script>

<style>
#article-panel {
  height: 1220px;
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
  display: flex;
  justify-content: center;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10px;
  order: 1;
}
</style>

