import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"
import router from "@/router/index.js"

// 用户/管理员登录
export function userLogin(params) {
    Request({  // 发送请求
        method: 'GET',
        url: 'login',
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
        // url: 'register',
        url: 'http://127.0.0.1:4523/m1/2699367-0-febb5d0d/api/register',
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