<template slot="color">
    <div>
        <div class="homeAdmin-page">
            <div class="header">
                <navTopAdmin></navTopAdmin>
            </div>
            <div class="homeAdmin-card">
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
                        </el-form>
                    </el-form-item>
                    <div class="line"></div>
                    <!-- 这是一条分割线 -->
                </el-form>
                <el-form :inline=true style="position:absolute;top:25%;left:20%;">
                    <el-form-item style="width:50px;">
                        <b style="color:black;">ID</b>
                    </el-form-item>
                    <el-form-item>
                        <b style="position:absolute;left:80%;color:black;">{{ store.state.Info.id }}</b>
                    </el-form-item>
                </el-form>
                <el-form :inline=true style="position:absolute;top:35%;left:20%;">
                    <el-form-item style="width:50px;">
                        <b style="color:black;">Email</b>
                    </el-form-item>
                    <el-form-item>
                        <b style="position:absolute;left:80%;color:black;">{{ store.state.Info.email }}</b>
                    </el-form-item>
                </el-form>
                <el-form :inline=true style="position:absolute;top:45%;left:20%;">
                    <el-form-item style="width:50px;">
                        <b style="color:black;">Tel</b>
                    </el-form-item>
                    <el-form-item>
                        <b style="position:absolute;left:80%;color:black;">{{ store.state.Info.tel }}</b>
                    </el-form-item>
                </el-form>
                <el-form style="position:absolute;top:60%;left:20%;">
                    <el-form-item>
                    <el-button class="button0" @click="toAllFood">
                        <span>进入论坛</span>
                        <span class="iconfont icon-ic_play_black"></span>
                    </el-button>
                </el-form-item>
                </el-form>
                <!-- 退出登录的按钮 -->
                <el-button class="homeAdmin-logout-btn" style="position:absolute;bottom:5%;left:60%" @click="logout">
                    <span>退出登录</span>
                </el-button>
                <el-button class="post-notice" style="position:absolute;bottom:5%;left:15%" @click="addNotice">
                    <span>发送消息</span>
                </el-button>
            </div>
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
                    <!-- 审核留言-->
                    <el-button class="comment-btn" @click="intoCheckComment"></el-button>
                </el-form-item>
            </el-form>
        </div>
        <!-- START 发布消息弹窗 -->
        <el-dialog v-model="dialogVisible" title="给指定用户发送一条消息" width="50%" :before-close="handleClose">
            <el-form @submit.native.prevent="submitNotice">
                <el-form-item label="用户ID：">
                    <el-input type="textarea" v-model="form.USERID"/>
                </el-form-item>
                <el-form-item label="内容：">
                    <el-input type="textarea" v-model="form.CONTENT"/>
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="dialogVisible = false">Cancel</el-button>
                    <el-button type="primary" @click="submitNotice">Submit</el-button>
                </span>
            </template>
        </el-dialog>
        <!-- END 发布消息弹窗 -->
    </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import { reactive } from 'vue';
import navTopAdmin from "@/components/navTopAdmin.vue"
import { useStore } from 'vuex'//引入store
import { postNotice } from "@/api/notice.js"
import { useRouter } from 'vue-router'

const router=useRouter()
const store = useStore();//使用store必须加上

const logout = () => {
    //退出登录
    router.push({ name: 'login' })
};
const intoCheckProfession = () => {
    router.push({ name: 'work', params:{choice:'1'}})
}
const intoAnnounce = () => {
    router.push({ name: 'work', params:{choice:'3'} })
}
const intoCheckArticle = () => {
    router.push({ name: 'work', params:{choice:'2'}})
}
const intoCheckComment = () => {
    router.push({ name: 'work' , params:{choice:'4'}})
}

const toAllFood = () => {
    router.push({ path: 'layout' })
};

const formData = reactive({
    keyword: '',
});

//   发送消息START
const dialogVisible = ref(false)

const form = ref({
    USERID: '',
    CONTENT: '',
});

const addNotice = () => {
    dialogVisible.value = true;
}

const submitNotice = () => {
    let params = {
        adminId: store.state.Info.id,
        userId: form.value.USERID,
        noticeContent: form.value.CONTENT,
    }
    //console.log(params);
    postNotice(params);
    dialogVisible.value = false;
    //console.log("success");
    location.reload();
};
//   发送消息END
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
    width: 342px;
    height: 700px;
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
    height: 1px;
    background: #888888;
    position: absolute;
}

.classify-position {
    position: absolute;
    top: 30%;
    left: 40%;
}

.button0 {
    height: 100%;
    width: 190%;
    background-color: black;
    color: #ffffff;
}

.button0:hover {
    opacity: 0.8;
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
    background: url('@/assets/checkpost.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 300px;
    height: 150px;
    border-radius: 20px;
}

.comment-btn {
    background: url('@/assets/checkcomment.png');
    background-position: center center;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    width: 300px;
    height: 150px;
    border-radius: 20px;
}

.post-notice {
    /* 设置发送消息按钮风格 */
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
</style>