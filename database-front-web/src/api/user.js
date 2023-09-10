// 本文件存放与用户/管理员相关的接口请求函数

import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

// 用户/管理员登录
export function userLogin(params) {  // 在 src/views/login/index.vue 里调用，可以去看看是如何调用的
    return Request({  // 发送请求
        method: 'GET',
        url: 'user/login',  // 与后端接口对应！！！
        params: params
    }).then(function (response) {  // then 表示成功接收到响应后的操作
        if (response.data.code === 200) {
            Message.success("登录成功");
            
            // console.log(response.data); // 检查返回的数据
            return response.data;  //  // 正确响应，返回数据
        } else {
            Message.error("登录失败");
        }
    }).catch(function (error) {  // catch 表示接收到错误响应后的操作
        console.log(error);
    });
}

// 用户/管理员注册
export function userRegister(params) {
    return Request({
        method: 'POST',
        url: 'user/register',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success("注册成功");
            router.push({ path: '/login' });
            return response.data;  // 正确响应，返回数据
        } else {
            Message.error("注册失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}

//通过Email获取用户/管理员信息
export function GetInfoByEmail(params) {
    return Request({
        method: 'GET',
        url: 'user/getInfoByEmail',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  // 正确响应，返回数据
        } else {
            Message.error("信息获取失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}

//通过ID获取用户/管理员信息
export function GetInfoByID(params) {
    return Request({
        method: 'GET',
        url: 'user/InfoByID',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  // 正确响应，返回数据
        } else {
            Message.error("信息获取失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}

// 更改用户积分
export function changePoint(params) {
    return Request({
        method: 'POST',
        url: 'user/changePoint',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success("success");
            return response.data;
        } else {
            Message.error("error");
        }
    }).catch(function (error) {
        console.log(error);
    });

    
}

// 更改用户信息
export function editInfo(params) {
    return Request({
        method: 'POST',
        url: 'user/edit',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success("success");
            return response.data;
        } else {
            Message.error("error");
        }
    }).catch(function (error) {
        console.log(error);
    });

    
}
