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
            return Ok(new
            {
                code = 404,
                msg = "用户不存在",
            });
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

        return Ok(new
        {
            code = 404,
            msg = "用户头像不存在",
        });
    }

    // 前端通过 formdata 上传图片数据
    // 参考：https://www.cnblogs.com/leoxuan/articles/11087121.html
    // 必须要显示指明参数来自 [FromForm] 
    // 去掉 [FromForm] 后，可以在 swagger 上上传图片
    [HttpPost("uploadAvatar")]
    public IActionResult UploadAvatar([FromForm] int userId, [FromForm] IFormFile avatarFile)
    {
        var user = _database.Users.FirstOrDefault(x => x.UserId == userId);
        if (user == null)
        {
            return Ok(new
            {
                code = 404,
                msg = "用户不存在",
            });
        }
        user.Avatar = "images/avatars/" + userId.ToString() + ".jpg";
        _database.SaveChanges();
        string path = "wwwroot/images/avatars/" + userId.ToString() + ".jpg";  // 指定图片存储路径

        // 存储图片
        using (var stream = new FileStream(path, FileMode.Create))
        {
            avatarFile.CopyTo(stream);  // 将图片上传到指定路径
        }

        return Ok(new
        {
            code = 200,
            msg = "用户头像上传成功",
        });
    }
}
