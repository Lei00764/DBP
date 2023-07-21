<template>
  <el-row>
    <el-col v-for="(o, index) in 2" :key="o" :span="8" :offset="index > 0 ? 2 : 0">
      <el-card :body-style="{ padding: '0px' }">
        <img src="https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png"
          class="image" />
        <div style="padding: 14px">
          <span>Yummy hamburger</span>
          <div class="bottom">
            <time class="time">{{ currentDate }}</time>
            <el-button type="primary" round class editButton size="small">编辑</el-button>
            <el-button type="danger" round class deleteButton size="small">删除</el-button>
          </div>
        </div>
      </el-card>
    </el-col>
  </el-row>
</template>


<script lang="ts" setup>
import { ref } from 'vue'
const currentDate = ref(new Date())//

// 存储返回的数据
// const allMenus = ref()
//获取数据的完整逻辑
// const getAllArticle = asyncu =>{
//   const res =  getAl1()
//   console.log(res)
// }
// getAllArtical()
</script>

<style>
.time {
  font-size: 12px;
  color: #999;
}

.bottom {
  margin-top: 13px;
  line-height: 12px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}



.image {
  width: 100%;
  display: block;
}
</style>
