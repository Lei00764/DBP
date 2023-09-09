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
            path: '/layout',
            name: 'layout',
            component: () => import('@/views/layout.vue'),
            children: [
                {
                    path: '',  // 默认显示的子路由页面
                    name: '所有文章',
                    component: () => import('@/views/forum/articleList.vue'),
                },
                {
                    path: '/forum/:pBoardId',  // pBoardId 从 0 开始 （动态路由）
                    name: "一级板块",
                    component: () => import('@/views/forum/articleList.vue'),
                }
            ]
        },
        {
            path: '/Search',  // 搜索页面
            name: 'Search',
            component: () => import('@/views/Search.vue'),
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
            path: '/checkProfession/:choice',  // 审核专业认证
            name: 'work',
            component: () => import('@/views/checkProfession/index.vue'),
        },
        {
            path: '/announcementAdmin',  // 管理员界面的公告栏
            name: 'announcementAdmin',
            component: () => import('@/views/announcementAdmin/announcementAdmin.vue'),
        },
        {
            path: '/announcementUser',  // 用户界面的公告栏
            name: 'announcementUser',
            component: () => import('@/views/announcementUser/announcementUser.vue'),
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
            path: '/addArticle',  // 发布文章
            name: 'addArticle',
            component: () => import('@/views/addArticle/index.vue'),
        },
        {
            path: '/editArticle/:articleId/',  // 修改文章
            name: 'editArticle',
            component: () => import('@/views/editArticle/index.vue'),
        },
        {
            path: '/forumArticleDetail/:articleId/',  // 论坛帖子详情页
            name: 'forumArticleDetail',
            component: () => import('@/views/forumArticleDetail/forumArticleDetail.vue'),
        },
        {
            path: '/reportArticle',  // 论坛帖子举报页
            name: 'reportArticle',
            component: () => import('@/views/reportArticle/index.vue'),
        },
        {
            path: '/userHomePage',  // 个人主页 
            name: 'userHomePage',
            children: [
                {
                    path: '',
                    name: 'homo',
                    component: () => import('@/views/userHomePage/home/index.vue'),
                },
                {
                    path: '',
                    name: 'theme',
                    component: () => import('@/views/userHomePage/theme/themeSwitching.vue'),
                },
            ]
        },
        // {
        //     path: '/adminHomePage',  // 管理员主页
        //     name: 'adminHomePage',
        //     component: () => import('@/views/adminHomePage/index.vue'),
        // },
    ]
})

export default router
