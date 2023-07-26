using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;

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
    public async Task<IActionResult> GetArticleByTagAsync(int p_board_id, int page_num)
    {
        var code = 400;
        var msg = "success";
        // 初始化一个列表
        List<string> tag_list = new List<string>() { "全部", "中餐", "西餐", "甜点", "其他" };
        var tag = tag_list[p_board_id];
        // 根据 tag 的值，筛选不同的文章
        if (tag != "全部" && tag != "中餐" && tag != "西餐" && tag != "甜点" && tag != "其他")
        {
            code = 400;
            msg = "不存在该板块";
            return BadRequest(new
            {
                code = code,
                msg = msg
            });
        }

        code = 200;
        // var page_size = 10;  // 每一页所容纳的数据量


        if (tag == "全部")
        {
            // 虽然此处和下面重复度高，但以后可能会改成推荐
            var article_data = await (
                from article in _database.Articles
                join user in _database.Users on article.AuthorId equals user.UserId
                select new
                {
                    ID = article.PostId,//文章ID
                    TAG = article.Tag,  // 文章标签
                    Title = article.Title,  // 文章标题
                    Views = article.Views,  // 文章浏览量
                    FavouriteNum = article.FavouriteNum,  // 文章收藏量
                    LikeNum = article.LikeNum,  // 文章点赞量
                    AuthorName = user.UserName, // 包含作者的名字
                    Content = article.Content,  // 文章内容
                    IsBanned = article.IsBanned  // 是否被封禁
                }
            ).ToListAsync();

            return Ok(new
            {
                code = code,
                msg = msg,
                data = article_data
            });
        }
        else
        {
            var article_data = await (
                 from article in _database.Articles
                 join user in _database.Users on article.AuthorId equals user.UserId
                 where article.Tag == tag
                 select new
                 {
                     ID = article.PostId,//文章ID
                     TAG = article.Tag,  // 文章标签
                     Title = article.Title,  // 文章标题
                     Views = article.Views,  // 文章浏览量
                     FavouriteNum = article.FavouriteNum,  // 文章收藏量
                     LikeNum = article.LikeNum,  // 文章点赞量
                     AuthorName = user.UserName, // 包含作者的名字
                     Content = article.Content,  // 文章内容
                     IsBanned = article.IsBanned  // 是否被封禁
                 }
             ).ToListAsync();

            return Ok(new
            {
                code = code,
                msg = msg,
                data = article_data
            });
        }
    }


    [HttpGet("viewArticle")]
    public async Task<IActionResult> GetArticleDetailsAsync(int article_id)
    {
        var code = 200;
        var msg = "success";
        var temp = await _database.Articles.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有改文章
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
                        ID = article.PostId,//文章ID
                        TAG = article.Tag,  // 文章标签
                        Title = article.Title,  // 文章标题
                        Views = article.Views,  // 文章浏览量
                        FavouriteNum = article.FavouriteNum,  // 文章收藏量
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
                code = 400;
                msg = "该文章已被封禁";
                return BadRequest(new
                {
                    code = code,
                    msg = msg
                });
            }
            async void UpdateData()
            {
                //需要先查询
                var a = _database.Articles.Where(x => x.PostId == article_id);
                //再对查询到的数据进行修改
                foreach (var item in a)
                {
                    item.Views += 1;
                }
                //再save更改
                await _database.SaveChangesAsync();

            }
            UpdateData();
            return Ok(new
            {
                code = code,
                msg = msg,
                data = article_data,
            });
        }
        else
        {
            code = 400;
            msg = "不存在该文章";
            return BadRequest(new
            {
                code = code,
                msg = msg
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
            return BadRequest(new
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
            return BadRequest(new
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
                ReleaseTime=DateTime.Now
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
            return BadRequest(new
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
            return BadRequest(new
            {
                code = code,
                msg = msg
            });
        }
    }

    // 下面这段代码有问题！！！

    // //论坛页面搜索文章
    // [HttpGet("Article/forum_searchArticle")]
    // public async Task<IActionResult> SearchArticlesAsync(string keyword,int page_num)
    // {
    //     var code = 200;
    //     var msg = "success";

    //     // 查询文章
    //     var articles = await _database.Articles
    //         .Where(a => a.Title.Contains(keyword)) // 在标题中搜索关键词
    //         .ToListAsync();

    //     if (articles.Count > 0) // 有结果的话
    //     {
    //         var articleList = articles.Select(a => new
    //         {
    //             ID = article.PostId,//文章ID
    //             TAG = article.Tag,  // 文章标签
    //             Title = article.Title,  // 文章标题
    //             Views = article.Views,  // 文章浏览量
    //             FavouriteNum = article.FavouriteNum,  // 文章收藏量
    //             LikeNum = article.LikeNum,  // 文章点赞量
    //             AuthorName = user.UserName, // 包含作者的名字
    //             Content = article.Content,  // 文章内容
    //             IsBanned = article.IsBanned
    //         }).ToListAsync();

    //         var totalCount = articleList.Count;
    //         var pageSize = 6;// 暂定一页6篇文章
    //         var pageTotal = (int)Math.Ceiling((double)totalCount / pageSize); // 总页数

    //         var searchArticleList = articleList
    //             .Skip((page_num - 1) * pageSize)   // 跳过前面几页的数据，拿出本页的数据
    //             .Take(pageSize)
    //             .ToListAsync();

    //         var result = new
    //         {
    //             totalCount = totalCount,
    //             pageTotal = pageTotal,
    //             list = searchArticleList
    //         };

    //         return Ok(new
    //         {
    //             code = code,
    //             msg = msg,
    //             data = result
    //         });
    //     }
    //     else
    //     {
    //         code = 400;
    //         msg = "未找到相关文章";
    //         return BadRequest(new
    //         {
    //             code = code,
    //             msg = msg
    //         });
    //     }

    // }

    [HttpGet("forum_searchArticle")]
    public async Task<IActionResult> SearchArticlesAsync(string keyword)
    {
        var code = 200;
        var msg = "success";

        // 查询文章
        var articles = await _database.Articles
            .Where(a => a.Title.Contains(keyword)) // || a.Content.Contains(keyword)) // 在标题和内容中搜索关键词
            .ToListAsync();

        if (articles.Count > 0)
        {
            var articleList = articles.Select(article => new
            {
                ID = article.PostId,//文章ID
                   	    TAG = article.Tag,  // 文章标签
                        Title = article.Title,  // 文章标题
                        Views = article.Views,  // 文章浏览量
                        FavouriteNum = article.FavouriteNum,  // 文章收藏量
                        LikeNum = article.LikeNum,  // 文章点赞量
                        AuthorName = _database.Users.FirstOrDefault(user => user.UserId == article.AuthorId)?.UserName,
                        Content = article.Content,  // 文章内容
                        IsBanned = article.IsBanned  // 是否被封禁
            }).ToList();

        

            return Ok(new
            {
                code = code,
                msg = msg,
                data = articleList
            });
        }
        else
        {
            code = 400;
            msg = "未找到相关文章";
            return BadRequest(new
            {
                code = code,
                msg = msg
            });
        }
    
}


