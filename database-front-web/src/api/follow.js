import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

//关注/取消关注
export function followAuthor(params) {
    return Request({
        method: 'POST',
        url: 'Follow/Follow',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;
        } else if (response.data.code === 400) {
            Message.error("参数错误");
            return null;
        }else if (response.data.code === 401) {
            Message.error("用户不存在");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    });
}

//判断关注状态
export function isfollowAuthor(params) {
    return Request({
        method: 'GET',
        url: 'Follow/isFollow',
        params: params
    }).then(function (response) {
        return response.data;
    }).catch(function (error) {
        console.log(error);
    });
}

//获取用户粉丝数量
export function getFansNumber(params) {
    return Request({
        method: 'GET',
        url: 'Follow/FansNumber',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data.data;
        } else if (response.data.code === 400) {
            Message.error("参数错误");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    });
}
//获取用户关注数量
export function getFollowNumber(params) {
    return Request({
        method: 'GET',
        url: 'Follow/FollowNumber',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data.data;
        } else if (response.data.code === 400) {
            //Message.error("该用户无关注的用户");
            return response.data.data;
        }
    }).catch(function (error) {
        console.log(error);
    });
}