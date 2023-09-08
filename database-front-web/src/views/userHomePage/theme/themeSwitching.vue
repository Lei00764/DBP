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

    </el-scrollbar>
  </div>
</template>

<script>
import themes from '@/utils/themes'
import {colorMix} from "@/utils/theme-tool"
import {defineAsyncComponent} from 'vue'
export default {
  name: "Theme",
 
  data() {
    return {
      currentSkinName: 'darkTheme',
      themeColorObj: {
        defaultTheme: {
          title: '浅色主题'
        },
        darkTheme: {
          title: '深色主题'
        }
      },
      themeObj: {}
    };
  },
  mounted() {
    this.switchTheme()
  },
  methods: {
    // 根据不同的主题类型 获取不同主题数据
    switchTheme(type) {
      // themes 对象包含 defaultTheme、darkTheme 两个属性即默认主题与深色主题
      this.currentSkinName = type || 'defaultTheme'
      this.themeObj = themes[this.currentSkinName]
      this.getsTheColorScale()
      // 设置css 变量
      Object.keys(this.themeObj).map(item => {
        document.documentElement.style.setProperty(item, this.themeObj[item])
      })
    },
    //  // 获取色阶
    getsTheColorScale() {
      const colorList = ['primary', 'success', 'warning', 'danger', 'error', 'info']
      const prefix = '--el-color-'
      colorList.map(colorItem => {
        for (let i = 1; i < 10; i += 1) {
          if (i === 2) {
            // todo 深色变量生成未完成 以基本色设置
            this.themeObj[`${prefix}${colorItem}-dark-${2}`] = colorMix(this.themeObj[`${prefix}black`], this.themeObj[`${prefix}${colorItem}`], 1)
          } else {
            this.themeObj[`${prefix}${colorItem}-light-${10 - i}`] = colorMix(this.themeObj[`${prefix}white`], this.themeObj[`${prefix}${colorItem}`], i * 0.1)
          }
        }
      })
    }
  }
}
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