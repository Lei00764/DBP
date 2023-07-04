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
            name: 'login',  // 这个名字可以随便起
            component: () => import('@/views/login/index.vue'),
        },
        {
            path: '/register',
            name: 'register',
            component: () => import('@/views/register/index.vue'),
        },
        {
            path: '/homeUser',  // 用户首页
            name: 'homeUser',
            component: () => import('@/views/homeUser/index.vue'),
        },
        {
            path: '/homeAdmin',  // 管理员首页
            name: 'homeAdmin',
            component: () => import('@/views/homeAdmin/index.vue'),
        },
        {
            path: '/announcement',  // 公告
            name: 'announcement',
            component: () => import('@/views/announcement/index.vue'),
        },
        {
            path: '/forum',  // 论坛
            name: 'forum',
            component: () => import('@/views/forum/index.vue'),
        },
        {
            path: '/userHomePage',  // 个人主页 
            name: 'userHomePage',
            component: () => import('@/views/userHomePage/index.vue'),
        },
        {
            path: '/adminHomePage',  // 管理员主页
            name: 'adminHomePage',
            component: () => import('@/views/adminHomePage/index.vue'),
        }

    ]
})

export default router
