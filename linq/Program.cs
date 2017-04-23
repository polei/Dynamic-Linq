using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace linq
{
    class Program
    {
        class stu {
            public int id;

            public string name;
        }

        static void Main(string[] args)
        {

            //var list = new List<stu> {
            //    new stu{ id=1,name="123" },
            //    new stu{ id=2,name="a123" },
            //    new stu{ id=3,name="b123" },
            //    new stu{ id=4,name="c123" },
            //    new stu{ id=5,name="d123" },
            //    new stu{ id=6,name="e123" },
            //    new stu{ id=7,name="f123" },
            //};

            var list = new [] {
                new { id=1,name="123" },
                new { id=2,name="a123" },
                new { id=3,name="b123" },
                new { id=4,name="c123" },
                new { id=5,name="d123" },
                new { id=6,name="e123" },
                new { id=7,name="f123" },
            };

            //System.Linq.Dynamic
            {
                var temp = list.Where("id=6");


                var temp2 = list.OrderBy("name desc");
            }

            //条件过滤
            {
                System.Linq.Expressions.ParameterExpression paraExp = System.Linq.Expressions.Expression.Parameter(list.FirstOrDefault().GetType(), "a");
                System.Linq.Expressions.MemberExpression memberExp = System.Linq.Expressions.Expression.PropertyOrField(paraExp, "id");

                System.Linq.Expressions.ConstantExpression consExp = System.Linq.Expressions.Expression.Constant(3);

                System.Linq.Expressions.BinaryExpression largeExp = System.Linq.Expressions.Expression.GreaterThanOrEqual(memberExp, consExp);
                
                var lambda = FitExpression(list.FirstOrDefault(), true, largeExp, paraExp);


                var temp = list.Where(lambda.Compile());
            }

            //排序字段
            {
                System.Linq.Expressions.ParameterExpression paraExp = System.Linq.Expressions.Expression.Parameter(list.FirstOrDefault().GetType(), "a");
                System.Linq.Expressions.MemberExpression memberExp = System.Linq.Expressions.Expression.PropertyOrField(paraExp, "id");
                
                var lambda = FitExpression(list.FirstOrDefault(),"asf", memberExp, paraExp);


                var temp = list.OrderByDescending(lambda.Compile());
            }

            list.OrderByDescending(a => a.id);

            //var temp2 = from a in list
            //            where a.id % 2 == 0
            //            select a;

        }



        private static System.Linq.Expressions.Expression<Func<T, TResult>> FitExpression<T, TResult>( T paramItem, TResult result, System.Linq.Expressions.Expression body, params System.Linq.Expressions.ParameterExpression[] parameters)
        {
            System.Linq.Expressions.Expression<Func<T, TResult>> lamba = 
                System.Linq.Expressions.Expression.Lambda<Func<T, TResult>>(body, parameters);

            return lamba;
        }
    }
}
