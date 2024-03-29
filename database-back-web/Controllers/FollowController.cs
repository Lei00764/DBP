using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;
using auth.Utils;

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
        MyUtil tool = new MyUtil(_database);
        if (user_data == null)
        {
            code = 400;
            msg = "数据库中没有数据";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        //userid是关注者，author是被关注者
        var a = _database.Users.Where(x => x.UserId == user_id);
        var b = _database.Users.Where(x => x.UserId == author_id);
        //改了这里
        var record = _database.Follows.Where(x => x.UserId == author_id && x.FollowerUserId == user_id);
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
            //改了这里
            if (r.UserId == author_id && r.FollowerUserId == user_id)
            {
                r_exist = true;
                break;
            }
        }
        if (a_exist == false || b_exist == false)
        {
            code = 401;
            msg = "用户不存在";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        foreach (var item in a)//更改关注的人的数量
        {
            if (r_exist == false)//未关注
            {
                item.FollowNum += 1;
            }
            else{ //已关注
                item.FollowNum -= 1;
            }
        }
        foreach (var item in b)//更改粉丝数及关注记录
        {
            if (r_exist == false)//未关注
            {
                msg = "关注成功";
                item.FollowerNum += 1;
                await _database.SaveChangesAsync();
                var newRecord = new Follow()
                {
                    //还有这里
                    UserId = author_id,
                    FollowerUserId = user_id
                };
                _database.Follows.AddRange(newRecord);
                await _database.SaveChangesAsync();
                tool.ChangePoints(author_id,3);
            }
            else//已关注
            {
                msg = "取消关注成功";
                item.FollowerNum -= 1;
                await _database.SaveChangesAsync();
                _database.Follows.RemoveRange(record);//删除记录
                await _database.SaveChangesAsync();
                tool.ChangePoints(author_id,-3);
            }
        }
        return Ok(new
        {
            code = code,
            msg = msg
        });
    }

    //判断关注状态
    [HttpGet("isFollow")]
    public IActionResult isFollowAsync(int user_id, int author_id)
    {
        var code = 200;
        var msg = "success";
        var a = _database.Users.Where(x => x.UserId == user_id);
        var b = _database.Users.Where(x => x.UserId == author_id);
        var record = _database.Follows.Where(x => x.UserId == author_id && x.FollowerUserId == user_id);
        bool r_exist = false;
        foreach (var r in record)
        {
            if (r.UserId == author_id && r.FollowerUserId == user_id)
            {
                r_exist = true;
                return Ok(new
                {
                    data = r_exist,
                    code = code,
                    msg = msg,
                });
            }
        }
        return Ok(new
        {
            data = r_exist,
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
            //var fans_data = _database.Users.Where(a => a.UserId == user_id).Select(a => a.FollowerNum);
            //！！！数据库 用户表中粉丝数量信息与关注表中的数量不一致 ！！！
            var fans_num = _database.Follows.Count(a => a.UserId == user_id);
            return Ok(new
            {
                code = code,
                msg = msg,
                data = fans_num
            });
        }
        else{
            code = 400;
            msg = "不存在该用户信息";
            return Ok(new
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

    //获取关注列表

    //获取粉丝列表

}