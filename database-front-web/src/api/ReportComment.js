// 本文件存放与举报留言信息相关的接口请求函数

import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

//获取待处理的举报留言信息（管理员）
export function ReportCommentToDeal() {
    return Request({
        method: 'GET',
        url: 'ReportComment/ReportCommentToDeal',
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  // 正确响应，返回数据
        } else {
            Message.error("当前没有新的举报");
        }
    }).catch(function (error) {
        console.log(error);
    });
}

//处理举报留言信息（管理员）
export function DealMsgReportAync(params){
    return Request({
        method: 'PUT',
        url: 'ReportComment/DealMsgReport',
        params: params,
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  // 正确响应，返回数据
        } else {
            Message.error("操作失败");
        }
    }).catch(function (error) {
        console.log(error);
    });
}