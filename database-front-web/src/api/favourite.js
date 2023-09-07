import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

// 收藏/取消收藏文章
export function favourite(params) {
    return Request({
        method: 'POST',
        url: 'favourite/favourite',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success(response.data.msg);  // 收藏/取消收藏成功
        } else if (response.data.code === 400) {
            Message.error("收藏失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}