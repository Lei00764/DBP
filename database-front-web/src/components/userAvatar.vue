<template>
    <div class="avatar" :style="{
        width: width + 'px',
        height: width + 'px',
        'border-radius': width / 2 + 'px',
    }">
        <el-image v-if="userId" :style="{
            width: width + 'px',
            height: width + 'px',
            'border-radius': width / 2 + 'px',
        }" :src="proxy.globalInfo.avatarUrl + '?userId=' + userId" fit="scale-down" loading="lazy"
            @click="goToUserCenter">
            <!-- src 拼接了字符串
                实际请求地址：http://localhost:5045/api/Files/getAvatar?userId=1 -->
        </el-image>
        <div v-else class="no-login">未登录</div>
    </div>
</template>
  
<script setup>
import { getCurrentInstance } from "vue";
const { proxy } = getCurrentInstance();
// 通过 <userAvatar :userId="用户Id" :width="50" :addLink="false"></userAvatar> 使用该组件

const props = defineProps({
    userId: {
        type: Number,
    },
    width: {
        type: Number,
        default: 60,
    },
    addLink: {
        type: Boolean,
        default: true,
    },
});

const goToUserCenter = () => {

};
</script>
  
<style lang="scss">
.avatar {
    display: flex;
    cursor: pointer;
    background: #f0f0f0;
    align-items: center;
    overflow: hidden;

    .no-login {
        width: 100%;
        text-align: center;
        font-size: 13px;
    }
}
</style>