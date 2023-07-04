<template>
    <div>
        <div class="announcement-page">
            <!-- 表单 -->
            <div class="announ-head-form">
                <el-form :inline=true>
                    <!-- 搜索栏及按钮设置 -->
                    <el-form-item>
                        <el-input placeholder="请输入关键词" class="announ-head-search" v-model="formData.keywords"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" class="announ-head-searchbutton" @click="doSubmitSearch">
                            <span>搜索</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <el-form :inline=true>
                    <el-form-item>
                        <el-button type="primary" class="announ-corner-button1" @click="doSubmitSearch">
                            <span>首页</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" class="announ-corner-button2" @click="doSubmitSearch">
                            <span>个人主页</span>
                        </el-button>
                    </el-form-item>
                </el-form>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
const formData = reactive({
    keywords:'',
});
</script>

<style scoped>
.announcement-page {
    background-image: url('@/assets/announcement-bkg.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 98vh;
    width: 99vw;
}

.announ-head-form {
    position: absolute;
    top: 5%;
    left: 35%;
    /*transform: translate(-50%, -50%);*/
    height: 10%;
    width: 50%;
}
.announ-head-search {
    position: absolute;
    top: 0px;
    left: -100px;
    /* height: 32px; */
    width: 600px;
}
.announ-head-searchbutton {
    position: absolute;
    top: 0px;
    left: 490px;
    /* height: 32px; */
    width: 70px;
    /* color: green; */
    background-color: white;
    color: black;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
}

.announ-corner-button1 {
    position: absolute;
    top: 30px;
    left: 1180px;
    /* height: 32px; */
    width: 70px;
    /* color: green; */
    background-color: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
}
.announ-corner-button2 {
    position: absolute;
    top: 30px;
    left: 1230px;
    /* height: 32px; */
    width: 70px;
    /* color: green; */
    background-color: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
}
:deep(.el-input__wrapper) {
     background: #08664B;
     border-radius: 12px;
     box-shadow: 0px 4px 4px 0px gray;
     color:rgb(225, 223, 223);
}
:deep(.el-input__inner) {
    color:rgb(225, 223, 223);

}
</style>