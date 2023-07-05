<template>
    <div>
        <div class="forum-page">
            <!-- 表单 -->
            <div class="forum-form">
                <el-form :inline=true>
                    <!-- 搜索栏及按钮设置 -->
                    <el-form-item>
                        <el-input placeholder="请输入关键词" class="forum-search" v-model="formData.keywords">
                            <template #prefix>
                                <el-icon>
                                    <Search/>
                                </el-icon>
                            </template>
                        </el-input>
                    </el-form-item>

                    <el-form-item>
                        <el-button type="primary" class="forum-searchbutton" @click="doSubmitSearch">
                            <span>搜索</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <el-form :inline=true>
                <el-form-item>
                    <el-button type="primary" class="forum-home-button" @click="doSubmitSearch">
                        <span>首页</span>
                    </el-button>
                </el-form-item>

                <el-form-item>
                    <el-button type="primary" class="forum-userhome-button" @click="doSubmitSearch">
                        <span>个人主页</span>
                    </el-button>
                </el-form-item>

                <el-form-item>
                    <el-button type="primary" class="forum-button1" @click="doSubmitSearch">
                        <span>中餐</span>
                    </el-button>
                </el-form-item>

                <el-form-item>
                    <el-button type="primary" class="forum-button2" @click="doSubmitSearch">
                        <span>西餐</span>
                    </el-button>
                </el-form-item>

                <el-form-item>
                    <el-button type="primary" class="forum-button3" @click="doSubmitSearch">
                        <span>甜点</span>
                    </el-button>
                </el-form-item>

                <el-form-item>
                    <el-button type="primary" class="forum-button4" @click="doSubmitSearch">
                        <span>其他</span>
                    </el-button>
                </el-form-item>
            </el-form>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';

const doSearch = () => {};

const formData = reactive({
    keywords: '',
});
</script>


<style scoped>
/*背景图相关设置 */
.forum-page {
    background-image: url('@/assets/forum_bkg.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 100vh;
    width: 100vw;
}

.forum-form {
/*搜索输入框相关设置 */
    position: absolute;
    top: 8%;
    left: 33%;
    height: 10%;
    width: 50%;
}

.forum-search {
    position: absolute;
    top: 0px;
    left: -100px;
    /* height: 32px; */
    width: 600px;
}

.forum-searchbutton {
/*搜索按钮相关设置 */
    position: absolute;
    top: 0px;
    left: 490px;
    height: 32px;
    width: 70px;
    border-radius: 14px;
    background-color: white;
    color: black;
    box-shadow: 0px 4px 4px 0px gray;
}

.forum-home-button {
/*首页按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1220px;
    height: 33px;
    width: 70px;
    background-color: white;
    color: black;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.forum-userhome-button {
/*个人首页按钮相关设置 */
    position: absolute;
    top: 66px;
    left: 1280px;
    height: 33px;
    width: 100px;
    background: #08664B;
    border-radius: 14px;
    box-shadow: 0px 4px 4px 0px gray;
}

.forum-button1 {
/*菜品种类第一个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 80px;
    height: 50px;
    width: 250px;
    background: #08664B;
    border-color: black;
    border-radius: 30px;
}

.forum-button2 {
/*菜品种类第二个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 400px;
    height: 50px;
    width: 250px;
    background: #08664B;
    border-color: black;
    border-radius: 30px;
}

.forum-button3 {
/*菜品种类第三个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 720px;
    height: 50px;
    width: 250px;
    background: #08664B;
    border-color: black;
    border-radius: 30px;
}

.forum-button4 {
/*菜品种类第四个按钮相关设置 */
    position: absolute;
    top: 200px;
    left: 1040px;
    height: 50px;
    width: 250px;
    background: #08664B;
    border-color: black;
    border-radius: 30px;
}

:deep(.el-input__wrapper) {
/* 搜索输入框背景、圆角、字体颜色设置 */
    background: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
    color: rgb(235, 235, 235);
}

:deep(.el-input__inner) {
    color: rgb(235, 235, 235);
}

</style>