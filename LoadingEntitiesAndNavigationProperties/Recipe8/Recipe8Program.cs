using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe8
{
    /// <summary>
    /// 5-8  延缓加载（Deferred Loading）相关实体
    /// </summary>
    public class Recipe8Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var company = new Company { Name = "Acme Products" };
                var acc = new Department { Name = "Accounting", Company = company };
                var ship = new Department { Name = "Shipping", Company = company };
                var emp1 = new Employee { Name = "Jill Carpenter", Department = acc };
                var emp2 = new Employee { Name = "Steven Hill", Department = ship };
                context.Employees.Add(emp1);
                context.Employees.Add(emp2);
                context.SaveChanges();
            }
          //  如果我们还没有Employee实体的实例，我们可以简单地使用Include()方法和一个查询路径 Department.Company来实现 。
          //  这是我们之前使用的方法。它的缺点是，它会获取employee实体的所有列。有很多情况下，这是一个昂贵的操作。因为我们已经加载对象到上下文中，
          //  再一次去数据库获取所有的列并传输到上下文中，这是一个浪费！

　　      //  在第二个查询中，我们使用上下文对象DbContext公布的Entry()方法访问Employee对象并对其执行操作。
          //  然后我们链式调用Reference()方法和DbReferenceEntity类的Query()方法，返回一个从数据库中加载关联对象Deparment的查询。
          //  另外，我们链式调用Include()方法来拉取关联对象Company的信息。正如所期望的那样，这个查询获取了Department和Company的数据，
          //  它没有去获取Employees的数据，因为这些数据已经存在于上下文对象中了。

            // 第一种方法
            using (var context = new EFContext())
            {
                // 假设我们已经拥有一个employee
                var jill = context.Employees.First(o => o.Name == "Jill Carpenter");

                // 获取Jill的部门和公司, 但我们需要重新加载employee
                var results = context.Employees.Include("Department.Company")
                                     .First(o => o.EmployeeId == jill.EmployeeId);
                Console.WriteLine("{0} works in {1} for {2}", jill.Name, jill.Department.Name,
                                  jill.Department.Company.Name);
            }

            //更有效的方法, 不用再加载employee
            using (var context = new EFContext())
            {
                // 假设我们已经拥有一个employee
                var jill = context.Employees.Where(o => o.Name == "Jill Carpenter").First();

            //    SELECT 
            //    [Extent2].[DepartmentId]
            //        AS[DepartmentId], 
            //    [Extent2].[Name]
            //        AS[Name], 
            //    [Extent2].[CompanyId]
            //        AS[CompanyId], 
            //    [Extent3].[CompanyId]
            //        AS[CompanyId1], 
            //    [Extent3].[Name]
            //        AS[Name1]
            //FROM[Chapter5].[Employees]
            //        AS[Extent1]
            //INNER JOIN[Chapter5].[Departments]
            //        AS[Extent2] ON[Extent1].[Department_DepartmentId] = [Extent2].[DepartmentId]
            //        INNER JOIN[Chapter5].[Companys]
            //        AS[Extent3] ON[Extent2].[CompanyId] = [Extent3].[CompanyId]
            //WHERE([Extent1].[Department_DepartmentId] IS NOT NULL) AND([Extent1].[EmployeeId] = @EntityKeyValue1)
                //凭借Entry、Reference，Query和Include方法获取Department和Company数据，不用去查询底层的Employee表
                context.Entry(jill).Reference(x => x.Department).Query().Include(y => y.Company).Load();

                Console.WriteLine("{0} works in {1} for {2}", jill.Name, jill.Department.Name,
                                  jill.Department.Company.Name);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
