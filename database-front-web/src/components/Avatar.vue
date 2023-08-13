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
        }" :src="proxy.globalInfo.avatarUrl + userId" fit="scale-down" loading="lazy" @click="goToUserCenter">
        </el-image>
        <div v-else class="no-login">未登录</div>
    </div>
</template>
  
<script setup>
import { getCurrentInstance } from "vue";
const { proxy } = getCurrentInstance();
// 通过 <avatar :userId="用户Id" :width="50" :addLink="false"></avatar> 使用该组件

const props = defineProps({
    userId: {
        type: String,
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