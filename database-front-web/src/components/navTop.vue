<template>
    <div>
        <div class="header-content">
            <!-- 左侧 logo -->
            <router-link to="/" class="logo">
                <span>
                    Foodieland
                </span>
            </router-link>

            <!-- 中间 搜索栏 -->
            <div class="search-panel">
                <el-input placeholder="Search Key Words" class="custom-input">
                    <!-- prefix 前置插入槽 -->
                    <template #prefix>
                        <font-awesome-icon :icon="['fas', 'magnifying-glass']" />
                    </template>
                </el-input>
            </div>

            <!-- 右侧 个人信息 -->
            <div class="user-info-panel">
                <el-button class="icon-button">
                    <font-awesome-icon :icon="['fas', 'house']" />
                    <span class="button-text">首页</span>
                </el-button>
                <el-button class="icon-button">
                    <font-awesome-icon :icon="['fas', 'circle-user']" />
                    <span class="button-text">我的主页</span>
                </el-button>
                <el-button class="icon-button">
                    <font-awesome-icon :icon="['fas', 'right-to-bracket']" />
                    <span class="button-text">退出登录</span>
                </el-button>
                <el-button class="icon-button">
                    <font-awesome-icon :icon="['fas', 'comments']" />
                    <span class="button-text">消息</span>
                </el-button>
            </div>
        </div>
    </div>
</template>

<script setup>
// 修改当前页面的 element-plus 主题色
import { changeTheme } from '@/utils/changeTheme';
changeTheme("#FFD700");  // 目前为红色，可以修改
</script>

<style>
.header-content {
    margin: 0 auto;
    align-items: center;
    /* 通过将高度设置成外层容器一致，达到居中效果 */
    height: 10vh;
    width: 80vw;
    display: flex;
}

.logo {
    /* 取消下划线样式 */
    text-decoration: none;
    font-size: 24px;
    color: rgb(96, 98, 102);
}

.search-panel {
    flex: 1;
    margin-left: 10vw;
    margin-right: 10vw;
}

.custom-input>.el-input__wrapper {
    height: 40px;
    border-radius: 10px;
    border: 1px solid rgb(96, 98, 102);
    box-shadow: none;
}

.user-info-panel {
    display: flex;
}

.user-info-panel .el-button {
    border: none;
    /* 去掉按钮的背景颜色 */
    background-color: transparent;
    padding: 0;
    /* margin: 0; */
    margin-right: 1vw;
}

.user-info-panel .el-button:hover {
    background-color: transparent;
}

.button-text {
    margin-left: 5px;
}
</style>