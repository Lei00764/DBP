import { createApp } from 'vue'
import App from './App.vue'
// 引入 element-plus
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
// 引入 router
import router from './router'

import * as ELIcons from '@element-plus/icons-vue'//引入图标

// 全局方法 
// import Message from '@/utils/Message'
// import Request from '@/utils/Request'

const app = createApp(App)

for (let iconName in ELIcons) {
    app.component(iconName, ELIcons[iconName])
}

app.use(ElementPlus);
app.use(router)

// 全局方法
// app.config.globalProperties.Message = Message; 
// app.config.globalProperties.Request = Request;

app.mount('#app') 