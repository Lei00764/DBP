<template>
    <div>
        <div class="homeUser-page">
            <div class="searchBox">
                <el-form :inline="true">
                    <el-form-item>
                        <el-input class="inputBox" clearable placeholder="请输入搜索关键词" v-model="FormData.keyword">
                            <template #prefix>
                                <el-icon>
                                    <Search />
                                </el-icon>
                            </template>
                        </el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="searchButton" type="primary" @click="doSearch">
                            <span>搜索</span>
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>

            <div class="header">
                <el-icon class="homeIcon" @click="goHome">
                    <HomeFilled />
                </el-icon>
                <el-icon class="userIcon" @click="goUser">
                    <UserFilled />
                </el-icon>
                <el-button class="loginButton" type="primary" @click="doLogin">
                    <span>退出登录</span>
                </el-button>
            </div>

            <div class="bottom">
                <el-form :inline="true">
                    <el-form-item>
                        <el-button class="button0" type="primary" @click="toAllFood">
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button1" type="primary" @click="toChineseFood">
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button2" type="primary" @click="toWesternFood">
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button3" type="primary" @click="toDessert">
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button class="button4" type="primary" @click="toOthers">
                        </el-button>
                    </el-form-item>
                </el-form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, reactive } from 'vue';
import router from "@/router/index.js"

const FormData = reactive({
    keyword: '',
});

const doSearch = () => {

};

const doLogin = () => {
    router.push({ name: 'login' })
};

const goHome = () => {

};

const goUser = () => {
    router.push({ name: 'userHomePage' })
};

const toAllFood = () => {
    router.push({ path: '/layout' })
};

const toChineseFood = () => {
    router.push({ path: '/forum/1' });
};

const toWesternFood = () => {
    router.push({ path: '/forum/2' });
};

const toDessert = () => {
    router.push({ path: '/forum/3' });
};

const toOthers = () => {
    router.push({ path: '/forum/4' });
};

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

.inputBox {
    width: 450px;
    height: 40px;
}

:deep(.el-input__wrapper) {
    background: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
    color: rgb(235, 235, 235);
}

:deep(.el-input__inner) {
    color: rgb(235, 235, 235);
}

.searchButton {
    border-radius: 20px;
    background-color: white;
    color: black;
    border-color: black;
    box-shadow: 0px 4px 4px 0px gray;
}

.searchButton:hover {
    border-radius: 20px;
    background-color: white;
    color: black;
    border-color: grey;
    box-shadow: 0px 4px 4px 0px gray;
}

.header {
    position: absolute;
    width: 100%;
}

.homeIcon {
    position: absolute;
    left: 81%;
    top: 18px;
    font-size: 25px;
}

.homeIcon:hover {
    color: rgb(74, 74, 74)
}

.userIcon {
    position: absolute;
    left: 85%;
    top: 18px;
    font-size: 25px;
}

.userIcon:hover {
    color: rgb(74, 74, 74)
}

.loginButton {
    position: absolute;
    left: 90%;
    top: 13px;
    border-radius: 10px;
    background-color: #08664B;
    color: white;
    border-color: black;
    box-shadow: 0px 4px 4px 0px gray;
}

.loginButton:hover {
    border-radius: 10px;
    background-color: #0f916c;
    color: white;
    border-color: grey;
    box-shadow: 0px 4px 4px 0px gray;
}

.bottom {
    position: absolute;
    top: 69%;
    left: 8%;
}

.button0 {
    width: 180px;
    height: 110px;
    border: none;
    background: url('@/assets/all.png');
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
}

.button0:hover {
    opacity: 0.8;
}

.button1 {
    width: 180px;
    height: 110px;
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
    width: 180px;
    height: 110px;
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
    width: 180px;
    height: 110px;
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
    width: 180px;
    height: 110px;
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