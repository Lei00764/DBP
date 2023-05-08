import { createRouter, createWebHistory } from 'vue-router'


const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: "login",  // 这个名字可以随便起
            component: () => import('@/views/Login.vue'),
        }
    ]
})

export default router
