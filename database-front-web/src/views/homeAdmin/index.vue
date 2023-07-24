<template slot="color">
    <div>
        <div class="homeAdmin-page">
            <div class="header">
                <navTopAdmin></navTopAdmin>
            </div>
            <el-card class="homeAdmin-card">
                <!-- 管理员信息卡片 -->
                <el-form :inline=true class="homeAdmin-avatar-ID">
                    <el-form-item>
                        <!-- 头像 -->
                        <el-avatar :size="80" :src="store.state.Info.avatar"></el-avatar>
                    </el-form-item>
                    <el-form-item>
                        <el-form>
                            <!-- 用户名与ID -->
                            <h2>{{ store.state.Info.name }}</h2>
                            <div style="color:grey">ID：{{ store.state.Info.id }}</div>
                        </el-form>
                    </el-form-item>
                    <div class="line"></div><!-- 这是一条分割线 -->
                </el-form>
                <!-- 退出登录的按钮 -->
                <el-button class="homeAdmin-logout-btn" style="position:absolute;bottom:5%;left:40%" @click="logout">
                    <span>退出登录</span>
                </el-button>
            </el-card>
            <el-form style="position: absolute" class="classify-position" :inline=true>
                <!-- 功能选择分区 -->
                <el-form-item>
                    <!-- 审核专业认证-->
                    <el-button class="check-btn" @click="intoCheckProfession"></el-button>
                </el-form-item>
                <el-form-item>
                    <!-- 公告-->
                    <el-button class="announce-btn" @click="intoAnnounce"></el-button>
                </el-form-item>
                <el-form-item>
                    <!-- 审核帖子-->
                    <el-button class="post-btn" @click="intoCheckArticle"></el-button>
                </el-form-item>
                <el-form-item>
                    <!-- 进入论坛-->
                    <el-button class="into-forum-btn" @click="intoForum"></el-button>
                </el-form-item>
            </el-form>
        </div>
    </div>
</template>

<script setup>
import { reactive } from 'vue';
import router from "@/router/index.js"
import navTopAdmin from "@/components/navTopAdmin.vue"
import { useStore } from 'vuex'//引入store

const store = useStore();//使用store必须加上
const doSearch = () => {
    //进行关键词搜索
};
const logout = () => {
    //退出登录
    router.push({ name: 'login' })
};
const intoCheckProfession = () => {
    router.push({ name: 'checkProfession' })
}
const intoAnnounce = () => {
    router.push({ name: 'announcement' })
}
const intoCheckArticle = () => {
    router.push({ name: 'checkArticle' })
}
const intoForum = () => {
    router.push({ path: 'layout' })
}
const formData = reactive({
    keyword: '',
});

console.log(store.state);
</script>

<style scoped>
/*背景图相关设置 */
.homeAdmin-page {
    background-image: url('@/assets/home_administrator.png');
    /* 背景图片地址 */
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

.homeAdmin-logout-btn {
    /* 设置退出登录按钮风格 */
    position: absolute;
    bottom: 5%;
    left: 40%;
    width: 80px;
    height: 35px;
    border-radius: 30px;
    background-color: #5eaedf;
    border-color: transparent;
    color: white;
    box-shadow: 0px 4px 4px 0px gray;
}

.homeAdmin-card {
    /* 管理员资料卡片的设置 */
    position: absolute;
    top: 11%;
    left: 0.6%;
    background-color: #E7FAFE;
    width: 342px;
    height: 500px;
}

.homeAdmin-avatar-ID {
    /* 头像、昵称和ID在卡片上的排布 */
    position: absolute;
    top: 5%;
    left: 10%;
}

.line {
    /* 横线 */
    float: right;
    width: 300px;
    height: 2px;
    background: #888888;
    position: absolute;
}

.classify-position {
    position: absolute;
    top: 30%;
    left: 40%;
}

.check-btn {
    background: url('@/assets/check_btn.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 300px;
    height: 150px;
    border-radius: 20px;
}

.announce-btn {
    background: url('@/assets/announce_btn.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 300px;
    height: 150px;
    border-radius: 20px;
}

.post-btn {
    background: url('@/assets/post_btn.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 300px;
    height: 150px;
    border-radius: 20px;
}

.into-forum-btn {
    background: url('@/assets/into_forum_btn.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 300px;
    height: 150px;
    border-radius: 20px;
}
</style>