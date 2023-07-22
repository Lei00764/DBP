using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using auth.Models;
using auth.Database;

[ApiController]
[Route("api/[controller]")]  // RESTful 风格
public class SearchController : ControllerBase  // 命名规范，继承自 ControllerBase 类的类名必须与 Controller 结尾
{
    private readonly AppDbContext _database;

    public SearchController(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作。
    }


    // 关键词搜索帖子
    [HttpGet("post")]
     public async Task<IActionResult> SearchPost(string keyword)
    {
        var code = 200;
        var msg = "success";
        var article_data = await (
            from article in _database.Articles
            where ((article.Title != null && article.Title.Contains(keyword)) 
                || (article.Content != null && article.Content.Contains(keyword)))
                && article.IsBanned == 0
            select new
            {
                id = article.PostId,
                title = article.Title,
                content = article.Content,
                isBanned = article.IsBanned,
            }
            ).ToListAsync();
        if (article_data.Count > 0)
        {
            return Ok(new
            {
                code = code,
                msg = msg,
                data = article_data
            });
        }
        else
        {
            return BadRequest(new
            {
                code = 400,
                msg = "未查询到结果"
            });
        }
    }
}