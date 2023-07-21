<!-- 单个文章缩略图卡片 -->
<template>
    <div>
        <div class="article-item">
            <div class="article-item-inner">
                <div class="article-body">
                    <!-- 用户信息 -->
                    <div class="user-info">
                        <router-link :to="`/layout`">{{ data.authorName }}</router-link>
                        <div class="post-time"></div>
                        <!-- <div class="content"> {{ data.content }}</div> -->
                    </div>
                    <div class="title">{{ data.title }}</div>
                    <div class="tag">{{ data.tag }}</div>
                    <div>
                        <span>喜欢：{{ data.likeNum }}</span>
                        <span>收藏：{{ data.favouriteNum }}</span>
                        <span>观看：{{ data.views }}</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { defineProps } from 'vue';


// 接收父组件的信息
const props = defineProps({
    data: {
        type: Object
    },
});

// console.log(props.data);
</script>

<style>
.article-item {}

.article-item-inner {}


.article-body {}

.user-info {
    display: flex;
}
</style>