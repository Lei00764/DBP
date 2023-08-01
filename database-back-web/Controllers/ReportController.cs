using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class ReportController : ControllerBase
{
    private AppDbContext _database;

    public ReportController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }


    [HttpPost("PostReport")]//提交举报
    public async Task<IActionResult> ReportArticleAsync(int user_id,int article_id,string reason)
    {
       var code = 200;
       var msg = "success"; 
       if(_database.Users.Any(x=>x.UserId==user_id)==false||_database.Articles.Any(x=>x.PostId==article_id)==false)
       {
         return BadRequest(new
            {
                code = 400,
                msg = "举报者或被举报文章不存在",
            });
       }
       ReportPost newRecord =new ReportPost{
            UserId=user_id,
            PostId=article_id,
            Reason=reason,
            Time=DateTime.Now,
            IsTrue=0,
       };
        _database.ReportPost.AddRange(newRecord);
        await _database.SaveChangesAsync();
        msg="提交成功";
       return Ok(new
        {
            code = code,
            msg = msg,
        });   
    }


    [HttpGet("ReportPostToDeal")]//列出未被处理的被举报的帖子
    public async Task<IActionResult> ReportPostToDeal()
    {
        var code = 200;
        var msg = "success";
        var report_post = await (
            from report in _database.ReportPost
            join article in _database.Articles on report.PostId equals article.PostId
            where report.IsTrue == 0 && article.IsBanned == 0 //未被管理员审核且文章未被封禁
            select new
            {
                id = report.UserId,//举报者ID
                time = report.Time,  // 举报时间
                postId = report.PostId,  //被举报的帖子ID
                reason = report.Reason,  // 举报原因
                reportId = report.PostReportId  //举报信息ID
            }
            ).ToListAsync();
        if (report_post.Count > 0)
        {
            return Ok(new
            {
                code = code,
                msg = msg,
                data = report_post
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

    [HttpPut("DealReport")]
    public async Task<IActionResult> DealReportAync(int report_id, int adminId, int is_true, string result)//处理举报信息（管理员）is_true=1为通过2为不通过
    {
        var code = 200;
        var msg = "success";
        bool exist = false;
        var record = _database.ReportPost.Where(x => x.PostReportId == report_id);
        int? post_id = 0;
        foreach (var r in record)
        {
            if (r.PostReportId == report_id)
            {
                exist = true;
                post_id = r.PostId;
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
        var article = _database.Articles.Where(x => x.PostId == post_id);//找到该文章
        if (is_true == 2)//未通过
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
            foreach (var a in article)
            {
                a.IsBanned = 1;
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