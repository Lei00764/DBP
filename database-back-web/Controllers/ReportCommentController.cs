using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using auth.Models;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class ReportCommentController : ControllerBase
{
    private AppDbContext _database;

    public ReportCommentController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }


    [HttpPost("CommentReport")]//提交举报
    public async Task<IActionResult> ReportCommentAsync(int user_id,int msg_id,string reason)
    {
       var code = 200;
       var msg = "success"; 
       if(_database.Users.Any(x=>x.UserId==user_id)==false||_database.Comments.Any(x=>x.MsgId==msg_id)==false)
       {
         return BadRequest(new
            {
                code = 400,
                msg = "举报者或被举报留言不存在",
            });
       }
       ReportComments newRecord =new ReportComments{
            UserId=user_id,
            MsgId=msg_id,
            Reason=reason,
            Time=DateTime.Now,
            IsTrue=0,
       };
        _database.ReportComments.AddRange(newRecord);
        await _database.SaveChangesAsync();
        msg="提交成功";
       return Ok(new
        {
            code = code,
            msg = msg,
        });   
    }


    [HttpGet("ReportCommentToDeal")]//列出未被处理的被举报的留言
    public async Task<IActionResult> ReportCommentToDeal()
    {
        var code = 200;
        var msg = "success";
        var report_comment = await (
            from report in _database.ReportComments
            join comment in _database.Comments on report.MsgId equals comment.MsgId
            where report.IsTrue == 0 && comment.IsBanned == 0 //未被管理员审核且留言未被封禁
            select new
            {
                id = report.UserId,//举报者ID
                time = report.Time,  // 举报时间
                msgId = report.MsgId,  //被举报的留言ID
                reason = report.Reason,  // 举报原因
                msgReportId = report.MsgReportId  //举报信息ID
            }
            ).ToListAsync();
        if (report_comment.Count > 0)
        {
            return Ok(new
            {
                code = code,
                msg = msg,
                data = report_comment
            });
        }
        else
        {
            return BadRequest(new
            {
                code = 400,
                msg = "当前没有新的举报信息",
            });
        }
    }

    [HttpPut("DealMsgReport")]//处理举报
    public async Task<IActionResult> DealMsgReportAync(int report_id, int adminId, int is_true, string result)//处理举报留言（管理员）is_true=1为通过2为不通过
    {
        var code = 200;
        var msg = "success";
        bool exist = false;
        var record = _database.ReportComments.Where(x => x.MsgReportId == report_id);
        int? msg_id = 0;
        foreach (var r in record)
        {
            if (r.MsgReportId == report_id)
            {
                exist = true;
                msg_id = r.MsgId;
                break;
            }
        }
        if (exist == false)
        {
            code = 400;
            msg = "该举报信息不存在";
            return BadRequest(new
            {
                code = code,
                msg = msg,
            });
        }
        var comment = _database.Comments.Where(x => x.MsgId == msg_id);//找到该留言
        if (is_true == 2)//审核未通过
        {
            foreach (var r in record)
            {
                r.IsTrue = 2;
                r.AdminId = adminId;
                r.Result = result;
                await _database.SaveChangesAsync();
            }
            msg = "举报未通过";
        }
        else if (is_true == 1)
        {
            foreach (var r in record)
            {
                r.IsTrue = 1;
                r.AdminId = adminId;
                r.Result = result;
                await _database.SaveChangesAsync();
            }
            foreach (var c in comment)
            {
                c.IsBanned = 1;
                await _database.SaveChangesAsync();
                msg = "举报已通过";
            }
        }
        else
        {
            code = 400;
            msg = "is_true参数错误";
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