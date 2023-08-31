using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using auth.Database;
using auth.Models;
using Microsoft.AspNetCore.Components.Web;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格

public class AnnouncementController : ControllerBase
{
    private AppDbContext _database;

    public AnnouncementController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    //加载公告
    [HttpGet("loadAnnouncement")]
    public async Task<IActionResult> GetAnnouncementAsync()
    {
        var code = 200;
        var msg = "success";

        var announcements = await (
            from announcement in _database.Announcements
            join admin in _database.Administrators on announcement.AdminId equals admin.AdminId
            select new
            {
                AdminId = announcement.AdminId,
                Title = announcement.Title, //公告标题
                AnnouncementID = announcement.AnnouncementId,
                AnnouncementTime = announcement.AnnouncementTime,
                AnnouncementContent = announcement.AnnouncementContent,
                AdminName = admin.AdminName,
                IsTop = announcement.IsTop
            }
        )
        .OrderByDescending(a => a.IsTop) // 根据置顶状态进行降序排序
        .ToListAsync();
        //.ToListAsync();

        /* 在这分页的话
        int pageSize = 6; // 每页显示的公告数
        int pageNumber; // 要获取的页码

        var paginatedAnnouncements = announcements.Skip((pageNumber - 1) * pageSize) .Take(pageSize) .ToList();*/
        if(announcements.Count >0){
            return Ok(new
            {
                code = code,
                msg = msg,
                data = announcements
            });
        }
        else{
            code = 400;
            msg = "没有公告";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }

    }
    //删除某公告
    [HttpDelete("deleteAnnouncement")]
    public async Task<IActionResult> DeleteAnnouncementAsync(int announcementId)
    {
        var code = 200;
        var msg = "success";

        var announcement = await _database.Announcements.Where(a => a.AnnouncementId == announcementId).ToListAsync();

        if (announcement.Count > 0)
        {
            _database.Announcements.RemoveRange(announcement);
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
    //发布公告
    [HttpPost("postAnnouncement")]
    public async Task<IActionResult> PostAnnouncementAsync(int adminId, string title, string announcementContent)
    {
        var code = 200;
        var msg = "success";

        //var admin = await _database.Administrators.FindAsync(adminId);
        var temp = await _database.Administrators.ToListAsync();
        bool exist = false;
        if (temp != null)//判断表内是否有信息
        {
            foreach (var user in temp)
            {
                if (user.AdminId == adminId)
                {
                    exist = true;
                    break;
                }
            }
        }
        if (exist)
        {
            Announcement newAnnouncement = new Announcement
            {
                //AnnouncementId = 4,
                AnnouncementTime = DateTime.Now,
                Title = title,
                AnnouncementContent = announcementContent,
                AdminId = adminId,
                IsTop = 0 // 默认为0，即未置顶
            };

            _database.Announcements.AddRange(newAnnouncement);
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
            msg = "不存在该管理员信息";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }
    //搜索公告
    [HttpGet("searchAnnouncement")]
    public async Task<IActionResult> SearchAnnouncementsAsync(string keyword)
    {
        var code = 200;
        var msg = "success";

        // 查询文章
        var announcements = await (
            from announcement in _database.Announcements
            join admin in _database.Administrators on announcement.AdminId equals admin.AdminId
            where(announcement.AnnouncementContent != null && announcement.AnnouncementContent.Contains(keyword))
            select new
            {
                AnnouncementID = announcement.AnnouncementId,
                Title = announcement.Title,
                AnnouncementTime = announcement.AnnouncementTime,
                AnnouncementContent = announcement.AnnouncementContent,
                AdminName = admin.AdminName,
                IsTop = announcement.IsTop
            }
        ).ToListAsync();

        if (announcements.Count > 0)
        {
            
            return Ok(new
            {
                code = code,
                msg = msg,
                data = announcements
            });
        }
        else
        {
            code = 400;
            msg = "未找到相关公告";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    
    }
    //修改公告
    [HttpPost("updateAnnouncement")]
    public async Task<IActionResult> UpdateAnnouncementAsync(int announcementId, string title, string content)
    {
        var code = 200;
        var msg = "success";
        var announcement = await _database.Announcements.Where(a => a.AnnouncementId == announcementId).ToListAsync();
        if (announcement.Count > 0)
        {
            foreach (var item in announcement)
            {
                item.AnnouncementContent = content;
                item.Title = title;
            }
            await _database.SaveChangesAsync();
            return Ok(new
            {
                code = code,
                msg = msg,
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
    //置顶与取消置顶
    [HttpPost("topAnnouncement")]
    public async Task<IActionResult> TopAnnouncementAsync(int announcementId,int istop)
    {
        var code = 200;
        var msg = "success";
        var announcement = await _database.Announcements.Where(a => a.AnnouncementId == announcementId).ToListAsync();
        if (announcement.Count > 0)
        {
            foreach (var item in announcement)
            {
                item.IsTop = istop;
            }
            await _database.SaveChangesAsync();
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        else
        {
            code = 400;
            msg = "操作失败";
            return Ok(new
            {
                code = code,
                msg = msg
            });
        }
    }
}