<template>
    <div>
        <div class="announcement-page">
            <div class="announ-head-form" :class="navBarFixed == true ? 'navBarWrap' :''">
                <el-form :inline=true>
                       <!-- 表单 -->
                    <!-- 搜索栏及按钮控件设置 -->
                    <el-form-item>
                        <el-input placeholder="请输入关键词" class="announ-head-search" v-model="formData.keywords">
                            <template #prefix>
                                <el-icon>
                                    <Search />
                                </el-icon>
                            </template>
                        </el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" class="announ-head-searchbutton" @click="doSearch">
                            <span>搜索</span>
                        </el-button>
                    </el-form-item>
                </el-form>
                <el-form :inline=true>
                 <!-- 表单 -->
                    <el-form-item>
                    <!-- 返回管理员主页 -->
                        <el-button type="primary" class="announ-corner-button1" @click="doReturnHome">
                            <span>首页</span>
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                    <!-- 返回管理员个人主页 -->
                        <el-button type="primary" class="announ-corner-button2" @click="doReturnadminHomepage">
                            <span>个人主页</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
            <el-affix position="top" class="anoun-add-new-button">
                <el-button
                    :icon="Search"
                    type="primary"
                    :disabled="currentPath"
                    @click="addNewAccoun"
                >
            </el-button>
        </el-affix>
    </div>
</div>
</template>

<script setup>
import { ref, reactive } from 'vue';
const formData = reactive({
    keywords: '',
});

const doSearch = () => {
    //这里进行搜索关键词操作
};


const doReturnHome = () => {
    //这里进行返回管理员主页（个人信息页）操作
};


const doReturnadminHomepage = () => {
    //这里进行返回管理员首页操作
};


const addNewAccoun = () => {
    //这里进行添加公告操作
};

</script>

<style scoped>
.announcement-page {
    background-image: url('@/assets/announcement_bkg.png');
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
    top: 5px;
    left: -100px;
    /* height: 32px; */
    width: 600px;
}

.announ-head-searchbutton {
    position: absolute;
    top: 5px;
    left: 490px;
    width: 70px;
    /* color: green; */
    background-color: white;
    color: black;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
}

/* 顶部导航按钮1-首页 */
.announ-corner-button1 {
    position: absolute;
    left: 680px;
    width: 70px;
    /* color: green; */
    background-color: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
}


/* 顶部导航按钮2-个人主页 */
.announ-corner-button2 {
    position: absolute;
    left: 730px;
    /* height: 32px; */
    width: 70px;
    /* color: green; */
    background-color: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
}


.anoun-add-new-button{
    
    position: absolute;
    left: 1280px;
    top: 700px;
}
:deep(.el-input__wrapper) {
    background: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
    color: rgb(225, 223, 223);
}

:deep(.el-input__inner) {
    color: rgb(225, 223, 223);

}
</style>