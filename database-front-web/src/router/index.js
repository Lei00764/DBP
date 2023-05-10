import { createRouter, createWebHistory } from 'vue-router'


const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            redirect: '/login'  // 重定向
        },
        {
            path: '/login',
            name: "login",  // 这个名字可以随便起
            component: () => import('@/views/login/index.vue'),
        }
    ]
})

export default router
