import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"



// 加载信息
export function loadNotice() {
    return Request({
        method: 'GET',
        url: 'Notice/loadNotice',
        params: {}  // 空对象表示不传递任何参数
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  // 返回 code + msg + data
        } else {
            Message.error("信息加载失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}

// 发送信息
export function postNotice(params) {
    return Request({
        method: 'Post',
        url: 'Notice/postNotice',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  
        } else {
            Message.error("信息发送失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}

// 删除信息
export function deleteNotice(params) {
    return Request({
        method: 'Delete',
        url: 'Notice/deleteNotice',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  
        } else {
            Message.error("信息删除失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}
