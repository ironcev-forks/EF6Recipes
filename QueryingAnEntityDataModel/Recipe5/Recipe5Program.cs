using QueryingAnEntityDataModel.Recipe5;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe5
{
    /// <summary>
    /// 3-5 查找主从复合结构关系中的拥有从表记录的主表记录
    /// </summary>
    public class Recipe5Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                // 删除测试数据
                context.Database.ExecuteSqlCommand("delete from chapter3.comments");
                context.Database.ExecuteSqlCommand("delete from chapter3.blogposts");
                // 添加新的测试数据
                var post1 = new BlogPost
                {
                    Title = "The Joy of LINQ",
                    Description = "101 things you always wanted to know about LINQ"
                };
                var post2 = new BlogPost
                {
                    Title = "LINQ as Dinner Conversation",
                    Description = "What wine goes with a Lambda expression?"
                };
                var post3 = new BlogPost
                {
                    Title = "LINQ and our Children",
                    Description = "Why we need to teach LINQ in High School"
                };
                var comment1 = new Comment
                {
                    Content = "Great post, I wish more people would talk about LINQ"
                };
                var comment2 = new Comment
                {
                    Content = "You're right, we should teach LINQ in high school!"
                };
                post1.Comments.Add(comment1);
                post3.Comments.Add(comment2);
                context.BlogPosts.Add(post1);
                context.BlogPosts.Add(post2);
                context.BlogPosts.Add(post3);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                Console.WriteLine("Blog Posts with comments...(LINQ)");
                //var posts = from post in context.BlogPosts
                //            where post.Comments.Any()
                //            select post;
                var posts = context.BlogPosts.Where(p => p.Comments.Any());
                foreach (var post in posts)
                {
                    Console.WriteLine("Blog Post: {0}", post.Title);
                    foreach (var comment in post.Comments)
                    {
                        Console.WriteLine("\t{0}", comment.Content);
                    }
                }
            }

            Console.WriteLine();

            using (var context = new EFContext())
            {
                Console.WriteLine("Blog Posts with comments...(eSQL)");
                var esql = "select value p from BlogPosts as p where exists(p.Comments)";
                var posts = ((IObjectContextAdapter)context).ObjectContext.CreateQuery<BlogPost>(esql);
                foreach (var post in posts)
                {
                    Console.WriteLine("Blog Post: {0}", post.Title);
                    foreach (var comment in post.Comments)
                    {
                        Console.WriteLine("\t{0}", comment.Content);
                    }
                }
            }
        }

    }
}
