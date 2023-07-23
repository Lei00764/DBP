// 本文件存放与搜索相关的接口请求函数

import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

// 搜索文章
export function searchPost(params) {  // 在 src/views/login/index.vue 里调用，可以去看看是如何调用的
    return Request({  // 发送请求
        method: 'GET',
        url: 'Search/post',  // 与后端接口对应！！！
        params: params
    }).then(function (response) {  // then 表示成功接收到响应后的操作
        if (response.data.code === 200) {
            return response.data;  //  // 正确响应，返回数据
        } else {
            Message.error("没有相关内容");
        }
    }).catch(function (error) {  // catch 表示接收到错误响应后的操作
        console.log(error);
    });
}
