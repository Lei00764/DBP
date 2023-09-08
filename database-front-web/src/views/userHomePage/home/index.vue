<template>
    <div>
        <!-- ********************************* -->
        <el-dialog v-model="showTipTapEditor" title="发布文章" width="80%" height="80%" align-center>
            <tiptapEditor initial-content="
          <p> </p>
          <p> </p>
          <p> </p>
          <p> </p>
          <p> </p>
          " :active-buttons="[
              'bold',
              'italic',
              'strike',
              'underline',
              'code',
              'image',
              'h1',
              'h2',
              'h3',
              'bulletList',
              'orderedList',
              'blockquote',
              'codeBlock',
              'horizontalRule',
              'undo',
              'redo',
          ]" @update="test"></tiptapEditor>
        </el-dialog>
        <!-- ********************************* -->

        <component1 v-if="refreshs" @child-click="refreshing"></component1>


        <el-button class="pro" @click="applyForProfession">申请专业厨师认证</el-button>
        <!-- START 用户申请专业认证弹窗 -->
        <!-- 【BUG】：当没有输出完成时，用户点击确认，系统应给出提示！ lx -->
        <el-dialog v-model="dialogVisible" title="专业厨师认证申请" width="50%" :before-close="handleClose" :show-close="false">
            <el-form @submit.native.prevent="submitApplication">
                <el-form-item label="阐述:">
                    <el-input type="textarea" v-model="form.illustrate" />
                </el-form-item>
                <el-form-item label="证明:">
                    <el-input type="textarea" v-model="form.evidence" />
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="dialogVisible = false">Cancel</el-button>
                    <el-button type="primary" @click="submitApplication">Submit</el-button>
                </span>
            </template>
        </el-dialog>
        <!-- END 用户申请专业认证弹窗 -->

        <!-- 顶部的很好的 -->
        <div class="topp">
            <el-form>
                <el-form-item>
                    <el-input v-model="formData.keyword" clearable placehoder="请输入内容"
                        @keyup.enter.native="fetchData(formData.keyword)">
                    </el-input>
                </el-form-item>
                <!-- 右侧 个人信息 -->
                <div class="user-info-panel">
                    <el-button class="icon-button" @click="ToHome">
                        <font-awesome-icon :icon="['fas', 'house']" />
                        <span class="button-text">首页</span>
                    </el-button>
                    <el-button class="icon-button" @click="user = true">
                        <font-awesome-icon :icon="['fas', 'circle-user']" />
                        <span class="button-text">更改信息</span>
                    </el-button>
                    <el-dialog v-if="refreshs" v-model="user" title="更改个人信息" width="40%" height="80%" align-center>
                        <el-form-item>
                            头像：
                            <userAvatar class="img" :key="avatarKey" :userId=store.state.Info.id :width=50 :addLink="false">
                            </userAvatar>
                            <avatarUploader class="upload" @avatarUploaded="refreshing"></avatarUploader>
                        </el-form-item>
                        <div class="PersonSide_text">
                            <div class="user_name">
                                昵称：
                                <span> {{ UserInfo.name }} </span>

                            </div>
                        </div>
                    </el-dialog>
                    <el-button class="icon-button" @click="ToAnnouncement">
                        <font-awesome-icon :icon="['fas', 'paperclip']" />
                        <span class="button-text">公告栏</span>
                    </el-button>

                    <el-button class="icon-button" @click="ToLogOut">
                        <font-awesome-icon :icon="['fas', 'right-to-bracket']" />
                        <span class="button-text">退出登录</span>
                    </el-button>

                </div>
            </el-form>
        </div>

        <!-- 简直傻逼 如果你不小心看到了我的注释请无视它 它并没有什么真实意思 -->
        <!-- <el-button class=button12 round color=transparent @click="stars"
            style="color:#000000;background-color:transparent;margin-top: 2px;">
            <el-icon :size="20">
                <Star />
            </el-icon>
        </el-button> -->
        <!-- 论坛展示 -->
        <!-- 在这一部分我学会了把某些样式和定义放在一起 -->
        <div class="ShowPart">
            <p v-if="formData.posting === 0" class="Post"
                style="color:rgb(63, 66, 85);font-family: Poppins;font-size: 25px">
                Post:
            </p>
            <p v-else class="Post" style="color:rgb(63, 66, 85);font-family: Poppins;font-size: 25px">
                Star:
            </p>
            <!-- 帖子展示部分 -->
            <el-row>
                <userHomeArticleListltem v-if="refreshs"
                    v-for="item in articleListInfo.slice(formData.index, formData.index + 2)" :data="item" :articleid="item.postId"
                    @child-click="refreshing">
                </userHomeArticleListltem>
            </el-row>
            <!-- 底部页面跳转 -->
            <div class="myart1">
                <div class="article">
                    <router-view />
                </div>
                <div class="example-pagination-block">
                    <el-pagination background :page-size="2" layout="->,prev, pager, next,jumper" :total="articleNumber"
                        @current-change="handleCurrentChange" />
                </div>
                <!-- 新增的按钮部分 -->
                <div class="add-post-button">
                    <el-button  @click="showTipTapEditor = true" text class="add-button">
                        <span class="iconfont icon-icon-add"></span>
                    </el-button>
                </div>
            </div>
        </div>

    </div>
