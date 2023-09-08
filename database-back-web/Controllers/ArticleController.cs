using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

    private string GetSummary(string content)
    {
        content = Regex.Replace(content, "<[^>]+>", "");
        const int MaxSummaryLength = 30; // 设置文章概要的最大长度为5
        var summary = string.Empty;
        if (!string.IsNullOrEmpty(content))
        {
            var paragraphs = content.Split("\n");
            foreach (var paragraph in paragraphs)
            {
                if (summary.Length + paragraph.Length <= MaxSummaryLength)
                {
                    summary += paragraph;
                }
                else
                {
                    summary += paragraph.Substring(0, MaxSummaryLength - summary.Length);
                    break;
                }
            }
            if (summary.Length == MaxSummaryLength)
            {
                summary += "...";
            }
        }
        return summary;
    }


    [HttpGet("recommendArticle")]
    public async Task<IActionResult> GetArticleIndividually(int user_id)
    {
        if (_database.Likes.Any(x => x.UserId == user_id) == false)
        {
            return Ok(new
            {
                code = 400,
                msg = "成功获取个性化推荐文章",
                data = await _database
                    .Articles
                    .Where(x => x.IsBanned == 0)
                    .OrderByDescending(x => x.PostId)
                    .ToListAsync()
            });
        }
        double[] priority = new double[4] { 0, 0, 0, 0 };//对应 "中餐", "西餐", "甜点", "其他"
        var like_data = await _database.Likes.Where(x => x.UserId == user_id).ToListAsync();
        int[] sum = new int[4] { 0, 0, 0, 0 };
        sum[0] = _database.Articles.Where(x => x.Tag == "中餐").Count();
        sum[1] = _database.Articles.Where(x => x.Tag == "西餐").Count();
        sum[2] = _database.Articles.Where(x => x.Tag == "甜点").Count();
        sum[3] = _database.Articles.Where(x => x.Tag == "其他").Count();
        foreach (var r in like_data)
        {
            var article = _database.Articles.Where(x => x.PostId == r.PostId).First();
            if (article.Tag == "中餐")
            {
                priority[0] += 1;
            }
            else if (article.Tag == "西餐")
            {
                priority[1] += 1;
            }
            else if (article.Tag == "甜点")
            {
                priority[2] += 1;
            }
            else if (article.Tag == "其他")
            {
                priority[3] += 1;
            }
        }
        double total = priority.Sum();
        for (int i = 0; i < 4; i++)
        {
            priority[i] /= total;
            priority[i] *= 10;
            if (priority[i] < 1)
            {
                priority[i] = 1;
            }
            else
            {//做四舍五入
                int temp = Convert.ToInt32(priority[i]);
                if (priority[i] - temp < 0.5)
                {
                    priority[i] = temp;
                }
                else
                {
                    priority[i] = temp + 1;
                }
            }
        }
        int diff = 10 - Convert.ToInt32(priority.Sum());
        if (diff != 0)
        {
            int index = 0;
            for (int i = 1; i < 4; i++)
            {
                if (priority[i] > priority[index])
                {
                    index = i;
                }
            }
            priority[index] += diff;
        }
        int min = 999999999;
        for (int i = 0; i < 4; i++)
        {
            if (sum[i] * 10 / priority[i] < min)
            {
                min = Convert.ToInt32(sum[i] * 10 / priority[i]);
            }
        }
        min = min / 10 * 10;
        int[] num = new int[4];
        for (int i = 0; i < 4; i++)
        {
            num[i] = Convert.ToInt32(priority[i]) * min / 10;
        }
        List<Article> data0 = await _database
            .Articles
            .Where(x => x.IsBanned == 0 && x.Tag == "中餐")
            .OrderByDescending(x => x.PostId)
            .ToListAsync();
        data0 = data0.Take(num[0]).ToList();
        List<Article> data1 = await _database
            .Articles
            .Where(x => x.IsBanned == 0 && x.Tag == "西餐")
            .OrderByDescending(x => x.PostId)
            .ToListAsync();
        data1 = data1.Take(num[1]).ToList();
        List<Article> data2 = await _database
            .Articles
            .Where(x => x.IsBanned == 0 && x.Tag == "甜点")
            .OrderByDescending(x => x.PostId)
            .ToListAsync();
        data2 = data2.Take(num[2]).ToList();
        List<Article> data3 = await _database
            .Articles
            .Where(x => x.IsBanned == 0 && x.Tag == "其他")
            .OrderByDescending(x => x.PostId)
            .ToListAsync();
        data3 = data3.Take(num[3]).ToList();
        //拼接四个列表
        data0 = data0.Concat(data1).ToList();
        data0 = data0.Concat(data2).ToList();
        data0 = data0.Concat(data3).ToList();
        //打乱列表顺序
        var random = new Random();
        List<Article> data = new List<Article>();
        foreach (var item in data0)
        {
            data.Insert(random.Next(data.Count), item);
        }
        return Ok(new
        {
            code = 200,
            msg = "成功获取个性化推荐文章",
            data = data
        });
    }

    [HttpGet("loadArticle")]
    public async Task<IActionResult> GetArticleByTagAsync(int p_board_id)
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
            var data = await (
                from article in _database.Articles
                join user in _database.Users on article.AuthorId equals user.UserId
                select new
                {
                    postId = article.PostId,//文章ID
                    TAG = article.Tag,  // 文章标签
                    AuthorId = article.AuthorId, //文章作者ID
                    Title = article.Title,  // 文章标题
                    Views = article.Views,  // 文章浏览量
                    FavouriteNum = article.FavouriteNum,  // 文章收藏量
                    LikeNum = article.LikeNum,  // 文章点赞量
                    AuthorName = user.UserName, // 包含作者的名字
                    Avatar = user.Avatar, //作者头像
                    Content = article.Content,  // 文章内容
                    IsBanned = article.IsBanned,  // 是否被封禁
                    ReleaseTime = article.ReleaseTime, //文章发布时间
                    Picture = article.Picture, //文章内的图片
                    IsTop = article.IsTop  //文章是否置顶
                }
            ).Where(x => x.IsBanned == 0).OrderByDescending(x => x.postId).ToListAsync();
            /*var data = await _database
                .Articles
                .Where(x => x.IsBanned == 0)
                .OrderByDescending(x => x.PostId)
                .ToListAsync();*/

            //data = data.Skip((page_num - 1) * page_size).Take(page_size).ToList();//截取第page_num页的数据
            var summarizedData = data.Select(x => new
            {
                x.postId,
                x.Title,
                x.TAG,
                x.AuthorId,
                x.Views,
                x.FavouriteNum,
                x.LikeNum,
                x.Content,
                x.AuthorName,
                x.Avatar,
                ReleaseTime = x.ReleaseTime != null ? x.ReleaseTime.Value.ToString("yyyy-MM-dd") : null,
                x.Picture,
                x.IsTop,
                Summary = GetSummary(x.Content) // 获取文章概要
            });
            if (data.Count() == 0)
            {
                msg = "数据不足";
            }
            return Ok(new
            {
                code = code,
                msg = msg,
                data = summarizedData
            });
        }
        else//按Tag取
        {
            var data = await (
                from article in _database.Articles
                join user in _database.Users on article.AuthorId equals user.UserId
                select new
                {
                    postId = article.PostId,//文章ID
                    TAG = article.Tag,  // 文章标签
                    Title = article.Title,  // 文章标题
                    AuthorId = article.AuthorId, //文章作者ID
                    Views = article.Views,  // 文章浏览量
                    FavouriteNum = article.FavouriteNum,  // 文章收藏量
                    LikeNum = article.LikeNum,  // 文章点赞量
                    AuthorName = user.UserName, // 包含作者的名字
                    Avatar = user.Avatar,
                    Content = article.Content,  // 文章内容
                    IsBanned = article.IsBanned,  // 是否被封禁
                    ReleaseTime = article.ReleaseTime,
                    Picture = article.Picture,
                    IsTop = article.IsTop  //文章是否置顶
                }
            ).Where(x => x.TAG == tag_list[p_board_id] && x.IsBanned == 0).OrderByDescending(x => x.postId).ToListAsync();
            /*var data = await _database
                .Articles
                .OrderByDescending(x => x.PostId)
                .Where(x => x.Tag == tag_list[p_board_id] && x.IsBanned == 0)
                .ToListAsync();*/
            //data = data.Skip((page_num - 1) * page_size).Take(page_size).ToList();//截取第page_num页的数据
            var summarizedData = data.Select(x => new
            {
                x.postId,
                x.Title,
                x.TAG,
                x.AuthorId,
                x.Views,
                x.FavouriteNum,
                x.LikeNum,
                x.Content,
                x.AuthorName,
                x.Avatar,
                ReleaseTime = x.ReleaseTime != null ? x.ReleaseTime.Value.ToString("yyyy-MM-dd") : null,
                x.Picture,
                x.IsTop,
                Summary = GetSummary(x.Content) // 获取文章概要
            });
            if (data.Count() == 0)
            {
                msg = "数据不足";
            }
            return Ok(new
            {
                code = code,
                msg = msg,
                data = summarizedData
            });
        }
    }


    // modify by Xiang Lei 2023.8.16 （没详细改）
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
    [HttpPost("deleteArticle")]
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
                msg = "删除成功",
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
    [HttpPost("updateArticle")]
    public async Task<IActionResult> UpdateArticleAsync(int post_id, string title, string content)
    {
        var code = 200;
        var msg = "success";
        if(content.Length>3500)
        {
            return Ok(new
            {
                code = 400,
                msg = "字数过多，无法上传",
            });
        }
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
                msg = "更新成功"
            });
        }
    }

    public class ArticleRequestModel
    {
        public int user_id { get; set; }
        public string? tag { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public string? picture { get; set; }
        public string? Sharelink { get; set; }
    }

    //发布文章  
    [HttpPost("postArticle")]
    public async Task<IActionResult> postArticle([FromBody] ArticleRequestModel model)
    {
        // !!!!!TODO: 文章ID、分享链接应该怎么赋值？还要避免与表中已有文章数据重复
        // user_id 要满足完整性约束
        var temp = await _database.Users.ToListAsync();
        bool exist = false;
        if(model.content!=null&&model.content.Length>3500)
        {
            return Ok(new
            {
                code = 400,
                msg = "字数过多，无法上传",
            });
        }
        if (temp != null) // 判断表内是否有信息
        {
            foreach (var user in temp)
            {
                if (user.UserId == model.user_id)
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
                Tag = model.tag,
                Title = model.title,
                Content = model.content,
                AuthorId = model.user_id,
                //PostId = post_id,
                ShareLink = model.Sharelink,
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
                code = 200,
                msg = "发布成功",
            });
        }
        else
        {
            return Ok(new
            {
                code = 400,
                msg = "不存在该用户信息"
            });
        }
    }


    //查找文章信息
    [HttpGet("search")]
    public async Task<IActionResult> searchArticleAsync(int user_id)
    {
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
                code = 200,
                msg = "查找成功",
                data = article_data
            });
        }
        else
        {
            return Ok(new
            {
                code = 400,
                msg = "该用户无发布的文章"
            });
        }
    }

    // 根据关键词去搜索文章列表
    [HttpGet("searchArticle")]
    public async Task<IActionResult> SearchArticle(string keyword)
    {
        // 查询文章
        var articles = await (
            from article in _database.Articles
            join user in _database.Users on article.AuthorId equals user.UserId
            where (article.Title != null && article.Title.Contains(keyword)) ||
            (article.Content != null && article.Content.Contains(keyword))
            select new
            {
                PostId = article.PostId,
                Tag = article.Tag,
                Title = article.Title,
                Views = article.Views,
                FavouriteNum = article.FavouriteNum,
                LikeNum = article.LikeNum,
                AuthorName = user.UserName,
                Content = article.Content,
                IsBanned = article.IsBanned,
                ReleaseTime = article.ReleaseTime,
                AuthorId = article.AuthorId,
            }
        ).ToListAsync();

        var SearchData = articles.Select(x => new
        {
            x.PostId,
            x.Title,
            x.Tag,
            x.AuthorId,
            x.Views,
            x.FavouriteNum,
            x.LikeNum,
            x.Content,
            x.AuthorName,
            x.IsBanned,
            ReleaseTime = x.ReleaseTime != null ? x.ReleaseTime.Value.ToString("yyyy-MM-dd") : null,
            Summary = GetSummary(x.Content) // 获取文章概要
        });

        if (articles.Count > 0)
        {
            return Ok(new
            {
                code = 200,
                msg = "success",
                data = SearchData
            });
        }
        else
        {
            return Ok(new  // 没找到文章也算正常
            {
                code = 200,
                msg = "未找到相关文章",
                data = new List<object>()
            });
        }
    }

    // 文章浏览量+1
    [HttpPost("Articleview")]
    public async Task<IActionResult> ArticleView(int post_id)
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
                item.Views = item.Views + 1;
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
    
