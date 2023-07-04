using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class loginController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
{

    private readonly AppDbContext _database;

    public loginController(AppDbContext appDbContext)
    {
        _database = appDbContext;
    }

    // http://localhost:5045/api/login?id=1&password=xxx
    [HttpGet]
    public IActionResult CheckUser(string email, string password)  // 对外暴露的接口参数名为 id 和 password
    {
        var type = -1;  // 区分登陆的是管理员还是用户
        // type=1 用户
        // type=0 管理员

        var code = 400;
        var msg = "success";
        object user_data = _database.Users.ToListAsync().Result;
        object admin_data = _database.Administrators.ToListAsync().Result;
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
        foreach (var user in (System.Collections.Generic.List<auth.Models.User>)user_data)
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
        if (temp == false)//用户表里未找到
        {
            foreach (var admin in (System.Collections.Generic.List<auth.Models.Administrator>)admin_data)
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
                // 打印
                //System.Console.WriteLine(admin.UserId);
            }
        }
        return Ok(new
        {
            code = code,
            msg = msg,
            type = type,
        });
    }
}