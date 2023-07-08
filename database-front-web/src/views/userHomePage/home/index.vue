<template>
    <div>
        <component1></component1>
        <!-- 签到积分 -->
        <el-button class=sign round color= transparent
            @click="signs"
            style="color: rgb(8, 102, 75);background-color:rgba(224, 248, 242, 0.9);
            width: 218px;height: 50px;left:20px;top:570px;border-radius: 15px;"
            >            
            <el-icon :size="23"><CircleCheckFilled /></el-icon>
            签到        
        </el-button>

        <!-- 简直傻逼 如果你不小心看到了我的注释请无视它 它并没有什么真实意思 -->
        <el-button class=button12 round color= transparent @click="stars"
                    style="color:#000000;background-color:transparent;margin-top: 2px;">
                    <el-icon :size="20"><Star /></el-icon>
        </el-button>
        <!-- 论坛展示 -->
        <!-- 在这一部分我学会了把某些样式和定义放在一起 -->
        <div class="ShowPart">
            <p v-if="posting===0" class="Post" style="color:rgb(63, 66, 85);font-family: Poppins;font-size: 25px">
            Post:
            </p>
            <p v-else class="Post" style="color:rgb(63, 66, 85);font-family: Poppins;font-size: 25px">
            Star:
            </p>
            <div class="myart1">
                
                <div class="article">
                  <router-view/>
                </div>
                <div class="example-pagination-block">
                    <el-pagination background
                    :page-size="2"
                    layout="->,prev, pager, next,jumper" :total="1000" />
                    <!-- @current-change="handelCurrentChange"
                    v-model:current-page="changePage.currentPage" -->                   
                </div>
            </div>
        </div>
        
    </div>
</template>
  
<script setup="props">
import { ref, reactive,toRefs,onMounted } from 'vue';
import  component1  from '../component1/component1.vue';
import Message from "@/utils/Message.js"
import { ElPagination } from 'element-plus' 
import router from "@/router/index.js"
import { useRoute,useRouter } from "vue-router"
const state = reactive({
  fits: ['fill'],
  url: 'https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg',
})
const { fits, url } = toRefs(state)
const formData = reactive({
    email: '',
    password: '',
});

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
const signs=()=>{
    
}
const stars=()=>{
    if(posting===0){
        posting=1
    }
    else{
        posting=0
    }
};
const input = reactive({
    code:{
        input:'',
    }
   
});
const CheckImgExists=(imgurl)=>{
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
.sign{
    position: absolute;
    left: 70px;
    top: 600px;
    
}
.button12{
    position: absolute;
    left: 790px;
    top: 26px;
    
}
.ShowPart{
    position: absolute;
    width: 750px;
    height: 620px;
    left:350px;
    top: 100px;
}
.example-pagination-block{
    position: absolute;
    left:100px;
    bottom: 120px;

}
</style>
  