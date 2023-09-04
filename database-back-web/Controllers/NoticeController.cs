using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class NoticeController : ControllerBase
{
    private AppDbContext _database;

    public NoticeController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }
    private string GetSummary(string content)
    {
        const int MaxSummaryLength = 5; // 设置文章概要的最大长度为5
        var summary = string.Empty;
        if (!string.IsNullOrEmpty(content))
        {
            var paragraphs = content.Split("\n\n"); // 假设每段之间是用两个换行符（\n\n）分隔的
            foreach (var paragraph in paragraphs)
            {
                if (summary.Length + paragraph.Length <= MaxSummaryLength)
                {
                    summary += paragraph;
                }
                else
                {
                    summary += paragraph.Substring(0, MaxSummaryLength - summary.Length);
                    break;
                }
            }
            if(summary.Length == MaxSummaryLength)
            {
                summary += "...";
            }
        }
        return summary;
   }


    //加载信息
    [HttpGet("loadNotice")]
    public async Task<IActionResult> GetNoticeAsync(int user_id)
    {
        var code = 200;
        var msg = "success";
        var number = 5;

        var notices = await (
            from notice in _database.Notices
            join admin in _database.Administrators on notice.AdminId equals admin.AdminId
            join user in _database.Users on notice.UserId equals user.UserId
            select new
            {
                AdminId = admin.AdminId,
                UserId = notice.UserId,
                NoticeID = notice.NoticeId,
                NoticeTime = notice.NoticeTime,
                NoticeContent = notice.NoticeContent,
                AdminName = admin.AdminName,
                
            }
        ).Where(x => x.UserId == user_id).OrderByDescending(x => x.NoticeID).ToListAsync();

        /* 在这分页的话
        int pageSize = 6; // 每页显示的公告数
        int pageNumber; // 要获取的页码
        
        var paginatedNotices = notices.Skip((pageNumber - 1) * pageSize) .Take(pageSize) .ToList();*/
        notices = notices.Take(number).ToList();

        var summarizedData = notices.Select(x => new {
                x.AdminId,
                x.UserId,
                x.NoticeID,
                x.NoticeTime,
                x.NoticeContent,
                x.AdminName,
                summary = GetSummary(x.NoticeContent) // 获取文章概要
            });
        if(notices.Count >0){
            return Ok(new
            {
                code = code,
                msg = msg,
                data = summarizedData
            });
        }
        else{
            code = 400;
            msg = "没有信息";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }

    }

    //发送信息
    [HttpPost("postNotice")]
    public async Task<IActionResult> PostNoticeAsync(int adminId, int userId, string noticeContent)
    {
        var code = 200;
        var msg = "已成功发送";

        if (_database.Administrators.Any(x=>x.AdminId==adminId)==true&&_database.Users.Any(x=>x.UserId==userId))
        {
            Notice newNotice = new Notice
            {
                NoticeTime = DateTime.Now,
                NoticeContent = noticeContent,
                AdminId = adminId,
                UserId = userId,
            };

            _database.Notices.AddRange(newNotice);
            await _database.SaveChangesAsync();

            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
        else
        {
            code = 400;
            msg = "管理员或用户信息不存在";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }

    //删除某信息
    [HttpDelete("deleteNotice")]
    public async Task<IActionResult> DeleteNoticeAsync(int noticeId)
    {
        var code = 200;
        var msg = "已成功删除";

        var notice = await _database.Notices.Where(a => a.NoticeId == noticeId).ToListAsync();

        if(_database.Notices.Any(x=>x.NoticeId==noticeId)==true)
        {
            _database.Notices.RemoveRange(notice);
            await _database.SaveChangesAsync();
             return Ok(new
            {
                code = code,
                msg = msg
            });
        }
        else
        {
            code = 400;
            msg = "不存在该公告";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }

}