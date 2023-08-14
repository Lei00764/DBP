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
    [HttpGet("searchArticle")]
     public async Task<IActionResult> SearchPost(int user_id,string keyword)
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
        if(_database.SearchHistories.Any(x=>x.UserId==user_id&&x.Content==keyword)==false)//数据库中没有相同的搜索记录
        {
            var record =new SearchHistory()
            {
                UserId=user_id,
                Content=keyword
            };
            _database.SearchHistories.AddRange(record);
            _database.SaveChanges();
        }
        
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

    [HttpPost("AddSearchHistory")]
    public async Task<IActionResult> AddSearchHistory(int user_id,string content)
    {
        var code = 200;
        var msg = "success";
        if(_database.Users.Any(x=>x.UserId==user_id)==false)
        {
            return BadRequest(new
            {
                code = 400,
                msg = "用户不存在"
            });
        }
        if(_database.SearchHistories.Any(x=>x.Content==content)==true)
        {//同名记录已存在
            msg="数据库已有该记录";
        }
        else
        {
            var newRecord = new SearchHistory()
            {
                UserId = user_id,
                Content = content
            };
            _database.SearchHistories.AddRange(newRecord);
            await _database.SaveChangesAsync();
            msg="添加成功";
        }
        return Ok(new
        {
            code = code,
            msg = msg,
        });
        
    }

    [HttpDelete("RemoveSearchHistory")]
    public async Task<IActionResult> RemoveSearchHistory(int id)
    {
        var code = 200;
        var msg = "success";
        if(_database.SearchHistories.Any(x=>x.SearchId==id)==false)
        {
            return BadRequest(new
            {
                code = 400,
                msg = "搜索记录不存在"
            });
        }
        var r=_database.SearchHistories.Where(x=>x.SearchId==id);
        _database.SearchHistories.RemoveRange(r);
        await _database.SaveChangesAsync();
        return Ok(new
        {
            code = code,
            msg = "删除成功",
        });
    }

    [HttpGet("LoadSearchHistory")]
    public async Task<IActionResult> GetSearchHistoryAsync(int user_id,int num)//num为要获取的记录数，-1表示全部
    {
        var code = 200;
        var msg="suc";
        List<SearchHistory> data=new List<SearchHistory>();
        if(_database.Users.Any(x=>x.UserId==user_id)==false)
        {
            return BadRequest(new
            {
                code = 400,
                msg = "用户不存在"
            });
        }
        data=await _database
            .SearchHistories
            .OrderByDescending(x=>x.SearchId)
            .Where(x=>x.UserId==user_id)
            .ToListAsync();
        if(num==-1||num>=data.Count())
        {
            msg = "已获取全部记录";
        }
        else if(num>0)
        {
            data=data.Take(num).ToList();
            msg="已获取前"+num+"条记录";
        }
        else
        {
            return BadRequest(new
            {
                code = 400,
                msg = "num取值有误"
            });
        }
        return Ok(new
        {
            code = code,
            msg = msg,
            data=data
        });
    }
}