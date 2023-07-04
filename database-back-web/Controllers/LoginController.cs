using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class LoginController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
{

    private readonly AppDbContext _database;

    public LoginController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作。
    }

    // http://localhost:5045/api/login?id=1&password=xxx
    // 定义一个名为 loginController 的控制器类，该类继承自 ControllerBase 类，这样它就可以处理 HTTP 请求了
    [HttpGet]
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
}