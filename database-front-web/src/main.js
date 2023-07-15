import { createApp } from 'vue'
import App from './App.vue'
// 引入 element-plus
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
// 引入 router
import router from './router'
import * as ELIcons from '@element-plus/icons-vue'//引入图标
import store from './store'  //引入store

import "./assets/icon/iconfont.css"  //图标

<<<<<<< HEAD
import "./assets/icon/iconfont.css"//图标

/* import the fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core'
/* import font awesome icon component */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
/* import specific icons */
import { fas } from '@fortawesome/free-solid-svg-icons'
/* add icons to the library */
library.add(fas)


=======
// 全局方法 
// import Message from '@/utils/Message'
// import Request from '@/utils/Request'
>>>>>>> main

const app = createApp(App)

for (let iconName in ELIcons) {
    app.component(iconName, ELIcons[iconName])
}

app.component('font-awesome-icon', FontAwesomeIcon)

app.use(ElementPlus);
app.use(router);
app.use(store)



app.mount('#app') 