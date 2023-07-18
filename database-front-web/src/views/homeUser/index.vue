<template>
    <div>
        <div class="homeUser-page">
            <div class="header">
                <navTop></navTop>
            </div>

            <div class="classify">
                <el-form>
                    <el-form-item>
                        <el-button class="button1" @click="chineseFood">
                            <span>中餐</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button2" @click="westernFood">
                            <span>西餐</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button3" @click="dessert">
                            <span>甜点</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button4" @click="others">
                            <span>其他</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <div class="enter-forum">
                <el-form-item>
                        <el-button class="button0" @click="allFood">
                            <span>进入论坛</span>
                            <span class="iconfont icon-ic_play_black"></span>
                        </el-button>
                    </el-form-item>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"
import navTop from "@/components/navTop.vue"

const FormData = reactive({
    keyword: '',
});


const allFood = () => {
    router.push({ name: 'forum', params: { tag: 'all' } })
};

const chineseFood = () => {
    router.push({ name: 'forum', params: { tag: 'chinese' } })
};

const westernFood = () => {
    router.push({ name: 'forum', params: { tag: 'western' } })
};

const dessert = () => {
    router.push({ name: 'forum', params: { tag: 'dessert' } })
};

const others = () => {
    router.push({ name: 'forum', params: { tag: 'others' } })
};
</script>

<style scoped>
span{
    font-family:AlimamaShuHeiTi-Bold;
    font-weight: 2000;
}
.homeUser-page {
    background-image: url('@/assets/background.png');
    background-position: center center;
    /* 背景图片位置 */
    background-repeat: no-repeat;
    /* 背景图片是否重复 */
    background-size: 100% 100%;
    /* 背景图片大小 */
    height: 98vh;
    /* 背景图片宽高 */
    width: 99vw;
}


.header {
    position: absolute;
    width: 100%;
}


.enter-forum{
    position:absolute;
    top:75%;
    left:10%;
}


.button0 {
    height:100%;
    width:190%;
    background-color: black;
    color:#ffffff;
}

.button0:hover {
    opacity: 0.8;
}

.classify {
    position: absolute;
    top: 20%;
    left: 70%;
}



.button1 {
    width: 368px;
    height: 120px;
    border: none;
    background: url('@/assets/chinese_food.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
}

.button1:hover {
    opacity: 0.8;
}

.button2 {
    width: 368px;
    height: 120px;
    border: none;
    background: url('@/assets/western_food.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
}

.button2:hover {
    opacity: 0.8;
}

.button3 {
    width: 368px;
    height: 120px;
    border: none;
    background: url('@/assets/dessert.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
}

.button3:hover {
    opacity: 0.8;
}

.button4 {
    width: 368px;
    height: 120px;
    border: none;
    background: url('@/assets/others.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
}

.button4:hover {
    opacity: 0.8;
}
</style>