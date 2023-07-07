import { createRouter, createWebHashHistory } from 'vue-router'

import Others from '@/views/forum/others/index.vue'

const router = createRouter({
    history: createWebHashHistory(),
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
            path: '/checkProfession',  // 审核专业认证
            name: 'checkProfession',
            component: () => import('@/views/checkProfession/index.vue'),
        },
        {
            path: '/announcement',  // 公告
            name: 'announcement',
            component: () => import('@/views/announcement/index.vue'),
        },
        {
            path: '/checkArticle',  // 公告
            name: 'checkArticle',
            component: () => import('@/views/checkArticle/index.vue'),
        },
        {
            path: '/forum',  // 论坛
            name: 'forum',
            // redirect: '/forum/chinese_food',  // 重定向
            // component: () => import('@/views/forum/index.vue'),  // 不能要
            children: [  // children里面的path 不要加 / ，加了就表示是根目录下的路由
                {
                    path: 'chineseFood',
                    name: 'chineseFood',
                    component: () => import('@/views/forum/chineseFood/index.vue'),
                },
                {
                    path: 'westernFood',
                    name: 'westernFood',
                    component: () => import('@/views/forum/westernFood/index.vue'),
                },
                {
                    path: 'dessert',
                    name: 'dessert',
                    component: () => import('@/views/forum/dessert/index.vue'),
                },
                {
                    path: 'others',
                    name: 'others',
                    component: Others
                }
            ]
        },
        {
            path: '/userHomePage',  // 个人主页 
            name: 'userHomePage',
            children: [
                {
                  path:'',
                  component: () => import('@/views/userHomePage/home/index.vue'),
                },
                {
                    path:'',
                    component: () => import('@/views/userHomePage/user/change.vue'),
                }
                    
            ]
        },
        {
            path: '/adminHomePage',  // 管理员主页
            name: 'adminHomePage',
            component: () => import('@/views/adminHomePage/index.vue'),
        }

    ]
})

export default router
