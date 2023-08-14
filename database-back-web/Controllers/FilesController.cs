/*
知识点：
    在 ASP.NET Core 项目中，使用 wwwroot 文件夹作为静态资源根目录
*/

using Microsoft.AspNetCore.Mvc;

using auth.Database;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class FilesController : ControllerBase
{
    private AppDbContext _database;

    public FilesController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }
    // 返回用户头像
    [HttpGet("getAvatar")]
    public IActionResult GetAvatar(int userId)
    {
        var user = _database.Users.FirstOrDefault(x => x.UserId == userId);
        if (user == null)
        {
            return NotFound("用户不存在");
        }

        string? avatarPath = user.Avatar;

        string DefaultAvatarPath = Path.Combine("wwwroot", "images", "avatars", "default.jpg");

        if (!string.IsNullOrEmpty(avatarPath))
        {
            if (avatarPath.Equals(DefaultAvatarPath))
            {
                avatarPath = Path.Combine("wwwroot", DefaultAvatarPath);
            }
            else
            {
                avatarPath = Path.Combine("wwwroot", avatarPath);
            }

            if (System.IO.File.Exists(avatarPath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(avatarPath);
                return File(imageBytes, "image/jpeg");
            }
        }

        return NotFound("用户头像不存在");
    }
}
