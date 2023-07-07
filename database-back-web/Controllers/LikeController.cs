using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using auth.Models;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class LikeController :ControllerBase
{
    private AppDbContext _database;

    public LikeController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    [HttpPost("Like")]
    public async Task<IActionResult> LikeArticleAsync(int user_id,int post_id)//点赞或取消点赞
    {
        var code = 200;
        var msg = "success";
        var user_data=await _database.Users.ToListAsync();
        var article_data=await _database.Articles.ToListAsync();
        if(user_data==null||article_data==null)
        {
            code=400;
            msg="数据库中没有数据";
            return BadRequest(new{
                code=code,
                msg=msg,
            });
        }
        var u = _database.Users.Where(x => x.UserId == user_id);
        var a = _database.Articles.Where(x => x.PostId == post_id);
        //Console.Write(a);
        var record=_database.Likes.Where(x => x.PostId == post_id&&x.UserId==user_id);
        bool u_exist=false;
        bool a_exist=false;
        bool r_exist=false;
        foreach(var user in user_data)
        {
            if(user.UserId==user_id)
            {
                u_exist=true;
                break; 
            }
        }
        foreach(var article in article_data)
        {
            if(article.PostId==post_id)
            {
                a_exist=true;
                break; 
            }
        }
        foreach(var r in record)
        {
            if(r.PostId==post_id&&r.UserId==user_id)
            {
                r_exist=true;
                break; 
            }
        }
        if(a_exist==false||u_exist==false)
        {
            code=400;
            msg="用户或文章不存在";
            return BadRequest(new{
                code=code,
                msg=msg,
            });
        }
        foreach(var item in a)//更改点赞数及点赞记录
        {
            if(r_exist==false)//未点赞
            {
                item.LikeNum+=1;
                await _database.SaveChangesAsync();
                var newRecord= new Like()
                {
                    UserId=user_id,
                    PostId=post_id
                };
                _database.Likes.AddRange(newRecord);
                await _database.SaveChangesAsync();
            }
            else//已点赞
            {
                item.LikeNum-=1;
                await _database.SaveChangesAsync();
                _database.Likes.RemoveRange(record);//删除记录
                await _database.SaveChangesAsync();
            }
        }
        

        return Ok(new
        {
            code=code,
            msg=msg
        });
    }
}