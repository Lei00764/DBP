// using Microsoft.AspNetCore.Mvc;
// using auth.Database;
// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using auth.Models;

// [ApiController]
// [Route("api/[controller]")]  // RESTful 风格

// public class CommentController : ControllerBase
// {
//     private AppDbContext _database;

//     public CommentController(AppDbContext appDbContext)
//     {
//         _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
//     }

//     [HttpPost("postComment")]
// }