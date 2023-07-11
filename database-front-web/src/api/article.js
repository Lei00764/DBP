import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

//查看帖子详情(修改后)
export function getArticleDetail(params) {
    Request({
        method: 'GET',
        params: params,
        url: 'viewArticle/{article_id}'
    }).then(function (response) {
        if (response.data.code === 200) {//返回帖子信息
            //Message.success("成功");
            router.push({ path: '/forum-article-detail' });
        } else if (response.data.code == 404) {
            Message.error("帖子不存在");
        } else if (response.data.code == 400) {
            Message.error("参数无效");
        }
    }).catch(function (error) {
        console.log(error);
    });
}

// 获取文章列表
export function loadArticle(params) {
    //     return Request({
    //         method: 'GET',
    //         url: 'forum/loadArticle',
    //         params: params
    //     }).then(function (response) {
    //         if (response.data.code === 200) {
    //             return response.data.data;
    //         } else {
    //             Message.error("获取文章列表失败");
    //             return null;
    //         }
    //     }).catch(function (error) {
    //         console.log(error);
    //         return null;
    //     });
}
