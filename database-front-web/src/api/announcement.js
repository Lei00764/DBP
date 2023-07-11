import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

//管理员展示公告
export function adminShowAnnouncement(params) {
    //     Request({  // 发送请求
    //         method: 'GET',
    //         url: 'http://127.0.0.1:4523/m1/2699367-0-febb5d0d/api/users/{userId}/announcement',
    //         params: params
    //     }).then(function (response) {
    //         if (response.data.code === 200) {
    //             Message.success("成功显示");
    //         } else if (response.data.code == 404) {
    //             Message.error("目前没有公告");
    //         }
    //     }).catch(function (error) {
    //         console.log(error);
    //     });
}


//管理员发布公告
export function adminPublishAnnouncement(params) {
    //     Request({
    //         method: 'POST',
    //         url: 'http://127.0.0.1:4523/m1/2699367-0-febb5d0d/api/administor/{administorId}/announcement',
    //         params: params
    //     }).then(function (response) {
    //         if (response.data.code === 201) {
    //             Message.success("成功发布");
    //         } else {
    //             Message.error("发布失败");
    //         }
    //     }).catch(function (error) {
    //         console.log(error);
    //     });
}