// 本文件存放与文章相关的接口请求函数

import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

// 获取用户的文章列表
export function searchArticle(params) {
    return Request({
        method: 'GET',
        url: 'Article/Article/search',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data.data;
        } else if(response.data.code === 404){
            Message.error("没有发布的文章");
            return null;
        }else if(response.data.code === 400){
            Message.error("参数无效");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    });
}