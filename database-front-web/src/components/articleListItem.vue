<!-- 单个文章缩略图卡片 -->
<template>
  <div>
    <div class="article-item">
      <!-- <router-link tag="button" @dblclick="" :to="`/forumArticleDetail/${data.id}`"> -->
      <div class="article-item-inner">
        <div class="article-body">

          <!--显示用户信息-->
          <div class="user-info">
            <userAvatar :userId=data.authorId :width=30 :addLink="false"></userAvatar>
            <div class="user-info">{{ data.authorName }}</div>

            <el-divider direction="vertical"></el-divider>

            <div class="post-time">{{ data.releaseTime }}</div>

            <el-divider direction="vertical"></el-divider>

            <div class="tag">{{ data.tag }}</div>
          </div>
          <!--显示用户信息-->

          <!--显示帖子信息-->
          <router-link :to="`/forumArticleDetail/${data.postId}`" class="title">{{ data.title }}</router-link>
          <div class="summary">{{ data.summary }}</div>
          <div class="article-info">
            <span class="iconfont icon-eye">
              {{ data.views == 0 ? "浏览" : data.views }}
            </span>
            <span class="iconfont icon-heart">
              {{ data.likeNum == 0 ? "点赞" : data.likeNum }}
            </span>
            <span class="iconfont icon-star">
              {{ data.favouriteNum == 0 ? "收藏" : data.favouriteNum }}
            </span>
          </div>
        </div>
        <router-link :to="`/forumArticleDetail/${data.postId}`">
          <Cover :cover="data.Picture" :width="100" v-if="data.Picture"></Cover>
        </router-link>
        <!--显示帖子信息-->
      </div>
      <!-- </router-link> -->
    </div>
  </div>
</template>

<script setup>

// 接收父组件的信息
const props = defineProps({
  data: {
    type: Object
  },
});

// console.log(props.data);
</script>

<style>
.article-item {
  padding: 5px 15px 0 15px;

  .article-item-inner {
    border-bottom: 1px solid #ddd;
    padding: 10px 0px;
    display: flex;

    .article-body {
      flex: 1;

      .user-info {
        display: flex;
        align-items: center;
        font-size: 14px;
        color: #4e5969;

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
      }

      .title {
        font-weight: bold;
        text-decoration: none;
        color: #4a4a4a;
        font-size: 16px;
        margin: 10px 0px;
        display: inline-block;
      }

      .title:hover {
        color: rgb(121, 182, 248)
      }

      .summary {
        color: rgb(132, 132, 132)
      }

      .tag {
        color: rgb(132, 132, 132)
      }

      .article-info {
        margin-top: 10px;
        display: flex;
        align-items: center;
        font-size: 13px;

        .iconfont {
          color: #86909c;
          margin-right: 25px;
          font-size: 14px;
        }

        .iconfont:before {
          padding-right: 3px;
        }
      }
    }
  }
}

.article-item:hover {
  background: #fffbfb;
}</style>