using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class CommentController : ControllerBase
{
    private AppDbContext _database;

    public CommentController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    // 上传一条评论 modify by Xiang Lei 2023.8.13
    [HttpPost("postComment")]
    public async Task<IActionResult> PostComment(int user_id, int article_id, string content)
    {
        if (_database.Users.Any(x => x.UserId == user_id) == false || _database.Articles.Any(x => x.PostId == article_id) == false)
        {
            return NotFound(new
            {
                code = 404,
                msg = "用户或文章不存在",
            });
        }

        Comment newRecord = new Comment
        {
            UserId = user_id,
            PostId = article_id,
            Content = content,
            ReleaseTime = DateTime.Now,
            IsBanned = 0,
        };

        _database.Comments.AddRange(newRecord);
        await _database.SaveChangesAsync();

        return Ok(new
        {
            code = 200,
            msg = "留言成功",
        });
    }

    // 删除指定评论 modify by Xiang Lei 2023.8.13
    [HttpPost("deleteComment")]
    public async Task<IActionResult> DeleteCommentByMsgId(int msg_id)
    {
        if (_database.Comments.Any(x => x.MsgId == msg_id) == false)
        {
            return NotFound(new
            {
                code = 404,
                msg = "留言不存在",
            });
        }

        var c = await _database.Comments.Where(a => a.MsgId == msg_id).ToListAsync();

        _database.Comments.RemoveRange(c); //删除操作
        await _database.SaveChangesAsync();

        return Ok(new
        {
            code = 200,
            msg = "删除成功",
        });
    }

    // 加载指定文章的评论 modify by Xiang Lei 2023.8.13
    [HttpGet("loadComment")]
    public async Task<IActionResult> GetCommentByArticleId(int article_id)
    {
        if (_database.Articles.Any(x => x.PostId == article_id) == false)
        {
            return NotFound(new
            {
                code = 404,
                msg = "文章不存在",
            });
        }

        var comment_data = await _database.Comments
             .Where(x => x.PostId == article_id && x.IsBanned == 0) // 去掉被封禁的留言
             .OrderBy(x => x.ReleaseTime)
             .ToListAsync();

        return Ok(new
        {
            code = 200,
            msg = "留言获取成功(注：留言为空会返回 [])",
            data = comment_data
        });
    }

    [HttpGet("viewComment")]
    public async Task<IActionResult> GetCommentDetailsAsync(int msg_id)
    {
        var code = 200;
        var msg = "success";
        var temp = await _database.Comments.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有该留言
        {
            foreach (var comment in temp)
            {
                if (comment.MsgId == msg_id)
                {
                    exist = true;
                    break;
                }
            }
        }
        if (exist)
        {
            var comment_data = await _database.Comments
                .Where(a => a.MsgId == msg_id)
                .Join(_database.Users,
                    comment => comment.UserId,
                    user => user.UserId,
                    (comment, user) => new
                    {
                        ID = comment.MsgId, //留言ID
                        AuthorName = user.UserName, // 包含留言者的名字
                        AuthorId = user.UserId, // 包含留言者的ID
                        AuthorAvatar = user.Avatar, // 包含留言者的头像
                        Content = comment.Content,  // 留言内容
                        IsBanned = comment.IsBanned
                    })
                .ToListAsync();

            if (comment_data[0].IsBanned != 0)
            {
                code = 400;
                msg = "该留言已被封禁";
                return BadRequest(new
                {
                    code = code,
                    msg = msg
                });
            }

            return Ok(new
            {
                code = code,
                msg = msg,
                data = comment_data,
            });
        }
        else
        {
            code = 400;
            msg = "不存在该留言";
            return BadRequest(new
            {
                code = code,
                msg = msg
            });
        }
    }
}