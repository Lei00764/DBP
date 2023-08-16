import Request from "@/utils/Request.js";  // 在每个 api 文件里都要引入这两个文件
import Message from "@/utils/Message.js"  // 在每个 api 文件里都要引入这两个文件
import router from "@/router/index.js"

// 加载文章
export function loadArticle(params) {
    return Request({
        method: 'GET',
        url: 'Article/loadArticle',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;  // 返回 code + msg + data
        } else {
            Message.error("文章加载失败");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}

// 获取用户的文章列表
export function searchArticle(params) {
    return Request({
        method: 'GET',
        url: 'Article/Article/search',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;
        } else if (response.data.code === 404) {
            Message.error("没有发布的文章");
            return null;
        } else if (response.data.code === 400) {
            Message.error("参数无效");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    });
}

// navTop 根据关键词去搜索文章列表
export function searchArticles(params) {
    return Request({
        method: 'GET',
        url: 'Article/searchArticle',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;
        }
    }).catch(function (error) {
        console.log(error);
    });
}


//删除文章
export function deleteArticle(params) {
    return Request({
        method: 'DELETE',
        url: 'Article/deleteArticle',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;
        } else if (response.data.code === 400) {
            Message.error("参数错误");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    })
}


// 获取文章详情
// modify by Xiang Lei 2023.8.16
export function GetArticleDetailsAsync(params) {
    return Request({
        method: 'GET',
        url: 'Article/viewArticle',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;
        } else if (response.data.code === 404) {
            Message.error("帖子已被封禁");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
        return null;
    });
}


// 获取文章列表
export function getArticle(params) {
    // return Request({
    //     method: 'GET',
    //     url: 'forum/loadArticle',
    //     params: params
    // }).then(function (response) {
    //     if (response.data.code === 200) {
    //         return response.data.data;
    //     } else {
    //         Message.error("获取文章列表失败");
    //         return null;
    //     }
    // }).catch(function (error) {
    //     console.log(error);
    //     return null;
    // });
}

// 获取用户的文章数量
export function getArticleNumber(params) {
    return Request({
        method: 'GET',
        url: 'Article/ArticleNumber',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data.data;
        } else if (response.data.code === 400) {
            Message.error("该用户未发布文章");
            return response.data.data;
        }
    }).catch(function (error) {
        console.log(error);
    });
}

//修改文章（传参：文章id+修改的标题+内容）
export function editArticle(params) {
    return Request({
        method: 'POST',
        url: 'Article/updateArticle',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;
        } else if (response.data.code === 400) {
            Message.error("参数错误");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    });
}

//发布文章
export function postArticle(params) {
    return Request({
        method: 'POST',
        url: 'Article/postArticle',
        params: params
    }).then(function (response) {
        if (response.data.code === 200) {
            return response.data;
        } else if (response.data.code === 400) {
            Message.error("参数错误");
            return null;
        }
    }).catch(function (error) {
        console.log(error);
    });
}
