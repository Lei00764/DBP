<template>
  <div class="box">
    <div class="header">
      <p>主题切换</p>
      <div>
        <el-select v-model="currentSkinName"
                   style="width: 120px"
                   placeholder="请选择"
                   @change="switchTheme">
          <el-option v-for="(item,index) in Object.keys(themeColorObj)"
                     :key="index"
                     :label="themeColorObj[item].title"
                     :value="item">
          </el-option>
        </el-select>
      </div>
    </div>
    <el-scrollbar style="height:92vh">
      <!-- 这里放滚动内容 -->
    </el-scrollbar>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import themes from '@/utils/themes'
import { colorMix } from "@/utils/theme-tool"

const currentSkinName = ref('darkTheme');
const themeColorObj = computed(() => ({
  defaultTheme: {
    title: '浅色主题'
  },
  darkTheme: {
    title: '深色主题'
  }
}));

const themeObj = ref({});

onMounted(() => {
  switchTheme();
});

const switchTheme = (type) => {
  currentSkinName.value = type || 'defaultTheme';
  themeObj.value = themes[currentSkinName.value];
  getsTheColorScale();
  Object.keys(themeObj.value).map(item => {
    document.documentElement.style.setProperty(item, themeObj.value[item]);
  });
};

const getsTheColorScale = () => {
  const colorList = ['primary', 'success', 'warning', 'danger', 'error', 'info'];
  const prefix = '--el-color-';
  colorList.map(colorItem => {
    for (let i = 1; i < 10; i += 1) {
      if (i === 2) {
        // todo 深色变量生成未完成 以基本色设置
        themeObj.value[`${prefix}${colorItem}-dark-${2}`] = colorMix(themeObj.value[`${prefix}black`], themeObj.value[`${prefix}${colorItem}`], 1);
      } else {
        themeObj.value[`${prefix}${colorItem}-light-${10 - i}`] = colorMix(themeObj.value[`${prefix}white`], themeObj.value[`${prefix}${colorItem}`], i * 0.1);
      }
    }
  });
};
</script>

<style lang="scss" scoped>
.box {
  width: 100vw;
  height: 100vh;
  box-sizing: border-box;
  background: var(--el-bg-color);
  .header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 2vw;
    height: 7vh;
    font-size: 30px;
    font-weight: bold;
    color: var(--el-text-color-primary);
    background: var(--el-bg-color);
    border-bottom: 4px solid var(--el-color-black);
  }
  .container {
    margin: .5vw 1vh;
    width: 99vw;
  }
}
</style>
