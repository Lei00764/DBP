<template>
  <!-- <article-item v-for="a in allData" :key="a.id" v-bind="a"/>
                    <el-empty
                    v-if="allData.length == 0"
                    :image-size="250"
                    description="暂未发表任何帖子"
                ></el-empty> -->
  <el-button class="back1">
    <el-form-item>
      <div class="pic1">
      </div>
      <div class="topic1">
        ??????
      </div>
      <div class="article1">
        好吃的
      </div>
    </el-form-item>

  </el-button>
  <el-button class="back2">
    <el-form-item>
      <div class="pic2">

      </div>
      <div class="topic2">
        ??????
      </div>
      <div class="article2">
        很好吃的
      </div>
    </el-form-item>
  </el-button>
</template>
<script setup>
</script>
<style>
.back1 {
  position: absolute;
  color: transparent;
  width: 700px;
  height: 150px;
  left: 0px;
  top: 80px;
  background: rgba(246, 246, 246, 0.5);
  border-radius:
    13px;
}

.back2 {
  position: absolute;
  color: transparent;
  width: 700px;
  height: 150px;
  left: -12px;
  top: 280px;
  background: rgba(246, 246, 246, 0.5);
  border-radius:
    13px;
}

.pic1 {
  position: absolute;
  width: 170px;
  height: 100px;
  left: -300px;
  background: url('@/assets/hamburger.png');
  background-size: cover;
  background-position: center;
  border-radius: 10px;
}

.pic2 {
  position: absolute;
  width: 170px;
  height: 100px;
  left: -300px;
  background: url('@/assets/hamburger.png');
  background-size: cover;
  background-position: center;
  border-radius: 10px;
}

.topic1 {
  /* Elaine-GIFT */
  position: absolute;
  width: 252px;
  height: 47px;
  left: 10px;
  top: -40px;
  color: rgb(57, 57, 57);
  font-family: Cabin;
  font-size: 20px;
  text-align: left;
}

.topic2 {
  /* Elaine-GIFT */
  position: absolute;
  width: 252px;
  height: 47px;
  left: 10px;
  top: -40px;
  color: rgb(57, 57, 57);
  font-family: Cabin;
  font-size: 20px;
  text-align: left;
}

.article1 {
  /* Elaine-GIFT */
  position: absolute;
  width: 252px;
  height: 47px;
  left: 10px;
  top: 0px;
  color: rgb(57, 57, 57);
  font-family: Cabin;
  font-size: 18px;
  text-align: left;
}

.article2 {
  /* Elaine-GIFT */
  position: absolute;
  width: 252px;
  height: 47px;
  left: 10px;
  top: 0px;
  color: rgb(57, 57, 57);
  font-family: Cabin;
  font-size: 18px;
  text-align: left;
}</style>
