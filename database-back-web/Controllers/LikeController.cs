using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;
using auth.Utils;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class LikeController : ControllerBase
{
    private AppDbContext _database;

    public LikeController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    [HttpPost("Like")]
    public async Task<IActionResult> LikeArticleAsync(int user_id, int post_id)//点赞或取消点赞
    {
        var user_data = await _database.Users.ToListAsync();
        var article_data = await _database.Articles.ToListAsync();
        if (user_data == null || article_data == null)
        {
            return Ok(new
            {
                code = 400,
                msg = "数据库中没有数据",
            });
        }
        var u = _database.Users.Where(x => x.UserId == user_id).ToList();
        var a = _database.Articles.Where(x => x.PostId == post_id).ToList();
        int? author_id = a.First().AuthorId; // 获取被点赞的作者ID
        // Console.Write(a);
        var record = _database.Likes.Where(x => x.PostId == post_id && x.UserId == user_id);
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
                msg = "用户或文章不存在",
            });
        }

        MyUtil tool = new MyUtil(_database);

        string msg = "";

        foreach (var item in a) // 更改点赞数及点赞记录
        {
            if (r_exist == false) // 未点赞
            {
                msg = "点赞成功";
                item.LikeNum += 1;
                await _database.SaveChangesAsync();
                var newRecord = new Like()
                {
                    UserId = user_id,
                    PostId = post_id
                };
                _database.Likes.AddRange(newRecord);
                await _database.SaveChangesAsync();
                tool.ChangePoints(author_id, 1); // 增加积分
            }
            else // 已点赞
            {
                msg = "取消点赞成功";
                item.LikeNum -= 1;
                await _database.SaveChangesAsync();
                _database.Likes.RemoveRange(record); // 删除记录
                await _database.SaveChangesAsync();
                tool.ChangePoints(author_id, -1); // 减少积分

            }
        }
        return Ok(new
        {
            code = 200,
            msg = msg
        });
    }
}