import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"
import router from "@/router/index.js"

// 用户/管理员登录
export function userLogin(params) {
    Request({  // 发送请求
        method: 'GET',
        url: 'user/login',
        // url: 'http://127.0.0.1:4523/m1/2699367-0-febb5d0d/api/login',  // mock
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success("登录成功");
            if (response.data.type === 1) {
                router.push({ path: '/homeUser' });  // 路由跳转，将页面跳转到路径为 '/homeUser' 的页面
            } else {
                router.push({ path: '/homeAdmin' });
            }
        } else {
            Message.error("登录失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}


// 用户/管理员注册
export function userRegister(params) {
    Request({
        method: 'POST',
        url: 'user/register',
        // url: 'http://127.0.0.1:4523/m1/2699367-0-febb5d0d/api/register',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success("注册成功");
            router.push({ path: '/login' });
        } else {
            Message.error("注册失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}


//查看帖子详情
export function getArticleDetail(params) {
    Request({
        method: 'GET',
        url: 'http://127.0.0.1:4523/m1/2699367-0-febb5d0d/api/show',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {//返回帖子信息
            //Message.success("成功");
            router.push({ path: '/forum-article-detail' });
        } else if (response.data.code == 404) {
            Message.error("帖子不存在");
        } else if (response.data.code == 400) {
            Message.error("参数无效");
        }
    }).catch(function (error) {
        console.log(error);
    });
}





//管理员展示公告
export function adminShowAnnouncement(params) {
    Request({  // 发送请求
        method: 'GET',
        url: 'http://127.0.0.1:4523/m1/2699367-0-febb5d0d/api/users/{userId}/announcement',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success("成功显示");
        } else if (response.data.code == 404) {
            Message.error("目前没有公告");
        }
    }).catch(function (error) {
        console.log(error);
    });
}


//管理员发布公告
export function adminPublishAnnouncement(params) {
    Request({
        method: 'POST',
        url: 'http://127.0.0.1:4523/m1/2699367-0-febb5d0d/api/administor/{administorId}/announcement',
        params: params
    }).then(function (response) {
        if (response.data.code === 201) {
            Message.success("成功发布");
        } else {
            Message.error("发布失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}
