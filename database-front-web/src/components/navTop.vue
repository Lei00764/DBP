<template>
    <div>
        <!-- 用户主页顶部导航栏 -->
        <el-form-item>
            <el-button type="primary" class="forum-button1" @click="doAllFood">
                <span>全部</span>
            </el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-button2" @click="doChineseFood">
                <span>中餐</span>
            </el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-button3" @click="doWesternFood">
                <span>西餐</span>
            </el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-button4" @click="doDessert">
                <span>甜点</span>
            </el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-button5" @click="doOthers">
                <span>其他</span>
            </el-button>
        </el-form-item>
    </div>
</template>



<script setup>
import { ref } from 'vue';
import router from "@/router/index.js"
const selecttag=ref('')

const doAllFood = () => {
    selecttag.value="全部";
    router.push({ name: 'forum', params: { tag: 'all' } })
};

const doChineseFood = () => {
    selecttag.value="中餐";
    router.push({ name: 'forum', params: { tag: 'chinese' } })
};

const doWesternFood = () => {
    selecttag.value="西餐";
    router.push({ name: 'forum', params: { tag: 'western' } })
};

const doDessert = () => {
    selecttag.value="甜点";
    router.push({ name: 'forum', params: { tag: 'dessert' } })
};

const doOthers = () => {
    selecttag.value="其他"
    router.push({ name: 'forum', params: { tag: 'others' } })
};



</script>


<style>
.forum-button1 {
    /*菜品种类第一个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 100px;
    height: 50px;
    width: 210px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button1:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}

.forum-button2 {
    /*菜品种类第二个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 360px;
    height: 50px;
    width: 210px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button2:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}

.forum-button3 {
    /*菜品种类第三个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 620px;
    height: 50px;
    width: 210px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button3:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}

.forum-button4 {
    /*菜品种类第四个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 880px;
    height: 50px;
    width: 210px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button4:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}

.forum-button5 {
    /*菜品种类第五个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 1140px;
    height: 50px;
    width: 210px;
    background: #08664B;
    border-color: rgb(255, 255, 255);
    border-radius: 30px;
}

.forum-button5:hover {
    border-radius: 30px;
    background-color: #f8b720;
    color: #08664B;
    border-color: rgb(255, 255, 255);
}
</style>