using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

    [HttpPost("postComment")]
    public async Task<IActionResult> postCommentAsync(int user_id, int article_id, string content)
    {
        var code = 200;
        var msg = "success";
        if (_database.Users.Any(x => x.UserId == user_id) == false || _database.Articles.Any(x => x.PostId == article_id) == false)
        {
            return BadRequest(new
            {
                code = 400,
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
        msg = "留言成功";
        return Ok(new
        {
            code = code,
            msg = msg,
        });
    }

    [HttpPost("deleteComment")]
    public async Task<IActionResult> deleteCommentAsync(int msg_id)
    {
        var code = 200;
        var msg = "success";
        if (_database.Comments.Any(x => x.MsgId == msg_id) == false)
        {
            return BadRequest(new
            {
                code = 400,
                msg = "留言不存在",
            });
        }
        var c = await _database.Comments.Where(a => a.MsgId == msg_id).ToListAsync();
        _database.Comments.RemoveRange(c); //删除操作
        await _database.SaveChangesAsync();
        msg = "删除成功";
        return Ok(new
        {
            code = code,
            msg = msg,
        });
    }

    [HttpGet("loadComment")]
    public async Task<IActionResult> getCommentByArticleAsync(int article_id)
    {
        var code = 200;
        var msg = "success";
        if (_database.Articles.Any(x => x.PostId == article_id) == false)
        {
            return BadRequest(new
            {
                code = 400,
                msg = "文章不存在",
            });
        }
        var comment_data = _database.Comments.Where(x => x.PostId == article_id).OrderBy(x => x.ReleaseTime).ToList();
        msg = "留言获取成功";
        return Ok(new
        {
            code = code,
            msg = msg,
            data = comment_data
        });
    }
}