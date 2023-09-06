<template>
    <div>
        <div class="homeUser-page">
            <div id="header">
                <navTop></navTop>
            </div>
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
                <el-dialog v-if="refreshs" v-model="user" title="更改个人信息" width="40%" height="80%" align-center>
                    <el-form-item>
                        头像：
                        <userAvatar class="img" :key="avatarKey" :userId=store.state.Info.id :width=50 :addLink="false"></userAvatar>
                        <avatarUploader class="upload" @avatarUploaded="refreshing"></avatarUploader>
                    </el-form-item>
                    <div class="PersonSide_text">
                        <div class="user_name">
                            昵称：
                            <span> {{ UserInfo.name }} </span>

                        </div>
                    </div>
                </el-dialog>
            </el-form>
        </div>

        <!-- 简直傻逼 如果你不小心看到了我的注释请无视它 它并没有什么真实意思 -->
        <el-button class=button12 round color=transparent @click="stars"
            style="color:#000000;background-color:transparent;margin-top: 2px;">
            <el-icon :size="20">
                <Star />
            </el-icon>
        </el-button>
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
                    v-for="item in articleListInfo.slice(formData.index, formData.index + 2)" :data="item"
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
                    <router-link to="/addArticle" class="add-button">
                        <!-- 这里可以使用适当的图标库来创建白色十字图标 -->
                        <span class="cross-icon">+</span>
                    </router-link>
                </div>
            </div>
        </div>
        </div>

        

    </div>
</template>
  
<script setup="props">
import { getCurrentInstance, ref, reactive, toRefs, onMounted, nextTick, watch } from 'vue';
import component1 from '../component1/component1.vue';
import { GetInfoByID} from "@/api/user.js"
import router from "@/router/index.js"
import { useRoute, useRouter } from "vue-router"
import { ApplyProfession } from "@/api/profession.js"
import { useStore } from 'vuex' // 引入store
import { searchArticle, getArticleNumber, searchArticles } from "@/api/article.js"
import userHomeArticleListltem from "@/components/userHomeArticleListltem.vue"
import navTop from "@/components/navTop.vue"

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
const fetchuser = async () => {
    const params = {
        ID:store.state.Info.id,
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
const gotoLogin = () => {
    router.push(`/login`);
}
const gotoCreate = () => {
    router.push('/register');
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
#header {
    /* 如果调整height，记得去 @/components/navTop.vue 中调整 header-content 样式 */
    height: 10vh;
    width: 100vw;
    box-shadow: 0 2px 6px 0 #ddd;
}

.homeUser-page {
    background-image: url('@/assets/home-user-bkg.png');
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
.PersonSide_text{
    margin-top: 40px;
}
.button12 {
    position: absolute;
    left: 85%;
    top: 17%;

}
.pro {
    position: absolute;
    left: 7%;
    top: 18%;
    border-radius: 15px;
}
.ShowPart {
    position: absolute;
    width: 60%;
    height: 620px;
    left: 35%;
    top: 15%;
}

.example-pagination-block {
    position: absolute;
    left: 20%;
    top: 90%;
}


.add-post-button {
    /* position: fixed; */
    /* bottom: 240px;
    right: 110px; */
    position: absolute;
    left: 90%;
    top: 80%;
    width: 50px;
    height: 50px;
    background-color: #000000;
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
}

.add-button {
    color: white;
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

}

.el-input {
    position: absolute;
    width: 400px;
    /*调整整个组件的宽度*/
    height: 30px;
}



/* el-button */
.button11 {
    position: absolute;
    left: 410px;
    top: -18px;

}

.button13 {
    position: absolute;
    left: 445px;
    top: -18px;

}

/* login */
.button2 {
    border-radius: 8px;
    font-size: 12px;
    background-color: #ffffff;
    border: 2px solid rgb(46, 47, 53);
    position: absolute;
    width: 75px;
    height: 27px;
    left: 610px;
    top: -14px;
    /* 盒子阴影的样式 */
    box-shadow: 2px 2px 0px rgb(46, 47, 53);
}

/* 按钮被点击时将阴影切换 */
.button2:active {
    box-shadow: 3px 3px 3px inset rgb(99, 99, 99),
        -6px 6px 8px inset rgba(255, 255, 255, 0.6);

}

/* switch account */
.button3 {
    border-radius: 8px;
    font-size: 12px;
    color: #ffffff;
    background: rgb(255, 255, 255);
    border: 2px solid rgb(46, 47, 53);
    position: absolute;
    width: 78px;
    height: 27px;
    left: 700px;
    top: -14px;
    /* 盒子阴影的样式 */
    box-shadow: 2px 2px 0px rgb(46, 47, 53);
}

/* 按钮被点击时将阴影切换 */
.button3:active {
    box-shadow: 3px 3px 0px inset rgb(1, 18, 0),
        -3px 3px 0px inset rgba(0, 0, 0, 0.1);

}
</style>
  





