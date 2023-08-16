import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件

// 上传评论
export function postComment(params) {
  return Request({
    method: "POST",
    url: "Comment/postComment",
    params: params
  }).then(function (response) {
    if (response.data.code === 200) {
      Message.success("评论成功");
    }
    else if (response.data.code === 404) {
      Message.error("用户或文章不存在");
    }
  });
}

// 删除指定评论
export function deleteComment(params) {
  return Request({
    method: "POST",
    url: "Comment/deleteComment",
    params: params
  }).then(function (response) {
    if (response.data.code === 200) {
      Message.success("删除成功");
    }
    else if (response.data.code === 404) {
      Message.error("评论不存在");
    }
  });
}

// 加载指定文章的评论
export function loadComment(params) {
  return Request({
    method: "GET",
    url: "Comment/loadComment",
    params: params
  }).then(function (response) {
    if (response.data.code === 200) {
      return response.data;
    }
    else if (response.data.code === 404) {
      Message.error("文章不存在");
    }
  })
}

// 获取留言详情
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
