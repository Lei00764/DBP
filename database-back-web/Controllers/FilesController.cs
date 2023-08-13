/*
知识点：
    在 ASP.NET Core 项目中，使用 wwwroot 文件夹作为静态资源根目录
*/

using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class FilesController : ControllerBase
{
    // TODO : 目前均是返回默认头像，需要改成通过数据库中用户的avatar
    [HttpGet("getAvatar")]
    public IActionResult GetAvatar(string userId)
    {
        try
        {
            string avatarPath = Path.Combine("wwwroot", "images", "avatars", "default.jpg");  // 即 /wwwroot/images/avatars/default.jpg

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
