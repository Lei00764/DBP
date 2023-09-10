// 本文件存放与认证申请相关的接口请求函数

import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

// 用户 申请成为专业厨师
// 需要：用户id + 阐述 + 证明材料
export function ApplyProfession(params) {
    return Request({
        method: 'POST',
        url: 'Profession/ApplyForProfession',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            Message.success("申请成功");
            return response.data;
        } else if (response.data.code === 401) {
            Message.error(response.data.msg);
            // Message.error("当前用户已经是专业厨师");
        }
        else {
            Message.error("申请失败");
        }
    }).catch(function (error) {
        console.log(error);
    })
}

//获取待处理的申请信息（管理员）
export function ProfessionToDeal() {
    return Request({
        method: 'GET',
        url: 'Profession/ProfessionToDeal',
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  // 正确响应，返回数据
        } else {
            Message.error("当前没有新的申请");
        }
    }).catch(function (error) {
        console.log(error);
    });
}

//处理专业认证申请
export function DealProfession(params) {
    return Request({
        method: 'Put',
        url: 'Profession/DealRequest',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            router.push({ path: '/checkProfession/1' });
            return response.data;  // 正确响应，返回数据
        } else {
            Message.error("操作失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}