using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;
using auth.Utils;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class FavouriteController : ControllerBase
{
    private AppDbContext _database;

    public FavouriteController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    [HttpPost("Favourite")]
    public async Task<IActionResult> FavouriteArticle(int user_id, int post_id) // 收藏/取消收藏文章
    {
        var user_data = await _database.Users.ToListAsync();
        var article_data = await _database.Articles.ToListAsync();
        if (user_data == null || article_data == null)
        {

            return Ok(new
            {
                code = 400,
                msg = "数据库中没有数据"
            });
        }
        var u = _database.Users.Where(x => x.UserId == user_id);
        var a = _database.Articles.Where(x => x.PostId == post_id).ToList();
        int? author_id = a.First().AuthorId; // 获取被收藏的作者ID
        // Console.Write(a);
        var record = _database.Favourites.Where(x => x.PostId == post_id && x.UserId == user_id);
        bool u_exist = false;
        bool a_exist = false;
        bool r_exist = false;
        foreach (var user in user_data)
        {
            if (user.UserId == user_id)
            {
                u_exist = true;
                break;
            }
        }

        MyUtil tool = new MyUtil(_database);

        string msg = "";

        foreach (var article in article_data)
        {
            if (article.PostId == post_id)
            {
                a_exist = true;
                break;
            }
        }
        foreach (var r in record)
        {
            if (r.PostId == post_id && r.UserId == user_id)
            {
                r_exist = true;
                break;
            }
        }
        if (a_exist == false || u_exist == false)
        {
            return Ok(new
            {
                code = 400,
                msg = "用户或文章不存在"
            });
        }
        foreach (var item in a) // 更改收藏数及收藏记录
        {
            if (r_exist == false) // 未收藏
            {
                msg = "收藏成功";
                item.FavouriteNum += 1;
                await _database.SaveChangesAsync();
                var newRecord = new Favourite()
                {
                    UserId = user_id,
                    PostId = post_id
                };
                _database.Favourites.AddRange(newRecord);
                await _database.SaveChangesAsync();

                tool.ChangePoints(author_id, 2);
            }
            else // 已收藏
            {
                msg = "取消收藏成功";
                item.FavouriteNum -= 1;
                await _database.SaveChangesAsync();
                _database.Favourites.RemoveRange(record); // 删除记录
                await _database.SaveChangesAsync();
                tool.ChangePoints(author_id, -2);
            }
        }

        return Ok(new
        {
            code = 200,
            msg = msg
        });
    }

    [HttpGet("FavouriteList")]
    public async Task<IActionResult> GetFavouriteListByUserAsync(int user_id)
    {
        var code = 200;
        var msg = "success";
        var user_data = await _database.Users.ToListAsync();
        bool u_exist = false;
        foreach (var user in user_data)
        {
            if (user.UserId == user_id)
            {
                u_exist = true;
                break;
            }
        }
        if (u_exist == false)
        {
            msg = "用户不存在";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        var article_data = await (
            from article in _database.Articles
            join favourite in _database.Favourites on article.PostId equals favourite.PostId
            where favourite.UserId == user_id
            select new
            {
                ID = article.PostId,//文章ID
                TAG = article.Tag,  // 文章标签
                Title = article.Title,  // 文章标题
                Views = article.Views,  // 文章浏览量
                FavouriteNum = article.FavouriteNum,  // 文章收藏量
                LikeNum = article.LikeNum,  // 文章点赞量
                //AuthorName = user.UserName, // 包含作者的名字
                Content = article.Content,  // 文章内容
                IsBanned = article.IsBanned  // 是否被封禁
            }
        ).ToListAsync();

        if (article_data.Count == 0)
        {
            msg = "收藏夹为空";
        }
        return Ok(new
        {
            code = code,
            msg = msg,
            data = article_data
        });
    }
}