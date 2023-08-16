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
            path: '/checkProfession',  // 审核专业认证
            name: 'checkProfession',
            component: () => import('@/views/checkProfession/index.vue'),
        },
        {
            path: '/checkComment',  // 审核被举报留言
            name: 'checkComment',
            component: () => import('@/views/checkComment/index.vue'),
        },
        {
            path: '/announcement',  // 公告
            name: 'announcement',
            component: () => import('@/views/announcement/announcement.vue'),
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
            path: '/addArticle',  // 发布文章
            name: 'addArticle',
            component: () => import('@/views/addArticle/index.vue'),
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
                    path: 'user',
                    name: 'user',
                    component: () => import('@/views/userHomePage/user/change.vue'),
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