</template>
  
<script setup="props">
import { getCurrentInstance, ref, reactive, toRefs, onMounted, nextTick, watch } from 'vue';
import component1 from '../component1/component1.vue';
import { GetInfoByID } from "@/api/user.js"
import router from "@/router/index.js"
import { useRoute, useRouter } from "vue-router"
import { ApplyProfession } from "@/api/profession.js"
import { useStore } from 'vuex' // 引入store
import { searchArticle, getArticleNumber, searchArticles } from "@/api/article.js"
import userHomeArticleListltem from "@/components/userHomeArticleListltem.vue"

// START 用户申请专业认证弹窗

const store = useStore(); // 使用store必须加上

//——————————————————定义————————————————————————————
const dialogVisible = ref(false)
const form = ref({
    illustrate: '',
    evidence: '',

});
const refreshs = ref(true)
const UserInfo = ref([]);
const point = ref(0)
const user = ref(false)
const formData = reactive({
    isSigned: false,
    buttonLabel: '签到',
    posting: 0,
    index: 0,
});

onMounted(() => {
    fetchData();
    fetchuser();

})
//———————————————————函数——————————————————————————
const ToHome = () => {
    router.push(`/homeUser`);
}

const ToMy = () => {
    if (store.state.type == 1) { //管理员身份
        router.push(`/userHomePage`);
    }
    else if (store.state.type == 0) {  //用户身份
        router.push(`/homeAdmin`);
    }
}

const ToAnnouncement = () => {
    if (store.state.type == 1) { //用户身份
        router.push(`/announcementUser`);
    }
    else if (store.state.type == 0) {  //管理员身份
        router.push(`/announcementAdmin`);
    }
}

const ToLogOut = () => {
    router.push(`/login`);
}

const ToCheckMessage = () => {
    // 跳转到消息界面（管理员可以给用户发送消息）
}
const fetchuser = async () => {
    const params = {
        ID: store.state.Info.id,
        type: 1
    }
    let result = await GetInfoByID(params);
    if (!result) {
        return;
    }
    UserInfo.value = result.data;
};

const refreshing = () => {
    fetchData();
    refreshs.value = false
    nextTick(() => {
        refreshs.value = true
    })
}
const home = () => {
    router.push(`/homeUser`);
}
const applyForProfession = () => {
    dialogVisible.value = true;
}
const handleCurrentChange = (number) => {
    formData.index = number * 2 - 2;
}
const handleClose = (done) => {
    ElMessageBox.confirm('Are you sure to close this dialog?')
        .then(() => {
            done()
        })
        .catch(() => {
            // catch error
        })
}

const submitApplication = () => {
    // 提交申请的逻辑，这里暂时只做 console.log 输出
    // console.log(form.value);
    let params = {
        user_id: store.state.Info.id,
        illustrate: form.value.illustrate,
        evidence: form.value.evidence
    }
    console.log(params);
    ApplyProfession(params);
};
// END 用户申请专业认证弹窗


// START 发布文章
const showTipTapEditor = ref(false)
// END 发布文章


function stars() {
    if (formData.posting == 0) {
        formData.posting = 1;
    }
    else {
        formData.posting = 0;
    }
};

