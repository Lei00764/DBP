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

    //加载信息
    [HttpGet("loadNotice")]
    public async Task<IActionResult> GetNoticeAsync()
    {
        var code = 200;
        var msg = "success";

        var notices = await (
            from notice in _database.Notices
            join admin in _database.Administrators on notice.AdminId equals admin.AdminId
            join user in _database.Users on notice.UserId equals user.UserId
            select new
            {
                NoticeID = notice.NoticeId,
                NoticeTime = notice.NoticeTime,
                NoticeContent = notice.NoticeContent,
                AdminName = admin.AdminName,
            }
        ).ToListAsync();

        /* 在这分页的话
        int pageSize = 6; // 每页显示的公告数
        int pageNumber; // 要获取的页码

        var paginatedNotices = notices.Skip((pageNumber - 1) * pageSize) .Take(pageSize) .ToList();*/
        if(notices.Count >0){
            return Ok(new
            {
                code = code,
                msg = msg,
                data = notices
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