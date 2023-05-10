import { createApp } from 'vue'
import App from './App.vue'
// 引入 element-plus
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
// 引入 router
import router from './router'

// 全局方法 
import Message from '@/utils/Message'
import Request from '@/utils/Request'


const app = createApp(App)

app.use(ElementPlus);
app.use(router)

app.config.globalProperties.Message = Message;  // 全局方法
app.config.globalProperties.Request = Request;

app.mount('#app') 
