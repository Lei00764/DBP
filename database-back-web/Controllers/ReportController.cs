using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

    [HttpGet("ReportPostToDeal")]//列出未被处理的被举报的帖子
    public async Task<IActionResult> ReportPostToDeal()
    {
        var code = 200;
        var msg = "success";
        var report_post = await (
            from report in _database.ReportPost
            where report.IsTrue == 0  //未被管理员审核
            select new
            {
                id = report.UserId,//举报者ID
                time = report.Time,  // 举报时间
                postId = report.PostId,  //被举报的帖子ID
                reason = report.Reason,  // 举报原因
                reportId = report.PostReportId  //举报信息ID
            }
            ).ToListAsync();
        if (report_post != null)
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
}