const { isSigned, buttonLabel } = toRefs(formData)
const pBoardId = ref(router.currentRoute.value.params.pBoardId);
// 文章列表获取
// 存储获取的文章数据
const articleListInfo = ref([]);
const articleNumber = ref(0);
// 获取文章数据
const fetchData = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            user_id: store.state.Info.id,
        };
        result = await searchArticle(params);
    }
    else {
        const params = {
            keyword: stringValue
        };
        result = await searchArticles(params);
    }

    if (!result)
        return;
    articleListInfo.value = result.data;
    fetchnum();

};
const fetchnum = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            user_id: store.state.Info.id,
        };
        result = await getArticleNumber(params);
    }
    else {
        const params = {
            keyword: stringValue
        };
        // 这里并没有写好还得改
        result = await getArticleNumber(params);
    }

    if (!result)
        return;
    articleNumber.value = result;

};

const input = reactive({
    code: {
        input: '',
    }

});
const CheckImgExists = (imgurl) => {
    var ImgObj = new Image() // 判断图片是否存在
    ImgObj.src = imgurl
    // 存在图片
    if (ImgObj.fileSize > 0 || (ImgObj.width > 0 && ImgObj.height > 0)) {
        return true
    } else {
        return false
    }
}



</script> 
  
<style scoped>
/* 初始化 */
.img {
    position: absolute;
    left: 43px;
    top: -5px;

}

.upload {
    position: absolute;
    left: 100px;
    top: 0px;

}

.PersonSide_text {
    margin-top: 40px;
}

.button12 {
    position: absolute;
    left: 800px;
    top: 26px;

}

.pro {
    position: absolute;
    left: 80px;
    top: 75px;
    border-radius: 15px;
}

.ShowPart {
    position: absolute;
    width: 750px;
    height: 620px;
    left: 300px;
    top: 50px;
}

.example-pagination-block {
    position: absolute;
    left: 100px;
    bottom: 120px;

}


.add-post-button {
    /* position: fixed; */
    /* bottom: 240px;
    right: 110px; */
    position: absolute;
    left: 680px;
    top: 430px;
    background-color: rgb(58, 119, 189);
    border-radius: 20%;
    display: flex;
    justify-content: center;
    align-items: center;
}

.add-button {
    color: rgb(255, 255, 255);
    text-decoration: none;
    font-size: 35px;
    line-height: 1;
}

.cross-icon {
    transform: rotate(45deg);
}

/* 头像 */
.PersonSide_img {
    width: 60px;
    height: 60px;
    background: url('@/assets/head.jpg');
    background-size: cover;
    background-position: center;
    background-color: transparent;
    margin-right: 3%;
    margin-left: 5%;
    border-radius: 20px;
}

.PersonSide_img.hover {
    opacity: 0.2;
}

/* 用来设计输入框 */
.topp {
    position: absolute;
    left: 300px;
    top: 45px;
    width: 100%;
}

.el-input {
    position: absolute;
    width: 400px;
    /*调整整个组件的宽度*/
    height: 30px;
}

:deep().el-input__wrapper {
    background: rgb(255, 255, 255);
    border: 0;
    border-radius: 10px;
}

:deep().el-input__inner {
    font-size: 14px;
    font-family: PingFangSC-Regular, PingFang SC;
    color: #ffffff;

}

/* login */
.user-info-panel {
    position: absolute;
    left: 410px;
    top: -18px;

}

.user-info-panel .el-button {
    border: none;
    background-color: transparent;
    padding: 0;
    margin-right: 1vw;
}

.user-info-panel  {
    background-color: transparent;
}

.button-text {
    margin-left: 5px;
}

.dropdown .dropdown-title {
    display: inline-block;
    position: relative;
    padding: 0 24px;
    border-radius: 4px;
    cursor: pointer;
}

.dropdown .dropdown-content {
    position: absolute;
    visibility: hidden;
    opacity: 0;
    transition: all 0.6s ease-in-out;
}

.dropdown .dropdown-content .dropdown-menu {
    margin-top: 6px;
    padding: 10px 8px 15px;
    background-color: rgba(255, 255, 255, 0.011);
    color: black;
    border-radius: 4px;
}

.dropdown .dropdown-content .dropdown-menu {
    width: 100%;
    white-space: nowrap;
    padding: 10px 16px;
    font-size: 16px;
    color: white;
    cursor: pointer;
    border-radius: 4px;
}

.dropdown .dropdown-content .dropdown-menu {
    background-color: #ccc;
}

.dropdown:hover .dropdown-content {
    visibility: visible;
    opacity: 1;
}
</style>
  





