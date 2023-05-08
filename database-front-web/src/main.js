import { createApp } from 'vue'
import App from './App.vue'
// 引入 element-plus
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
// 引入 router
import router from './router'

const app = createApp(App)

app.use(ElementPlus);
app.use(router)
app.mount('#app') 
