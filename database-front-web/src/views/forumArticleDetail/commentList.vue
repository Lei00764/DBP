<template>
    <div>
        <div class="comment-panel">
            <div class="comment-title">评论</div>
            <div class="comment-list">
                <!-- :key 标识每个循环迭代的唯一，v-for 需要 -->
                <commentListItem v-for="item in commentListInfo" :key="item.msgId" :data="item">
                </commentListItem>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from "vue"
import { loadComment } from "@/api/comment.js"
import commentListItem from "./commentListItem.vue";

// 存储获取的评论数据
const commentListInfo = ref([]);

const fetchData = async () => {
    let params = {
        article_id: 25,  // 目前全部文章的评论都一样
        order: 0  // order=0 顺序 1 倒序
    };
    let result = await loadComment(params);
    console.log(result);
    commentListInfo.value = result.data;
}

// 在组件挂载时获取文章评论数据
onMounted(() => {
    fetchData();
});
</script>

<style lang="scss"></style>