<template>
    <div>
        <div id="container">
            <div id="header">
                <navTop></navTop>
            </div>
            <div id="choice-buttons">
                <el-form :inline="true" class="form-container">
                    <template v-for="(button, index) in buttonStyle" :key="index">
                        <el-form-item>
                            <el-button class="button" @click="handleButtonClick(index)"
                                :style="`background-image: url(${button.background})`">{{ button.name }}</el-button>
                        </el-form-item>
                    </template>
                </el-form>
            </div>
            <div id="content_wrapper">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import navTop from "@/components/navTop.vue"
import router from "@/router/index.js"

const pBoardId = ref(0);  // 默认为 0，即全部板块

const buttonStyle = [
    // 将图片换成修改之后的图片，注意：这里不能使用 @ 代替 src
    { name: "全部", background: "src/assets/forum-classify/all.png", hoverOpacity: 0.8 },
    { name: "中餐", background: "src/assets/forum-classify/chinesefood.png", hoverOpacity: 0.8 },
    { name: "西餐", background: "src/assets/forum-classify/westernfood.png", hoverOpacity: 0.8 },
    { name: "甜点", background: "src/assets/forum-classify/dessert.png", hoverOpacity: 0.8 },
    { name: "其他", background: "src/assets/forum-classify/others.png", hoverOpacity: 0.8 }
];


// 点击本页面上的按钮，跳转到相应的板块
const handleButtonClick = (p_board_id) => {
    router.push(`/forum/${p_board_id}`); // 根据 pBoardId 跳转到对应的子板块页面
    pBoardId.value = p_board_id;  // 更新 pBoardId
};
</script>

<style>
#container {
    height: 100vh;
    width: 100vw;
}

#header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

#choice-buttons {
    height: auto;
    width: 100vw;
    margin-top: 2vh;
    margin-bottom: 2vh;
}

.form-container {
    display: flex;
    justify-content: center;
    align-items: center;
}

.button {
    width: 240px;
    height: 70px;
    border: none;
    background-position: 20%;
    background-repeat: no-repeat;
    background-size: cover;
}

.button:hover {
    opacity: 0.8;
}

#content_warpper {
    width: 100vw;
    height: 80vh;

}
</style>