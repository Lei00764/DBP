using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
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

    // lx 修改 2023.7.21
    [HttpPost("ApplyForProfession")] // 提交专业认证申请（用户）
    public async Task<IActionResult> ApplyForProfessionAsync(int user_id, string illustrate, string evidence)//提交专业认证申请（用户）
    {
        var code = 200;
        var msg = "";

        bool existUser = _database.Users.Any(x => x.UserId == user_id);  //  Any() 方法来检查是否存在任何匹配 UserId 的记录
        if (!existUser)
        {
            code = 400;
            msg = "用户不存在";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }

        // 判断该用户是否已经是专业厨师：取 Profession 表查找是否存在 UserId = user_id 且 IsAccepted = 1 的记录
        // IsAccepted 等于 1 表示已经同意申请，用户已经是专业厨师
        // IsAccepted 等于 0 表示正在申请中
        // IsAccepted 等于 2 表示申请被拒绝
        bool existProfessionalChef = _database.Professions.Any(x => x.UserId == user_id && x.IsAccepted == 1);
        if (existProfessionalChef)
        {
            code = 401;
            msg = "该用户已经是专业厨师";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }

        // 创建一个 Profesion() 对象
        var newProfession = new Profession()
        {
            UserId = user_id,
            ApplyTime = DateTime.Now,
            Illustrate = illustrate,
            Evidence = evidence,
            IsAccepted = 0
        };

        _database.Professions.AddRange(newProfession);
        await _database.SaveChangesAsync();
        code = 200;
        msg = "申请成功";
        return Ok(new
        {
            code = code,
            msg = msg
        });
    }

    [HttpGet("ViewRequest/{request_id}")]
    public IActionResult ViewRequest(int request_id)//查看认证申请（管理员）
    {
        var code = 200;
        var msg = "success";
        bool exist = false;
        string? illustrate = "";
        string? evidence = "";
        var record = _database.Professions.Where(x => x.RequestId == request_id);
        foreach (var r in record)
        {
            if (r.RequestId == request_id)
            {
                exist = true;
                illustrate = r.Illustrate;
                evidence = r.Evidence;
                break;
            }
        }
        if (exist == false)
        {
            code = 400;
            msg = "认证申请不存在";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }

        return Ok(new
        {
            code = code,
            msg = msg,
            illustrate = illustrate,
            evidence = evidence
        });
    }

    [HttpGet("ProfessionToDeal")]
    public async Task<IActionResult> ProfessionToDeal()//列出未被处理的认证申请（管理员）
    {
        var code = 200;
        var msg = "success";
        var profession_data = await (
            from request in _database.Professions
            where request.IsAccepted == 0
            select new
            {
                id = request.UserId,//申请者ID
                time = request.ApplyTime,  // 申请时间
                illustrate = request.Illustrate,  //原因
                evidence = request.Evidence,  // 证明材料
                result = request.IsAccepted,  // 处理结果
                requestId = request.RequestId  //申请信息ID
            }
            ).ToListAsync();
        if (profession_data != null)
        {
            return Ok(new
            {
                code = code,
                msg = msg,
                data = profession_data
            });
        }
        else
        {
            return BadRequest(new
            {
                code = 400,
                msg = "当前没有新的专业申请",
            });
        }
    }

    [HttpPut("DealRequest")]
    public async Task<IActionResult> DealRequestAync(int request_id, int response)//处理认证申请（管理员）response=1为通过2为不通过
    {
        var code = 200;
        var msg = "success";
        bool exist = false;
        var record = _database.Professions.Where(x => x.RequestId == request_id);
        int? user_id = 0;
        string? illustrate = "";
        foreach (var r in record)
        {
            if (r.RequestId == request_id)
            {
                exist = true;
                user_id = r.UserId;//获取申请人的ID
                illustrate = r.Illustrate;//获取申请的内容
                break;
            }
        }
        if (exist == false)
        {
            code = 400;
            msg = "认证申请不存在";
            return BadRequest(new
            {
                code = code,
                msg = msg,
            });
        }
        var user = _database.Users.Where(x => x.UserId == user_id);
        if (response == 2)
        {
            foreach (var r in record)
            {
                r.IsAccepted = 2;
                await _database.SaveChangesAsync();
            }
            msg = "申请已拒绝";
        }
        else if (response == 1)
        {
            foreach (var r in record)
            {
                r.IsAccepted = 1;
                await _database.SaveChangesAsync();
            }
            foreach (var u in user)
            {
                u.Professional = illustrate;
                await _database.SaveChangesAsync();
                msg = "申请已通过";
            }
        }
        else
        {
            code = 400;
            msg = "response参数错误";
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
        });
    }
}