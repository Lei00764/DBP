using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using auth.Models;
using auth.Database;


[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class AdminController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
{

    private readonly AppDbContext _database;

    public AdminController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作。
    }

    //获取管理员资料信息
    [HttpGet]
    public async Task<IActionResult> GetAdminInfo(int adminId)
    {
        // 根据业务逻辑获取信息对象
        var code = 200;
        var msg = "success";
        var admin_data = await _database.Administrators.ToListAsync();
        // 如果数据库中没有数据，返回错误信息
        if (admin_data == null)
        {
            code = 400;
            msg = "数据库中没有数据";
            return Ok(new
            {
                code = code,
                msg = msg,
            });
        }
        // 遍历data，找到id匹配的管理员
        var name ="";
        var avatar ="";
        var tel ="";
        var email ="";
        if (admin_data != null)
        {
            foreach (var admin in admin_data)
            {
                if (admin.AdminId == adminId )
                {
                    code = 200;
                    msg = "查询到管理员信息";
                    name = admin.AdminName;
                    avatar = admin.Avatar;
                    tel = admin.Tel;
                    email = admin.Email;
                    break;
                }

            }
        }
        // 将管理员信息对象作为响应的数据发送回前端
        return Ok(new
        {
            code = code,
            msg = msg,
            avatar = avatar,
            tel = tel,
            email = email,
        });
    }
}