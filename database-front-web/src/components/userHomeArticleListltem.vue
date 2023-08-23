<!-- 用户主页文章缩略图卡片 -->
<template>
  <el-card :body-style="{ padding: '0px' }" style="height: 370px" class="cards">
    <img src="https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png" class="image" />
    <div style="padding: 10px">
      <!-- 文字展示 -->
      <div class="article-panel">
        <div class="article-list">
          <div class="article-item">
            <div class="article-item-inner">
              <div class="article-body">
                <!-- 用户信息 -->
                <div class="user-info">
                  <!-- <div class="content"> {{ data.content }}</div> -->
                </div>
                <div class="title">{{ data.title }}</div>
                <div class="tag">{{ data.tag }}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="bottom">
        <time class="time">{{ currentDate }}</time>
        <el-button type="primary" round class editButton size="small" @click="edit">编辑</el-button>
        <el-button type="danger" round class deleteButton size="small" @click="deleteArticles">删除</el-button>
      </div>
    </div>
  </el-card>
</template>

<script setup>
import { defaultInitialZIndex } from 'element-plus';
import { ref, reactive, toRefs, onMounted } from 'vue';
import { useRouter } from 'vue-router'
import { deleteArticle } from "@/api/article.js"
const currentDate = ref(new Date())//

// 接收父组件的信息
const props = defineProps({

  data: {
    type: Object,
  },
  index: {
    type: Int16Array,
  },
});
const edit = () => {
  router.push({ path: 'forumArticleDetail'})
};
const deleteArticles = async(postId) => {
    let result;
    const params = {
      post_id: props.data.postId
    };
    result = await deleteArticle(params);
    // if(result.code==200){
    //   window.alert('success');
    // }
    // else{
    //   window.alert('error');
    // }
};
// console.log(props.data);
</script>

<style>
.cards {
  position: inherit;
  width: 40%;
  left: 40px;
  top: -15px;
  padding: 5px;
}

.time {
  font-size: 12px;
  color: #999;
  margin-left: 5px;
}

.bottom {
  margin-top: 5px;
  line-height: 12px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.image {
  position: relative;
  width: 320px;
  height: 250px;
}

.article-item {
  padding-left: 5px;
}

.article-item-inner {
  border-bottom: 1px solid #ddd;
  padding-bottom: 5px;
}

.article-body {
  flex: 1;
}

.user-info {
  display: flex;
  align-items: center;
  font-size: 14px;
  color: #4e5969;
}

.link-info {
  margin-left: 5px;
  color: #494949;
  text-decoration: none;
}

.link-info:hover {
  color: var(--link);
}

.post-time {
  font-size: 13px;
  color: #9a9a9a;
}

.title {
  font-weight: bold;
  text-decoration: none;
  color: #4a4a4a;
  font-size: 16px;
  margin: 10px 0px;
  display: inline-block;
}
</style>