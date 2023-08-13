/*
知识点：
    在 ASP.NET Core 项目中，使用 wwwroot 文件夹作为静态资源根目录
*/

using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using auth.Database;
using auth.Models;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class FilesController : ControllerBase
{

    private AppDbContext _database;

    public FilesController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }
    // TODO : 目前均是返回默认头像，需要改成通过数据库中用户的avatar
    [HttpGet("getAvatar")]
    public IActionResult GetAvatar(int userId)
    {
        try
        {
            if(_database.Users.Any(x=>x.UserId==userId)==false)
            {
                return NotFound("User Not Found");
            }
            var user =_database.Users.Where(x=>x.UserId==userId).ToList().First();
            string? avatarPath =user.Avatar;
            if(avatarPath.Equals("images/avatars/defult.jpg"))
            {
                avatarPath = Path.Combine("wwwroot", "images", "avatars", "default.jpg");  // 即 wwwroot/images/avatars/default.jpg
            }
            else
            {
                avatarPath="wwwroot/"+avatarPath;
            }
            if (System.IO.File.Exists(avatarPath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(avatarPath);  // 读取头像图片的字节数据
                return File(imageBytes, "image/jpeg");
            }
            else
            {
                return NotFound("Avatar not found");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
