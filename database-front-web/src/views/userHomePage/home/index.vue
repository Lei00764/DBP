<template>
    <div>
        <component1></component1>
        <!-- <calendar1></calendar1> -->
        


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
                    <el-input v-model="formData.keyword" clearable placehoder="请输入内容" @keyup.enter.native="fetchData(formData.keyword)">
                    </el-input>
                </el-form-item>
                <el-button class=button11 round color=transparent @click="home"
                    style="color:#000000;background-color:transparent;margin-top: 2px;">
                    <el-icon :size="20">

                        <House />

                    </el-icon>
                </el-button>
                <el-button class=button13 round color=transparent @click="user"
                    style="color:#000000;background-color:transparent;margin-top: 2px;">
                    <el-icon :size="20">

                        <User />

                    </el-icon>
                </el-button>

                <div class="button2" @click="gotoLogin">
                    &emsp;退出登录
                </div>
                <div class="button3" @click="gotoCreate">
                    &emsp;创建账号
                </div>
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
                <userHomeArticleListltem v-for="item in articleListInfo.slice(formData.index, formData.index + 2)"
                    :data="item">
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
</template>
  
<script setup="props">
import { ref, reactive, toRefs, onMounted } from 'vue';
import component1 from '../component1/component1.vue';
import Message from "@/utils/Message.js"
// import { ElPagination } from 'element-plus'
import router from "@/router/index.js"
import { useRoute, useRouter } from "vue-router"
import { ApplyProfession } from "@/api/profession.js"
import { useStore } from 'vuex' // 引入store
import { searchArticle, getArticleNumber,searchArticles } from "@/api/article.js"
import userHomeArticleListltem from "@/components/userHomeArticleListltem.vue"

// START 用户申请专业认证弹窗

const store = useStore(); // 使用store必须加上

//——————————————————定义————————————————————————————
const dialogVisible = ref(false)
const form = ref({
    illustrate: '',
    evidence: '',
});
//用户头像
const state = reactive({
    fits: ['fill'],
    url: 'https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg',
})
const { fits, url } = toRefs(state)
const point = ref(0)
const formData = reactive({
    isSigned: false,
    buttonLabel: '签到',
    posting: 0,
    index: 0,
});

onMounted(() => {
    fetchData();
    fetchnum();

})
//———————————————————函数——————————————————————————


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
            user_id: 8
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

};
const fetchnum = async (stringValue = '') => {
    let result;
    if (!stringValue) {
        stringValue = "0"
        const params = {
            user_id: 8
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


// const changePage = reactive({
//   currentPage: 1,
//   total: gdata.length + 1 / 9,
// });

// const handelCurrentChange = (value) => {


//   //获取当前页码
//     changePage.currentPage = value; 


//   //判断当前页是否为首页 页码从1开始，是则直接调用后端数据，否则要进行计算
//     if (value > 1) {

//       var i = (value - 1) * 2;  //计算当前页第一条数据的下标，

//       var arry = [];  //建立一个临时数组

//       //比如每页10条数据，第二页的第一条数据就是从 （2-1）*10 = 10 开始的 结束下标就是2*10=20 
//       while (i < value * 2) {
//        //解决最后一页出现null值
//         if (gdata[i] != null) {
//           arry.push(gdata[i]);
//           i++;
//           continue
//         }
//         break
//       }
//       sdata.value=arry

//      } else {

//       sdata.value = gdata;

//     }
//   }


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
.button12 {
    position: absolute;
    left: 800px;
    top: 26px;

}

.ShowPart {
    position: absolute;
    width: 750px;
    height: 620px;
    left: 350px;
    top: 100px;
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
    top: 450px;
    width: 50px;
    height: 50px;
    background-color: #446b5c;
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
    left: 300px;
    top: 45px;
    width: 800px;
}

.el-input {
    position: absolute;
    width: 400px;
    /*调整整个组件的宽度*/
    height: 30px;
}

:deep().el-input__wrapper {
    background: rgb(8, 102, 75);
    border: 0;
    border-radius: 10px;
}

:deep().el-input__inner {
    font-size: 14px;
    font-family: PingFangSC-Regular, PingFang SC;
    color: #ffffff;

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
    background: rgb(8, 102, 75);
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
  





