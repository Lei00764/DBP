import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

// 给文章点赞/取消点赞
export function like(params) {
    return Request({
        method: 'POST',
        url: 'Like/Like',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success(response.data.msg);  // 点赞/取消点赞成功
        } else if (response.data.code === 400) {
            Message.error("点赞失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}