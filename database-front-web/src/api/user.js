import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"

// 用户登录
export function userLogin(params) {
    Request({  // 发送请求
        method: 'GET',
        url: 'login',
        params: params
    }).then(function (response) {
        if (response.data.code == 200) {
            Message.success("登录成功");
            // router.push({ path: '/home' });  // 路由跳转，将页面跳转到路径为 '/home' 的页面
        } else {
            Message.error("登录失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}