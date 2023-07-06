// using Microsoft.AspNetCore.Mvc;
// using auth.Database;
// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// public struct summary{//文章的概要
//             public string title;
//             public string content;
//             public string author;
//              public summary(string title,string content,string author)
//         {
//              this.title = title;
//              this.content = content;
//              this.author=author;
//         }
// }
// [ApiController]
// [Route("api/[controller]")]  // RESTful 风格
// public class ArticleController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
// {

//     private readonly AppDbContext _database;

//     public ArticleController(AppDbContext appDbContext)
//     {
//         _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作。
//     }

//     // http://localhost:5045/api/login?id=1&password=xxx
//     // 定义一个名为 loginController 的控制器类，该类继承自 ControllerBase 类，这样它就可以处理 HTTP 请求了
//     [HttpGet]
//     public async Task<IActionResult> GetPageAsync(string tag)  // 对外暴露的接口参数名为 TagName 和 PageNum
//     {
//         var code=400;
//         var msg="success";
//         var article_data = await _database.Articles.ToListAsync();

//         List<summary> sum = new List<summary>();//存放文章摘要的列表
//         // 如果数据库中没有数据，返回错误信息
//         if (article_data == null)
//         {
//             code = 400;
//             msg = "数据库中没有数据";
//             return Ok(new
//             {
//                 code = code,
//                 msg = msg,
//             });
//         }
//         // 遍历data，找到id和password匹配的用户或管理员
//         bool temp = false;
//         if (article_data!= null)
//         {
//             summary s=new summary("0","0","0");
//             foreach (var article in article_data)
//             {
//                 if (article.Tag == tag)
//                 {
//                     s.title=article.Title;
//                     s.content=article.content;
//                     s.author=article.
//                 }

//             }
//         }

//         if (temp == false)//用户表里未找到
//         {
//             foreach (var admin in admin_data)
//             {
//                 if (admin.Email == email && admin.PassWord == password)
//                 {
//                     code = 200;
//                     msg = "管理员登录成功";
//                     type = 0;
//                     temp = true;
//                     break;
//                 }
//                 else
//                 {
//                     code = 400;
//                     msg = "用户名或密码错误";
//                 }
//             }
//         }
//         return Ok(new
//         {
//             code = code,
//             msg = msg,
//             type = type,
//         });
//     }
// }