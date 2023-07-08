import { createRouter, createWebHashHistory } from 'vue-router'


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
            path: '/announcementDetail',  // 公告详情页
            name: 'announcementDetail',
            component: () => import('@/views/announcementDetail/announcementDetail.vue'),
        },
        {
            path: '/addAnnouncement',  // 添加公告页
            name: 'addAnnouncement',
            component: () => import('@/views/addAnnouncement/addAnnouncement.vue'),
        },
        {
            path: '/checkArticle',  // 公告
            name: 'checkArticle',
            component: () => import('@/views/checkArticle/index.vue'),
        },
        {
            // 动态路由
            path: '/forum/:tag',  // 论坛
            name: 'forum',
            component: () => import("@/views/forum/index.vue"),
        },
        {
            path: '/forumArticleDetail',  // 论坛帖子详情页
            name: 'forumArticleDetail',
            component: () => import('@/views/forumArticleDetail/forumArticleDetail.vue'),
        },
        {
            path: '/userHomePage',  // 个人主页 
            name: 'userHomePage',
            children: [
                {
                  path:'',
                  name:'homo',
                  component: () => import('@/views/userHomePage/home/index.vue'),
                  children:[{
                    path:'',
                    name:'article',
                    component: () => import('@/views/userHomePage/home/ArticleList.vue'),

                  }]
                },
                {
                    path:'user',
                    name:'user',
                    component: () => import('@/views/userHomePage/user/change.vue'),
                },                    
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
