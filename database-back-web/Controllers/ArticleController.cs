using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;
using Microsoft.AspNetCore.Http.HttpResults;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class ArticleController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
{
    private AppDbContext _database;

    public ArticleController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    [HttpGet("loadArticle")]
    public async Task<IActionResult> GetArticleByTagAsync(int p_board_id, int page_num, int page_size)
    {//page_num为页码从1开始，page_size为每页的文章数
        var code = 400;
        var msg = "success";
        // 初始化一个列表
        List<string> tag_list = new List<string>() { "全部", "中餐", "西餐", "甜点", "其他" };
        if (p_board_id > 4)
        {
            code = 400;
            msg = "不存在该板块";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
        var tag = tag_list[p_board_id];

        code = 200;
        if (tag == "全部")
        {
            var data = await _database
                .Articles
                .Where(x => x.IsBanned == 0)
                .OrderByDescending(x => x.PostId)
                .ToListAsync();

            data = data.Skip((page_num - 1) * page_size).Take(page_size).ToList();//截取第page_num页的数据
            if (data.Count() == 0)
            {
                msg = "数据不足";
            }
            return Ok(new
            {
                code = code,
                msg = msg,
                data = data
            });
        }
        else//按Tag取
        {
            var data = await _database
                .Articles
                .OrderByDescending(x => x.PostId)
                .Where(x => x.Tag == tag_list[p_board_id] && x.IsBanned == 0)
                .ToListAsync();
            data = data.Skip((page_num - 1) * page_size).Take(page_size).ToList();//截取第page_num页的数据
            if (data.Count() == 0)
            {
                msg = "数据不足";
            }
            return Ok(new
            {
                code = code,
                msg = msg,
                data = data
            });
        }
    }


    // modify by Xiang Lei 2023.8.16
    [HttpGet("viewArticle")]
    public async Task<IActionResult> GetArticleDetails(int article_id)
    {
        var temp = await _database.Articles.ToListAsync();
        bool exist = false;
        if (temp != null) // 判断表内是否有改文章
        {
            foreach (var article in temp)
            {
                if (article.PostId == article_id)
                {
                    exist = true;
                    break;
                }
            }
        }
        if (exist)
        {
            var article_data = await _database.Articles
                .Where(a => a.PostId == article_id)
                .Join(_database.Users,
                    article => article.AuthorId,
                    user => user.UserId,
                    (article, user) => new
                    {
                        ID = article.PostId, //文章ID
                        TAG = article.Tag,  // 文章标签
                        Title = article.Title,  // 文章标题
                        Views = article.Views,  // 文章浏览量
                        FavouriteNum = article.FavouriteNum, // 文章收藏量
                        LikeNum = article.LikeNum,  // 文章点赞量
                        AuthorName = user.UserName, // 包含作者的名字
                        AuthorId = user.UserId, // 包含作者的ID
                        AuthorAvatar = user.Avatar, // 包含作者的ID
                        Content = article.Content,  // 文章内容
                        IsBanned = article.IsBanned,
                        ReleaseTime = article.ReleaseTime
                    })
                .ToListAsync();

            if (article_data[0].IsBanned != 0)
            {
                return Ok(new
                {
                    code = 404,
                    msg = "帖子已被封禁"
                });
            }
            async void UpdateData()
            {
                // 需要先查询
                var a = _database.Articles.Where(x => x.PostId == article_id);
                // 再对查询到的数据进行修改
                foreach (var item in a)
                {
                    item.Views += 1;
                }
                // 再save更改
                await _database.SaveChangesAsync();

            }
            UpdateData();
            return Ok(new
            {
                code = 200,
                msg = "success",
                data = article_data,
            });
        }
        else
        {
            return Ok(new
            {
                code = 404,
                msg = "帖子不存在"
            });
        }
    }

    //删除文章
    [HttpDelete("deleteArticle/post_id")]
    public async Task<IActionResult> DeleteArticleAsync(int post_id)
    {
        var code = 200;
        var msg = "success";
        var temp = await _database.Articles.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有该文章
        {
            foreach (var article in temp)
            {
                if (article.PostId == post_id)
                {
                    exist = true;
                    break;
                }
            }
        }
        if (exist)
        {
            var article_data = await _database.Articles.Where(a => a.PostId == post_id).ToListAsync();
            _database.Articles.RemoveRange(article_data); //删除操作
            await _database.SaveChangesAsync();
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        else
        {
            code = 400;
            msg = "不存在该文章";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }

    //修改文章
    [HttpPost("updateArticle/post_id")]
    public async Task<IActionResult> UpdateArticleAsync(int post_id, string title, string content)
    {
        var code = 200;
        var msg = "success";
        var temp = await _database.Articles.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有该文章
        {
            foreach (var article in temp)
            {
                if (article.PostId == post_id)
                {
                    exist = true;
                    break;
                }
            }
        }
        if (exist)
        {
            var article_data = _database.Articles.Where(a => a.PostId == post_id);
            foreach (var item in article_data)
            {
                item.Title = title;
                item.Content = content;
            }
            await _database.SaveChangesAsync();
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        else
        {
            code = 400;
            msg = "不存在该文章";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }

    //发布文章  
    [HttpPost("postArticle")]
    public async Task<IActionResult> postArticleAsync(int user_id, string tag, string title, string content, string picture, string Sharelink)
    {
        var code = 200;
        var msg = "success";
        //!!!!!TODO: 文章ID、分享链接应该怎么赋值？还要避免与表中已有文章数据重复
        //user_id 要满足完整性约束
        var temp = await _database.Users.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有信息
        {
            foreach (var user in temp)
            {
                if (user.UserId == user_id)
                {
                    exist = true;
                    break;
                }
            }
        }
        if (exist)
        {
            Article newRecord = new Article
            {
                Tag = tag,
                Title = title,
                Content = content,
                AuthorId = user_id,
                //PostId = post_id,
                ShareLink = Sharelink,
                Views = 0,
                FavouriteNum = 0,
                LikeNum = 0,
                IsBanned = 0,
                ReleaseTime = DateTime.Now
            };

            _database.Articles.AddRange(newRecord);
            await _database.SaveChangesAsync();//注意：该语句与数据库更改语句一一匹配

            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
        else
        {
            code = 400;
            msg = "不存在该用户信息";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }


    //查找文章信息
    [HttpGet("Article/search")]
    public async Task<IActionResult> searchArticleAsync(int user_id)
    {
        var code = 200;
        var msg = "success";
        var temp = await _database.Articles.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有该文章
        {
            foreach (var article in temp)
            {
                if (article.AuthorId == user_id)
                {
                    exist = true;
                    break;
                }
            }
        }
        if (exist)
        {
            //查找用户写的所有文章并返回
            var article_data = _database.Articles.Where(a => a.AuthorId == user_id);
            return Ok(new
            {
                code = code,
                msg = msg,
                data = article_data
            });
        }
        else
        {
            code = 400;
            msg = "该用户无发布的文章";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }

    //论坛界面 搜索某文章
    [HttpGet("forum_searchArticle")]
    public async Task<IActionResult> SearchArticlesAsync(string keyword)
    {
        var code = 200;
        var msg = "success";

        // 查询文章
        var articles = await (
            from article in _database.Articles
            join user in _database.Users on article.AuthorId equals user.UserId
            where (article.Title != null && article.Title.Contains(keyword)) ||
            (article.Content != null && article.Content.Contains(keyword))
            select new
            {
                ID = article.PostId,
                TAG = article.Tag,
                Title = article.Title,
                Views = article.Views,
                FavouriteNum = article.FavouriteNum,
                LikeNum = article.LikeNum,
                AuthorName = user.UserName,
                Content = article.Content,
                IsBanned = article.IsBanned
            }
        ).ToListAsync();

        if (articles.Count > 0)
        {

            //             var articleList = articles.Select(article => new
            //             {
            //                 ID = article.PostId,//文章ID
            //                    	    TAG = article.Tag,  // 文章标签
            //                         Title = article.Title,  // 文章标题
            //                         Views = article.Views,  // 文章浏览量
            //                         FavouriteNum = article.FavouriteNum,  // 文章收藏量
            //                         LikeNum = article.LikeNum,  // 文章点赞量
            //                         AuthorName = _database.Users.FirstOrDefault(user => user.UserId == article.AuthorId)?.UserName,
            //                         Content = article.Content,  // 文章内容
            //                         IsBanned = article.IsBanned  // 是否被封禁
            //             }).ToList();




            return Ok(new
            {
                code = code,
                msg = msg,
                data = articles
            });
        }
        else
        {
            code = 400;
            msg = "未找到相关文章";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }

    }
    //获取文章数量
    [HttpGet("ArticleNumber")]
    public async Task<IActionResult> ArticleNumberAsync(int user_id)
    {
        var code = 200;
        var msg = "success";
        var temp = await _database.Articles.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有该用户的文章
        {
            foreach (var article in temp)
            {
                if (article.AuthorId == user_id)
                {
                    exist = true;
                    break;
                }
            }
        }
        if (exist)
        {
            //查找用户的文章数量并返回
            var article_num = _database.Articles.Count(a => a.AuthorId == user_id);
            return Ok(new
            {
                code = code,
                msg = msg,
                data = article_num
            });
        }
        else
        {
            code = 400;
            msg = "该用户没有发布的文章";
            return Ok(new
            {
                code = code,
                msg = msg,
                data = 0  //考虑用户的参数无效问题
            });
        }
    }
}




