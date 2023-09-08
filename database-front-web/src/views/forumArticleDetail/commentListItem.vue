<template>
    <el-card class="card">
        <!-- 举报留言弹窗 -->
        <el-button class="userReportIcon" @click="dialogVisible = true">
            <font-awesome-icon :icon="['fas', 'triangle-exclamation']" />
            <span class="button-text">举报</span>
        </el-button>
        <el-dialog v-model="dialogVisible" title="举报留言" width="30%" align-center>
            <el-form>
                <el-form-item label="举报原因:">
                    <el-input type="textarea" v-model="form.illustrate" />
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="dialogVisible = false">取消</el-button>
                    <el-button type="primary" @click="submitApplication">确认</el-button>
                </span>
            </template>
        </el-dialog>
    
        <el-form>
            <el-form-item class="avatar-container">
                <userAvatar :userId="data.userId" :width="40" :addLink="false"></userAvatar>
                <span class="user-name">{{ data.userName }}</span>
            </el-form-item>
            
            <div class="details-container">
                <p class="comment-content">{{ data.content }}</p>
                <p class="comment-time">{{ formatTime(data.time) }}</p>
            </div>
        </el-form>
    </el-card>
</template>
  
<script setup>
import { ref, reactive } from 'vue';
import { ReportComment } from "@/api/ReportComment.js"
import { useStore } from 'vuex' // 引入store
import router from "@/router/index.js"

const store = useStore(); // 使用store必须加上

const dialogVisible = ref(false)

const form = ref({
    illustrate: '',
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
        msg_id: props.data.msgID,
        reason: form.value.illustrate,
    }
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
.card{
    background-color: #f8f8f8;
    width:340px;
    
    border-radius: 5px;
    margin: 5px 0;
    box-shadow: 0 4px 4px rgba(0, 0, 0, 0.1);
    min-height: 150px;
}

.avatar-container {
    margin-right: 15px;
    display: flex;
    align-items: center;
    width:150px;
}

.user-avatar {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    object-fit: cover;
    margin-right: 10px;
    position:absolute;
}

.user-name {
    font-size: 14px;
    color: #333;
    position:absolute;
    left:50px;
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

.userReportIcon {
    position:absolute;
    right:30%;
    background-color: transparent;
    border-color: transparent;
}
</style>
  