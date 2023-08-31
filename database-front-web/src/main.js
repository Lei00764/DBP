import { createApp } from 'vue'
import App from './App.vue'
// 引入 element-plus
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
// 引入 router
import router from './router'
// 引入store
import store from './store'

// 引入图标
import * as ELIcons from '@element-plus/icons-vue'
// 图标
import "./assets/icon/iconfont.css"


/* import the fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core'
/* import font awesome icon component */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
/* import specific icons */
import { fas } from '@fortawesome/free-solid-svg-icons'
/* add icons to the library */
library.add(fas)

// 全局组件
import userAvatar from "@/components/userAvatar.vue"
import avatarUploader from "@/components/avatarUploader.vue"

const app = createApp(App)

// 挂载全局组件
for (let iconName in ELIcons) {
  app.component(iconName, ELIcons[iconName])
}
app.component('font-awesome-icon', FontAwesomeIcon)
app.component("userAvatar", userAvatar);
app.component("avatarUploader", avatarUploader);


// 从sessionStorage中恢复store状态
const savedState = JSON.parse(sessionStorage.getItem('store'))
if (savedState) {
  store.replaceState(savedState)
}

// 全局变量
// 通过 "proxy.globalInfo.变量名" 使用
// import { getCurrentInstance } from "vue";  // 需要引入
// const { proxy } = getCurrentInstance();
app.config.globalProperties.globalInfo = {
  avatarUrl: "http://localhost:5045/api/files/getAvatar/",
  search_keyword:"Search Key Words",
}

app.use(ElementPlus);
app.use(router);
app.use(store);

app.mount('#app') 