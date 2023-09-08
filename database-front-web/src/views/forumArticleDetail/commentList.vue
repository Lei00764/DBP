<template>
    <div>
        <div class="comment-panel">
            <el-form :inline="true">
                <el-form-item>
                    <b class="comment-title">全部评论</b>
                </el-form-item>
                <el-form-item>
                    <div class="mb-2 flex items-center text-sm">
                    <!-- 用户改变排序方式时，重新请求数据 -->
                    <el-radio-group v-model="order" class="ml-4" @change="fetchData">
                    <el-radio label="0" size="large">正序</el-radio>
                    <el-radio label="1" size="large">倒序</el-radio>
                    </el-radio-group>
                    </div>
                </el-form-item>
            </el-form>
            <el-form>
                <el-form-item class="comment-input">
                <input class="Input" v-model="commentContent" placeholder="请输入评论内容" />
                <button class="send-button" @click="sendComment">发送评论</button>
                </el-form-item>
                <el-form-item>
                <!-- :key 标识每个循环迭代的唯一，v-for 需要 -->
                    <commentListItem v-for="(item,index) in commentListInfo" :key="item.msgId" :data="item">
                    </commentListItem>
                </el-form-item>
            </el-form>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from "vue"
import { useStore } from 'vuex' // 引入store
// loadComment 加载评论
// postComment 上传评论 user_id + article_id + content
import { loadComment, postComment } from "@/api/comment.js"
import commentListItem from "./commentListItem.vue";
import Message from "@/utils/Message.js"

const store = useStore(); // 使用store必须加上

// 接受来自父组件的数据 articleId
const props = defineProps({
    articleId: {
        type: String
    },
});

const order = ref('0')  // 顺序

// 存储获取的评论数据
const commentListInfo = ref([]);

// 存储将要评论的数据
const commentContent = ref("");

// 获取当前文章的所有评论
const fetchData = async () => {
    let params = {
        article_id: props.articleId,
        order: order.value  // order=0 正序 1 倒序
    };
    let result = await loadComment(params);
    commentListInfo.value = result.data;
}

// 发送评论数据
const sendComment = async () => {
    // 检查评论内容是否为空
    if (commentContent.value.trim() === "") {
        Message.error("评论内容不能为空");
        return;
    }

    let params = {
        user_id: store.state.Info.id,
        article_id: props.articleId,
        content: commentContent.value.trim()
    };

    // 发送评论
    await postComment(params);

    // 清空评论内容
    commentContent.value = "";
    // 刷新评论列表
    fetchData();
}

// 在组件挂载时获取文章评论数据
onMounted(() => {
    fetchData();
});
</script>

<style lang="scss">
.comment-input{
    width:350px;
}

.Input{
    -web-kit-appearance:none;
    -moz-appearance: none;
    outline:0;

    border-radius: 10px;
    height:30px;
    max-width:250px;
    min-width:250px;
}

.send-button{
    position:absolute;
    right:2%;
    top:8.5%;
    border-radius: 10px;
    height:30px;
    background-color: rgb(45, 155, 45);
    color:white;
}
</style>