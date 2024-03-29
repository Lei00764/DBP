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
        // type = 1 用户
        // type = 0 管理员

        var code = 404;
        var msg = "success";
        var user_data = await _database.Users.ToListAsync();
        var admin_data = await _database.Administrators.ToListAsync();

        // 如果数据库中没有数据，返回错误信息
        if (user_data == null && admin_data == null)
        {
            return Ok(new
            {
                code = 400,
                msg = "数据库中没有数据"
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

        if (temp == false) // 用户表里未找到
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
                    code = 404;
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
        // AnyAsync 异步地确定序列中是否包含任何元素
        var emailUserExists = await _database.Users.AnyAsync(u => u.Email == email);
        if (emailUserExists)
        {
            return Ok(new
            {
                code = 400,
                msg = "该邮箱已被注册成为用户",
            });
        }

        var emailAdminExists = await _database.Administrators.AnyAsync(u => u.Email == email);
        if (emailAdminExists)
        {
            return Ok(new
            {
                code = 400,
                msg = "该邮箱已被注册成为管理员"
            });
        }

        if (type == 1)  // 注册用户
        {
            var newUser = new User
            {
                UserName = username,
                Email = email,
                PassWord = password,
            };

            await _database.Users.AddAsync(newUser);
            await _database.SaveChangesAsync();

            return Ok(new
            {
                code = 200,
                msg = "用户成功注册",
            });
        }
        else if (type == 0)  // 注册管理员
        {
            var newAdmin = new Administrator
            {
                AdminName = username,
                Email = email,
                PassWord = password,
            };

            await _database.Administrators.AddAsync(newAdmin);
            await _database.SaveChangesAsync();

            return Ok(new
            {
                code = 200,
                msg = "管理员成功注册",
            });
        }
        else // 通过网页注册不会出现这种情况，仅用于 swagger 调试
        {
            return Ok(new
            {
                code = 404,
                msg = "type 类型错误",
            });
        }
    }

    //通过Email获取用户/管理员资料信息
    [HttpGet("getInfoByEmail")]
    public IActionResult GetInfoByEmail(string email) // email 可以用来判断是管理员还是用户
    {
        // 在用户表中找到匹配的邮箱
        var user = _database.Users.Where(x => x.Email == email).ToList();
        if (user.Any())
        {
            var firstUser = user.First();
            return Ok(new
            {
                code = 200,
                msg = "success",
                data = new
                {
                    id = firstUser.UserId,
                    email = firstUser.Email,
                    password = firstUser.PassWord,
                    avatar = firstUser.Avatar,
                    tel = firstUser.Tel,
                    name = firstUser.UserName,
                    levels = firstUser.Levels,
                    points = firstUser.Points
                }
            });
        }

        // 在管理员表中找到匹配的邮箱
        var admin = _database.Administrators.Where(x => x.Email == email).ToList();
        if (admin.Any())
        {
            var firstAdmin = admin.First();
            return Ok(new
            {
                code = 200,
                msg = "success",
                data = new
                {
                    id = firstAdmin.AdminId,
                    email = firstAdmin.Email,
                    password = firstAdmin.PassWord,
                    avatar = firstAdmin.Avatar,
                    tel = firstAdmin.Tel,
                    name = firstAdmin.AdminName
                }
            });
        }

        // 如果在用户表和管理员表都找不到，返回错误信息
        return Ok(new
        {
            code = 404,
            msg = "在用户表和管理员表都找不到",
        });
    }

    // 通过ID获取用户/管理员资料信息
    [HttpGet("InfoByID")]
    public IActionResult GetInfoByID(int ID, int type) // 参数type:0为管理员，1为普通用户
    {
        if (type == 0)
        {
            var admin = _database.Administrators.Where(x => x.AdminId == ID).ToList();
            if (admin.Any())
            {
                var firstAdmin = admin.First();
                return Ok(new
                {
                    code = 200,
                    msg = "已返回管理员信息",
                    data = new
                    {
                        id = firstAdmin.AdminId,
                        email = firstAdmin.Email,
                        password = firstAdmin.PassWord,
                        avatar = firstAdmin.Avatar,
                        tel = firstAdmin.Tel,
                        name = firstAdmin.AdminName
                    }
                });
            }
        }
        else if (type == 1)
        {
            var user = _database.Users.Where(x => x.UserId == ID).ToList();
            if (user.Any())
            {
                var firstUser = user.First();
                return Ok(new
                {
                    code = 200,
                    msg = "已返回用户信息",
                    data = new
                    {
                        id = firstUser.UserId,
                        email = firstUser.Email,
                        password = firstUser.PassWord,
                        avatar = firstUser.Avatar,
                        tel = firstUser.Tel,
                        name = firstUser.UserName,
                        levels = firstUser.Levels,
                        points = firstUser.Points
                    }
                });
            }
        }
        else
        {
            return Ok(new
            {
                code = 400,
                msg = "type值无效"
            });
        }

        return Ok(new
        {
            code = 400,
            msg = "不存在该用户或管理员",
        });
        // 以下代码较繁琐，先注释掉（7.28 李泽凯）
        //根据业务逻辑获取信息对象
        // var code = 200;
        // var msg = "success";
        // var user_data = _database.Users.Where(x => x.UserId == ID);
        // var admin_data = _database.Administrators.Where(x => x.AdminId == ID);
        // bool exist = false;

        // if (type == 1)
        // {
        //     foreach (var item in user_data)
        //     {
        //         if (item.UserId == ID)
        //         {
        //             exist = true;
        //             break;
        //         }
        //     }
        // }
        // else
        // {
        //     foreach (var item in admin_data)
        //     {
        //         if (item.AdminId == ID)
        //         {
        //             exist = true;
        //             break;
        //         }
        //     }
        // }
        // if (exist == false)
        // {// 如果数据库中没有数据，返回错误信息
        //     code = 400;
        //     msg = "用户不存在";
        //     return Ok(new
        //     {
        //         code = code,
        //         msg = msg,
        //     });
        // }
        // // 遍历data，找到id匹配的用户
        // var name = "";
        // var avatar = "";
        // var tel = "";
        // var email = "";
        // if (type == 1)
        // {
        //     foreach (var user in user_data)
        //     {
        //         if (user.UserId == ID)
        //         {
        //             code = 200;
        //             msg = "查询到用户信息";
        //             name = user.UserName;
        //             avatar = user.Avatar;
        //             tel = user.Tel;
        //             email = user.Email;
        //             break;
        //         }
        //     }
        // }
        // else
        // {
        //     foreach (var admin in admin_data)
        //     {
        //         if (admin.AdminId == ID)
        //         {
        //             code = 200;
        //             msg = "查询到管理员信息";
        //             name = admin.AdminName;
        //             avatar = admin.Avatar;
        //             tel = admin.Tel;
        //             email = admin.Email;
        //             break;
        //         }
        //     }
        // }
        // // 将信息对象作为响应的数据发送回前端
        // return Ok(new
        // {
        //     code = code,
        //     msg = msg,
        //     name = name,  // 2023.7.12 lx
        //     avatar = avatar,
        //     tel = tel,
        //     email = email
        // });
    }
    //更改用户积分
    [HttpPost("changePoint")]
    public async Task<IActionResult> UserchangePoint(int user_id,int point_add,int level_add) // 传入增加的参数
    {
        
        // 在用户表中找到匹配的用户
        var user = _database.Users.Where(x => x.UserId == user_id);
        if (user.Any())
        {
            foreach (var item in user)
            {
                item.Points = item.Points+point_add;
                item.Levels = item.Levels+level_add;
            }
            await _database.SaveChangesAsync();
            return Ok(new
            {
                code = 200,
                msg = "success",
            });
        }
        else{
            // 如果在用户表找不到，返回错误信息
            return Ok(new
            {
                code = 400,
                msg = "Not Found",
            });
        }
        
    }
    //根据用户ID获取部分个人信息:用户昵称、个人积分、粉丝数量/ 关注数量、文章数量
    // [HttpGet("getUserInfo/user_id")]
    // public async Task<IActionResult> getUserInfoAsync(int user_id)
    // {
    //     var code = 200;
    //     var msg = "success";
    //     var temp = await _database.Users.ToListAsync();
    //     bool exist = false;
    //     if (temp != null)//判断表内是否有信息
    //     {
    //         foreach (var user in temp)
    //         {
    //             if (user.UserId == user_id)
    // 下面这段代码有问题！！！
    // 请以后提交代码前务必使用 dotnet run 运行后端代码


     //编辑个人信息
     //返回用户认证令牌(Token)未实现
     [HttpPost("edit")]
     public async Task<IActionResult> EditAsync(int Id, string name, string password)
     {
         var code = 200;
         var msg = "success";
         var user_data = _database.Users.Where(x => x.UserId == Id);
         bool exist = false;

        foreach (var item in user_data)
             {
                 if (item.UserId == Id)
                 {
                     exist = true;
                     break;
                 }
             }
         if(exist){
             //参数无效，返回401
             if (name == null || password == null )
            {
                code = 401;
                msg = "参数无效";
                return Ok(new
               {
                 code = code,
                 msg = msg,
               });
            }
            else{
                foreach (var item in user_data)
             {
                 item.UserName = name;
                 item.PassWord = password;
             }
             await _database.SaveChangesAsync();
             return Ok(new
               {
                 code = code,
                 msg = msg,
               });
            }
         }
         else{
             code = 400;
             msg = "不存在该用户信息";
             return Ok(new
             {
                 code = code,
                 msg = msg
             });
         }

         
    }
}

// var temp = await _database.Articles.ToListAsync();
//         bool exist = false;
//         if (temp != null)//判断表内是否有该文章
//         {
//             foreach (var article in temp)
//             {
//                 if (article.PostId == post_id)
//                 {
//                     exist = true;
//                     break;
//                 }
//             }
//         }
//         if (exist)
//         {
//             var article_data = _database.Articles.Where(a => a.PostId == post_id);
//             foreach (var item in article_data)
//             {
//                 item.Title = title;
//                 item.Content = content;
//             }
//             await _database.SaveChangesAsync();
//             return Ok(new
//             {
//                 code = code,
//                 msg = msg,
//             });
//         }
//         else
//         {
//             code = 400;
//             msg = "不存在该文章";
//             return Ok(new
//             {
//                 code = code,
//                 msg = "更新成功"
//             });
//         }
//     }