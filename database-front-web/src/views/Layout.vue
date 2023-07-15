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
                            <el-button class="button" type="primary" @click="handleButtonClick(index)"
                                :style="`background-image: url(${button.background})`"></el-button>
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


const buttonStyle = [
    // 将图片换成修改之后的图片，注意：这里不能使用 @ 代替 src
    { background: "src/assets/all.png", hoverOpacity: 0.8 },
    { background: "src/assets/chinese_food.png", hoverOpacity: 0.8 },
    { background: "src/assets/western_food.png", hoverOpacity: 0.8 },
    { background: "src/assets/dessert.png", hoverOpacity: 0.8 },
    { background: "src/assets/others.png", hoverOpacity: 0.8 }
];

const handleButtonClick = (pBoardId) => {
    router.push(`/forum/${pBoardId}`); // 根据 pBoardId 跳转到对应的子板块页面
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
    width: 180px;
    height: 110px;
    border: none;
    background-position: center;
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