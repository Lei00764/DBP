using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using auth.Models;


[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly AppDbContext _database;

    public RegisterController(AppDbContext appDbContext)
    {
        _database = appDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(string username, string email, string password, int type)
    {
        var code = 200;
        var msg = "success";

        if (type == 1)
        {
            // AnyAsync 异步地确定序列中是否包含任何元素
            var emailExists = await _database.Users.AnyAsync(u => u.Email == email);
            if (emailExists)
            {
                return BadRequest(new
                {
                    code = 400,
                    msg = "电子邮件已经被使用",
                });
            }
            else
            {
                var newUser = new User
                {
                    // UserId = 5,
                    UserName = username,
                    Email = email,
                    PassWord = password,
                };

                await _database.Users.AddAsync(newUser);
                msg = "用户成功注册";
            }
        }
        else if (type == 0)
        {
            var emailExists = await _database.Administrators.AnyAsync(u => u.Email == email);
            if (emailExists)
            {
                return BadRequest(new
                {
                    code = 400,
                    msg = "电子邮件已经被使用",
                });
            }
            else
            {
                var newAdmin = new Administrator
                {
                    AdminName = username,
                    Email = email,
                    PassWord = password,
                };

                await _database.Administrators.AddAsync(newAdmin);
                msg = "管理员成功注册";
            }
        }
        else  // 通过网页注册不会出现这种情况，仅用于 swagger 调试
        {
            return BadRequest(new
            {
                code = 400,
                msg = "type 类型错误",
            });
        }

        await _database.SaveChangesAsync();

        return Ok(new
        {
            code = code,
            msg = msg,
        });
    }
}
