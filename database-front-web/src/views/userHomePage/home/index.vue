<template>
  <div>
    <component1></component1>
    <!-- 论坛展示 -->
    <!-- 在这一部分我学会了把某些样式和定义放在一起 -->
    <div class="ShowPart">
      <p class="Post" style="color:rgb(63, 66, 85);font-family: Poppins;font-size: 25px">
        Post
      </p>
      <div class="myart1">
        <!-- <article-item v-for="a in allData" :key="a.id" v-bind="a"/>
                    <el-empty
                    v-if="allData.length == 0"
                    :image-size="250"
                    description="暂未发表任何帖子"
                ></el-empty> -->

        <div class="example-pagination-block">
          <el-pagination background :page-size="2" layout="->,prev, pager, next,jumper" :total="1000" />
          <!-- @current-change="handelCurrentChange"
                    v-model:current-page="changePage.currentPage" -->
        </div>
      </div>
    </div>

  </div>
</template>
  
<script>
import { ref, reactive, toRefs, onMounted } from 'vue';
import component1 from '../component1/component1.vue';
import Message from "@/utils/Message.js"
import { ElPagination } from 'element-plus'
import router from "@/router/index.js"
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

const input = reactive({
  code: {
    input: '',
  }

});
export default {
  name: "loadingImage",
  components: {
    component1
  },

};
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
.ShowPart {
  position: absolute;
  width: 750px;
  height: 550px;
  left: 350px;
  top: 180px;
}

.example-pagination-block {
  position: absolute;
  left: 100px;
  bottom: 120px;

}
</style>
  