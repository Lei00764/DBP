using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using auth.Models;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class ProfessionController : ControllerBase
{
    private AppDbContext _database;

    public ProfessionController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    [HttpPost("ApplyForProffesion")]//提交专业认证申请（用户）
     public async Task<IActionResult> ApplyForAsync(int user_id,int year, int month, int day,string illustrate,string evidence)//提交专业认证申请（用户）
     {
        var code = 200;
        var msg = "申请已提交";
        // var request = await _database.Users.ToListAsync();
        bool exist=false;
        // //time=(2020,7,4)
        // if (user_data == null)
        // {
        //     code = 400;
        //     msg = "用户不存在";
        //     return BadRequest(new
        //     {
        //         code = code,
        //         msg = msg,
        //     });
        // }
        
        var u=_database.Users.Where(x=>x.UserId==user_id);
        foreach (var item in u)
        {
            if (item.UserId== user_id)
            {
                exist=true;
                break;
            }
        }
        if(exist==false)
        {
            code = 400;
            msg = "用户不存在";
            return BadRequest(new
            {
                code = code,
                msg = msg,
            });
        }
        // var record= _database.Professions.Where(x=>x.UserId==user_id);
        // foreach (var r in record)
        // {
        //     if (r.UserId == user_id)
        //     {
        //     code = 400;
        //     msg = "请勿重复提交";
        //     return BadRequest(new
        //     {
        //         code = code,
        //         msg = msg,
        //     });
        //     }
        // }
        var newRecord = new Profession()
        {
            UserId = user_id,
            ApplyTime = new System.DateTime(year, month, day),
            Illustrate = illustrate,
            Evidence = evidence,
            IsAccepted = 0
        };
        _database.Professions.AddRange(newRecord);
        await _database.SaveChangesAsync();
        return Ok(new
        {
            code = code,
            msg = msg
        });

    }

    [HttpGet("ViewRequest/{request_id}")]
     public async Task<IActionResult> ViewRequest(int request_id)//查看认证申请（管理员）
     {
        var code = 200;
        var msg = "success";
        bool exist=false;
        string? illustrate="";
        string? evidence="";

        // var r_data=await _database.Professions.ToListAsync();
        // if(r_data==null)
        // {
        //     code = 400;
        //     msg = "用户不存在";
        //     return BadRequest(new
        //     {
        //         code = code,
        //         msg = msg,
        //     });
        // }
        var record= _database.Professions.Where(x=>x.RequestId==request_id);
        foreach (var r in record)
        {
            if (r.RequestId == request_id)
            {
            exist=true;
            illustrate=r.Illustrate;
            evidence=r.Evidence;
            break;
            }
        }
        if(exist==false)
        {
            code = 400;
            msg = "认证申请不存在";
            return BadRequest(new
            {
                code = code,
                msg = msg,
            });            
        }

        return Ok(new
        {
            code = code,
            msg = msg,
            illustrate=illustrate,
            evidence=evidence
        }); 
     }
}