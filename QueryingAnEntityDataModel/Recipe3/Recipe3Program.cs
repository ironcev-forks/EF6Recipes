using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe3
{
    /// <summary>
    /// 3-3使用原生SQL语句获取对象
    /// </summary>
    public class Recipe3Program
    {
        public static void Run()
        {

            using (var context = new EFContext())
            {
                // delete previous test data
                context.Database.ExecuteSqlCommand("delete from chapter3.students");
                // insert student data
                context.Students.Add(new Student
                {
                    FirstName = "Robert",
                    LastName = "Smith",
                    Degree = "Masters"
                });
                context.Students.Add(new Student
                {
                    FirstName = "Julia",
                    LastName = "Kerns",
                    Degree = "Masters"
                });
                context.Students.Add(new Student
                {
                    FirstName = "Nancy",
                    LastName = "Stiles",
                    Degree = "Doctorate"
                });
                context.SaveChanges();
            }
            using (var context = new EFContext())
            {
                string sql = "select * from Chapter3.Students where Degree = @Major";
                var parameters = new DbParameter[] { new SqlParameter { ParameterName = "Major", Value = "Masters" } };
                var students = context.Students.SqlQuery(sql, parameters);
                Console.WriteLine("Students...");
                foreach (var student in students)
                {
                    Console.WriteLine("{0} {1} is working on a {2} degree",
                    student.FirstName, student.LastName, student.Degree);
                }
            }
        }
    }
}