[HttpGet("GetLikeNum")]//获取文章点赞量
public async Task<IActionResult> GetLikeNumber(int article_id)
{
    if(await _database.Articles.AnyAsync(x=>x.PostId==article_id)==false)
    {
        return Ok(new
        {
            code = 400,
            msg = "文章不存在",
        });
    }
    Article a=_database.Articles.Where(x=>x.PostId==article_id).First();
    return Ok(new
        {
            code = 200,
            msg = "已获取文章点赞量",
            likeNum=a.LikeNum
        });
}

[HttpGet("GetFavouriteNum")]//获取文章点赞量
public async Task<IActionResult> GetFavouriteNumber(int article_id)
{
    if(await _database.Articles.AnyAsync(x=>x.PostId==article_id)==false)
    {
        return Ok(new
        {
            code = 400,
            msg = "文章不存在",
        });
    }
    Article a=_database.Articles.Where(x=>x.PostId==article_id).First();
    return Ok(new
        {
            code = 200,
            msg = "已获取文章收藏量",
            likeNum=a.FavouriteNum
        });
}
//置顶与取消置顶
    [HttpPost("topArticle")]
    public async Task<IActionResult> TopArticle(int articleId,int istop)
    {
        var code = 200;
        var msg = "success";
        if(istop!=0&&istop!=1)
        {
            return Ok(new
            {
                code = 400,
                msg = "istop只能为0或1",
            });
        }
        else if(istop==0)
        {
            msg="已取消置顶";
        }
        else if(istop==1)
        {
            msg="已置顶";
        }
        var article = await _database.Articles.Where(a => a.PostId == articleId).ToListAsync();
        if (article.Count > 0)
        {
            foreach (var item in article)
            {
                item.IsTop = istop;
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
            msg = "文章不存在";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }
}



