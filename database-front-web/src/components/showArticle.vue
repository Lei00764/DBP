<template>
    <div>
        <el-form-item>
            <el-button type="primary" class="forum-article1" @click="doArticle"></el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-article2" @click="doArticle"></el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-article3" @click="doArticle"></el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-article4" @click="doArticle"></el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-article5" @click="doArticle"></el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-article6" @click="doArticle"></el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-article7" @click="doArticle"></el-button>
        </el-form-item>

        <el-form-item>
            <el-button type="primary" class="forum-article8" @click="doArticle"></el-button>
        </el-form-item>
    </div>
</template>

<script setup>
const doArticle = () => { };
</script>

<style>
.forum-article1 {
    /*文章栏相关设置 */
    position: absolute;
    top: 360px;
    left: -60px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}

.forum-article1:hover {
    opacity: 0.7;
}

.forum-article2 {
    /*文章栏相关设置 */
    position: absolute;
    top: 360px;
    left: 635px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}

.forum-article2:hover {
    opacity: 0.7;
}

.forum-article3 {
    /*文章栏相关设置 */
    position: absolute;
    top: 600px;
    left: -124px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}

.forum-article3:hover {
    opacity: 0.7;
}

.forum-article4 {
    /*文章栏相关设置 */
    position: absolute;
    top: 600px;
    left: 572px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}

.forum-article4:hover {
    opacity: 0.7;
}

.forum-article5 {
    /*文章栏相关设置 */
    position: absolute;
    top: 840px;
    left: -188px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}

.forum-article5:hover {
    opacity: 0.7;
}

.forum-article6 {
    /*文章栏相关设置 */
    position: absolute;
    top: 840px;
    left: 509px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}

.forum-article6:hover {
    opacity: 0.7;
}

.forum-article7 {
    /*文章栏相关设置 */
    position: absolute;
    top: 1080px;
    left: -252px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}

.forum-article7:hover {
    opacity: 0.7;
}

.forum-article8 {
    /*文章栏相关设置 */
    position: absolute;
    top: 1080px;
    left: 446px;
    height: 180px;
    width: 640px;
    background: #e5e5e5;
    border-color: rgb(255, 255, 255);
    border-radius: 12px;
}

.forum-article8:hover {
    opacity: 0.7;
}

.elui {
    position: absolute;
    top: 1300px;
    left: 580px;
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