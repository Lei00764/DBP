using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using auth.Models;
using auth.Database;


[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class UserController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
{

    private readonly AppDbContext _database;

    public UserController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作。
    }


    // 用户/管理员注册
    // http://localhost:5045/api/login?id=1&password=xxx
    // 定义一个名为 loginController 的控制器类，该类继承自 ControllerBase 类，这样它就可以处理 HTTP 请求了
    [HttpGet("login")]
    public async Task<IActionResult> CheckUserAsync(string email, string password)  // 对外暴露的接口参数名为 id 和 password
    {
        var type = -1;  // 区分登陆的是管理员还是用户
        // type=1 用户
        // type=0 管理员

        var code = 400;
        var msg = "success";
        var user_data = await _database.Users.ToListAsync();
        var admin_data = await _database.Administrators.ToListAsync();

        // 如果数据库中没有数据，返回错误信息
        if (user_data == null && admin_data == null)
        {
            code = 400;
            msg = "数据库中没有数据";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        // 遍历data，找到id和password匹配的用户或管理员
        bool temp = false;
        if (user_data != null)
        {
            foreach (var user in user_data)
            {
                if (user.Email == email && user.PassWord == password)
                {
                    code = 200;
                    msg = "普通用户登录成功";
                    type = 1;
                    temp = true;
                    break;
                }

            }
        }

        if (temp == false)//用户表里未找到
        {
            foreach (var admin in admin_data)
            {
                if (admin.Email == email && admin.PassWord == password)
                {
                    code = 200;
                    msg = "管理员登录成功";
                    type = 0;
                    temp = true;
                    break;
                }
                else
                {
                    code = 400;
                    msg = "用户名或密码错误";
                }
            }
        }
        return Ok(new
        {
            code = code,
            msg = msg,
            type = type,
        });
    }

    // 用户/管理员登录
    [HttpPost("register")]
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