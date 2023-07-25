using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using auth.Models;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class FollowController : ControllerBase
{
    private AppDbContext _database;

    public FollowController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    //关注或取消关注
    [HttpPost("Follow")]
    public async Task<IActionResult> FollowUserAsync(int user_id, int author_id)
    {
        var code = 200;
        var msg = "success";
        var user_data = await _database.Users.ToListAsync();
        if (user_data == null)
        {
            code = 400;
            msg = "数据库中没有数据";
            return BadRequest(new
            {
                code = code,
                msg = msg,
            });
        }
        var a = _database.Users.Where(x => x.UserId == user_id);
        var b = _database.Users.Where(x => x.UserId == author_id);
        var record = _database.Follows.Where(x => x.UserId == user_id && x.FollowerUserId == author_id);
        bool a_exist = false;
        bool b_exist = false;
        bool r_exist = false;
        foreach (var user in user_data)
        {
            if (user.UserId == user_id)
            {
                a_exist = true;
                break;
            }
        }
        foreach (var user in user_data)
        {
            if (user.UserId == author_id)
            {
                b_exist = true;
                break;
            }
        }
        foreach (var r in record)
        {
            if (r.UserId == user_id && r.FollowerUserId == author_id)
            {
                r_exist = true;
                break;
            }
        }
        if (a_exist == false || b_exist == false)
        {
            code = 400;
            msg = "用户不存在";
            return BadRequest(new
            {
                code = code,
                msg = msg,
            });
        }
        foreach (var item in a)//更改粉丝数及关注记录
        {
            if (r_exist == false)//未关注
            {
                msg = "关注成功";
                item.FollowerNum += 1;
                await _database.SaveChangesAsync();
                var newRecord = new Follow()
                {
                    UserId = user_id,
                    FollowerUserId = author_id
                };
                _database.Follows.AddRange(newRecord);
                await _database.SaveChangesAsync();
            }
            else//已关注
            {
                msg = "取消关注成功";
                item.FollowerNum -= 1;
                await _database.SaveChangesAsync();
                _database.Follows.RemoveRange(record);//删除记录
                await _database.SaveChangesAsync();
            }
        }
        return Ok(new
        {
            code = code,
            msg = msg
        });
    }

    //获取粉丝数量
    [HttpGet("FansNumber")]
    public async Task<IActionResult> FansNumberAsync(int user_id)
    {
        var code = 200;
        var msg = "success";
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
        if(exist){
            //查找用户的粉丝数量并返回
            var fans_data = _database.Users.Where(a => a.UserId == user_id).Select(a => a.FollowerNum);
            return Ok(new
            {
                code = code,
                msg = msg,
                data = fans_data
            });
        }
        else{
            code = 400;
            msg = "不存在该用户信息";
            return BadRequest(new
            {
                code = code,
                msg = msg
            });
        }
    }

    //获取关注数量
    [HttpGet("FollowNumber")]
    public async Task<IActionResult> FollowNumberAsync(int user_id)
    {
        var code = 200;
        var msg = "success";
        var temp = await _database.Follows.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有该用户为关注者的信息
        {
            foreach (var Follower in temp)
            {
                if (Follower.FollowerUserId == user_id)
                {
                    exist = true;
                    break;
                }
            }
        }
        if(exist){
            //查找用户的粉丝数量并返回
            var follow_num = _database.Follows.Count(a => a.FollowerUserId == user_id);
            return Ok(new
            {
                code = code,
                msg = msg,
                data = follow_num
            });
        }
        else{
            code = 400;
            msg = "该用户没有关注他人";
            return Ok(new
            {
                code = code,
                msg = msg,
                data = 0  //
            });
        }
    }

}