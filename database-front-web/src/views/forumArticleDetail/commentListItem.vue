<template>
    <div class="comment">
        <div class="comment-info">
            <div class="avatar-container">
                <userAvatar :userId="data.userId" :width="50" :addLink="false"></userAvatar>
            </div>
            <div class="details-container">
                <p class="comment-content">{{ data.content }}</p>
                <p class="comment-time">{{ formatTime(data.releaseTime) }}</p>
            </div>
        </div>
    </div>
</template>
  
<script setup>
import { defineProps } from 'vue';

const props = defineProps({
    data: {
        type: Object,
        required: true
    }
});

// 格式化时间
const formatTime = (time) => {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(time).toLocaleDateString(undefined, options);
};
</script>
  
<style scoped lang="scss">
.comment {
    background-color: #f8f8f8;
    border-radius: 8px;
    padding: 15px;
    margin: 10px 0;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    transition: transform 0.2s ease-in-out;

    &:hover {
        transform: translateY(-3px);
    }
}

.comment-info {
    display: flex;
    align-items: center;
}

.avatar-container {
    margin-right: 15px;
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

.user-avatar {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    object-fit: cover;
}
</style>
  