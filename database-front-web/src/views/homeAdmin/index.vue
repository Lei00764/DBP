<template slot="color">
    <div>
        <div class="homeAdmin-page">
            <div class="homeAdmin-SearchPosition">
                <!-- 搜索栏 -->
                <el-form :inline=true>
                    <!-- 表单，让所有空间尽量显示到一行 -->
                    <el-form-item>
                        <!-- 控件1：搜索栏 -->
                        <el-input placeholder="请输入关键词" class="homeAdmin-searchbox" v-model="formData.keyword">
                            <template #prefix><!--设置前缀小图标-->
                                <el-icon>
                                    <Search />
                                </el-icon>
                            </template>
                        </el-input>
                    </el-form-item>
                    <el-form-item>
                        <!-- 控件2：搜索按钮 -->
                        <el-button type="primary" class="homeAdmin-Search-btn" @click="doSearch">
                            <span>搜索</span>
                        </el-button>
                    </el-form-item>
                </el-form>
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
            <el-form style="position: absolute;top:65%;left:5%" :inline=true>
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
import { ref, reactive } from 'vue';
import router from "@/router/index.js"
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
    router.push({ name: 'forum' })
}
const formData = reactive({
    keyword: '',
});
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

.homeAdmin-SearchPosition {
    /* 搜索栏的页面位置 */
    position: absolute;
    top: 45%;
    left: 4%;
}

.homeAdmin-searchbox {
    /* 搜索栏的大小格式*/
    width: 800px;
    height: 50px;
}

:deep(.el-input__wrapper) {
    /* 修改搜索栏的背景、圆角、字体颜色 */
    background: #08664B;
    border-radius: 12px;
    box-shadow: 0px 4px 4px 0px gray;
    color: rgb(235, 235, 235);
}

:deep(.el-input__inner) {
    /* 修改搜索栏的背景和圆角 */
    color: rgb(235, 235, 235);
}

.homeAdmin-Search-btn {
    /* 设置搜索按钮风格 */
    width: 80px;
    height: 35px;
    border-radius: 30px;
    background-color: white;
    color: black;
    border-color: black;
    box-shadow: 0px 4px 4px 0px gray;
}

.homeAdmin-logout-btn {
    /* 设置退出登录按钮风格 */
    position: absolute;
    bottom: 5%;
    left: 40%;
    width: 80px;
    height: 35px;
    border-radius: 30px;
    background-color: #08664B;
    border-color: transparent;
    color: white;
    box-shadow: 0px 4px 4px 0px gray;
}

.homeAdmin-card {
    /* 管理员资料卡片的设置 */
    position: absolute;
    top: 5%;
    left: 73%;
    background-color: rgb(255, 255, 255);
    width: 370px;
    height: 750px;
    box-shadow: 0px 4px 4px 0px gray;
    border-radius: 5px;
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

.check-btn {
    background: url('@/assets/check_btn.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 195px;
    height: 150px;
    border-radius: 20px;
}

.announce-btn {
    background: url('@/assets/announce_btn.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 195px;
    height: 150px;
    border-radius: 20px;
}

.post-btn {
    background: url('@/assets/post_btn.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 195px;
    height: 150px;
    border-radius: 20px;
}

.into-forum-btn {
    background: url('@/assets/into_forum_btn.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 195px;
    height: 150px;
    border-radius: 20px;
}
</style>