<template>
    <div style="background-color: #f8f8f8;
    border-radius: 8px;
    padding: 15px;
    margin: 10px 0;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    transition: transform 0.2s ease-in-out;
    ">
        <!-- 举报留言弹窗 -->
        <el-button class="userReportIcon" text @click="dialogVisible = true">
            <font-awesome-icon :icon="['fas', 'triangle-exclamation']" />
            <span class="button-text">举报</span>
        </el-button>
        <el-dialog v-model="dialogVisible" title="举报留言" width="30%" align-center>
            <el-form @submit.native.prevent="submitApplication">
                <el-form-item label="举报原因:">
                    <el-input type="textarea" v-model="form.illustrate" />
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="dialogVisible = false">取消</el-button>
                    <el-button type="primary" @click="dialogVisible = false,submitApplication">确认</el-button>
                </span>
            </template>
        </el-dialog>
    
        <div class="comment-info">
            <div class="avatar-container">
                <userAvatar :userId="data.userId" :width="50" :addLink="false"></userAvatar>
                <span class="user-name">{{ data.userName }}</span>
            </div>
            
            <div class="details-container">
                <p class="comment-content">{{ data.content }}</p>
                <p class="comment-time">{{ formatTime(data.time) }}</p>
            </div>
        </div>
   
    </div>
</template>
  
<script setup>
import { ref, reactive } from 'vue';
import { ReportComment } from "@/api/ReportComment.js"
import { useStore } from 'vuex' // 引入store
import router from "@/router/index.js"

const store = useStore(); // 使用store必须加上

const dialogVisible = ref(false)

const form = ref({
    Reason: '',
});

const props = defineProps({
    data: {
        type: Object,
        required: true
    },
});

const submitApplication = () => {
    let params = {
        user_id: store.state.Info.id,
        msg_id: router.currentRoute.value.params.msgId,
        reason: form.value.Reason,
    }
    console.log(params);
    ReportComment(params);
};

// 格式化时间
const formatTime = (time) => {
    const options = {
        year: 'numeric', month: 'long', day: 'numeric',
        hour: '2-digit', minute: '2-digit', second: '2-digit'
    };
    return new Date(time).toLocaleDateString(undefined, options);
};
</script>
  
<style scoped lang="scss">
// .comment {
//     background-color: #f8f8f8;
//     border-radius: 8px;
//     padding: 15px;
//     margin: 10px 0;
//     box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
//     transition: transform 0.2s ease-in-out;

//     &:hover {
//         transform: translateY(-3px);
//     }
// }

.comment-info {
    display: flex;
    align-items: center;
}

.avatar-container {
    margin-right: 15px;
    display: flex;
    align-items: center;
}

.user-avatar {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    object-fit: cover;
    margin-right: 10px;
}

.user-name {
    font-size: 14px;
    color: #333;
}

.details-container {
    flex: 1;
}

.comment-content {
    font-size: 16px;
    margin-bottom: 5px;
}

.comment-time {
    font-size: 12px;
    color: #666;
}

.button-text {
    margin-left: 5px;
}

.userReportIcon:hover {
    opacity: 0.8;
}
</style>
  