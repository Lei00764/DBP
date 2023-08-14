using Microsoft.AspNetCore.Mvc;
using auth.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using auth.Models;

using System;

namespace auth.Utils;


public class MyUtil
{
    private AppDbContext _database;

    public MyUtil(AppDbContext appDbContext)
    {
        _database = appDbContext;  // 依赖注入，在整个类中使用它来进行数据库操作
    }

    public int ChangePoints(int? user_id,int type)//变更用户积分的函数,可增可减
    {
        //获取对应的用户
        var user= _database.Users.Where(x=>x.UserId==user_id).ToList().First();
        int increment=0;
        if(user==null)
        {
            return -1;//用户不存在
        }
        if(Math.Abs(type)>3||type==0)
        {
            return -2;//type值无效
        }
        
        if(type==1||type==-1)//被点赞
        {
            increment=3;
        }
        else if(type==2||type==-2)//被收藏
        {
            increment=5;
        }
        else if(type==3||type==-3)//被关注
        {
            increment=2;
        }

        if(type<0)
        {//扣除积分
            increment*=-1;
        }

        user.Points+=increment;
        user.Levels=user.Points/100;
        _database.SaveChanges();//变更数据库
            
        return 0;
    }
}


