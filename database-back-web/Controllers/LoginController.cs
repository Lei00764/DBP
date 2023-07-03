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
    public IActionResult CheckUser(int id, string password)  // 对外暴露的接口参数名为 id 和 password
    {
        // await _database.Airelectricitys.ToListAsync();
        System.Console.WriteLine("loginController");

        var code = 200;
        var msg = "success";
        object data = _database.Users.ToListAsync().Result;
        // 如果数据库中没有数据，返回错误信息
        if (data == null)
        {
            code = 400;
            msg = "数据库中没有数据";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        // 遍历data，找到id和password匹配的用户
        foreach (var user in (System.Collections.Generic.List<auth.Models.User>)data)
        {
            if (user.UserId == id && user.PassWord == password)
            {
                code = 200;
                msg = "登录成功";
                break;
            }
            else
            {
                code = 400;
                msg = "用户名或密码错误";
            }
            // 打印
            System.Console.WriteLine(user.UserId);
        }
        return Ok(new
        {
            code = code,
            msg = msg,
        });
    }
}