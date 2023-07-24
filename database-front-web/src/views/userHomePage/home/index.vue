<template>
    <div>
        <component1></component1>
        <!-- <calendar1></calendar1> -->
        <!-- 签到积分 -->
        <div class="rectangle_point">
            <div class="point">
                积分：{{ point }}
            </div>
            <el-button @click="applyForProfession">申请专业厨师认证</el-button>
        </div>
        <!-- 签到积分 -->
        <el-form-item>
            <el-button class=sign-button round color=transparent :class="{ 'disabled': isSigned }" :disabled="isSigned"
                @click="handleSignIn" style="color: rgb(8, 102, 75);
                        background-color:rgba(224, 248, 242, 0.9);
                        ;border-radius: 15px;">
                <el-icon :size="23">
                    <CircleCheckFilled />
                </el-icon>
                {{ formData.buttonLabel }}
            </el-button>
        </el-form-item>


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
            <div class="myart1">

                <div class="article">
                    <router-view />
                </div>
                <div class="example-pagination-block">
                    <el-pagination background :page-size="2" layout="->,prev, pager, next,jumper" :total="1000" />
                    <!-- @current-change="handelCurrentChange"
                    v-model:current-page="changePage.currentPage" -->
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

// START 用户申请专业认证弹窗

const store = useStore(); // 使用store必须加上


const dialogVisible = ref(false)

const form = ref({
    illustrate: '',
    evidence: '',
});

const applyForProfession = () => {
    dialogVisible.value = true;
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


const state = reactive({
    fits: ['fill'],
    url: 'https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg',
})
const { fits, url } = toRefs(state)
function handleSignIn() {
    formData.isSigned = true;
    formData.buttonLabel = '签到成功，积分+2';
    point.value += 2
    // 执行积分+2的逻辑
}
function stars() {
    if (formData.posting == 0) {
        formData.posting = 1;
    }
    else {
        formData.posting = 0;
    }
};
const point = ref(0)
const formData = reactive({
    isSigned: false,
    buttonLabel: '签到',
    posting: 0

});
const { isSigned, buttonLabel } = toRefs(formData)
onMounted(() => {
    console.log(`计数器初始值为 ${point.value}。`)
})

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
.sign-button {
    position: absolute;
    width: 219px;
    height: 50px;
    left: 0px;
    top: 500px;
    border-radius: 15px;
}

.disabled {
    background-color: #888888;
    color: #ffffff;
    cursor: not-allowed;
}

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

/* 积分 */
.rectangle_point {
    position: absolute;
    width: 220px;
    height: 111px;
    left: 8px;
    top: 380px;
    background: rgba(224, 248, 242, 0.9);
    border-radius:
        27px;
}

/* 积分 */
.point {
    /* 积分：769 */
    position: absolute;
    left: 30px;
    top: 45px;
    color: rgb(8, 102, 75);
    font-family: Noto Sans SC;
    font-size: 20px;
    letter-spacing: 0px;
    display: flex;
}
</style>
  





