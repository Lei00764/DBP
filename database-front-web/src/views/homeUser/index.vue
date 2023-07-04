<template>
    <div>
        <div class="homeUser-page">
            <div class="searchBox">
                <el-form ref="form" label-width="80px" :inline="true">
                    <el-form-item>
                        <el-input size="large" clearable placeholder="请输入搜索关键词" v-model="FormData.keyword"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="doSearch"
                            style="color: rgb(70, 109, 185); background-color: white">
                            <span>搜索</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>

            <div class="header">
                <el-button type="primary" @click="doLogin" style="position: absolute;left: 87%; top: 13px">
                    <span>登录</span>
                </el-button>
                <el-button type="primary" @click="doRegister" style="position: absolute;left: 92%; top: 13px">
                    <span>注册</span>
                </el-button>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';

const FormData = ref({});

</script>

<style scoped>
.homeUser-page {
    background-image: url('@/assets/background.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 107vh;
    width: 100vw;
}

.searchBox {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
}

::v-deep .el-input__wrapper {
    background-color: #08664B;
}

.header {
    position: absolute;
    width: 100%;
}
</style>