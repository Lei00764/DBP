import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

//获取留言详情
export function GetCommentDetailsAsync(params) {
    return Request({
      method: 'GET',
      url: 'Comment/viewComment',
      params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;
        } else {
            Message.error("未搜索到结果");
            return null;
        }
    }).catch(function (error) {
      console.log(error);
      return null;
    });
}
