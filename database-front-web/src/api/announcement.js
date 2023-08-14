import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"



// 加载公告
export function loadAnnouncement() {
    return Request({
        method: 'GET',
        url: 'Announcement/loadAnnouncement',
        params: {}  // 空对象表示不传递任何参数
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  // 返回 code + msg + data
        } else {
            Message.error("公告加载失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}
// 删除公告
export function deleteAnnouncement(params) {
    return Request({
        method: 'Delete',
        url: 'Announcement/deleteAnnouncement',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  
        } else {
            Message.error("公告删除失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}
// 发布公告
export function postAnnouncement(params) {
    return Request({
        method: 'Post',
        url: 'Announcement/postAnnouncement',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  
        } else {
            Message.error("公告发布失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}
// 搜索公告
export function searchAnnouncement(params) {
    return Request({
        method: 'Get',
        url: 'Announcement/searchAnnouncement',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  
        } else {
            Message.error("公告搜索失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}
// 修改公告
export function updateAnnouncement(params) {
    return Request({
        method: 'Post',
        url: 'Announcement/updateAnnouncement',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  
        } else {
            Message.error("公告修改失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}
// 置顶公告
export function topAnnouncement(params) {
    return Request({
        method: 'Post',
        url: 'Announcement/topAnnouncement',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  
        } else {
            Message.error("公告置顶失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}


















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