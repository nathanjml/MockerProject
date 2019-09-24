using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mocker.Core.Expressions
{
    public class ExpressionEvaluator<T, Z>
        where T: class, new()
    {
        private readonly MockerInstance<T> _mockerInstance;
        private Expression<Func<T, Z>> _e; // x => x.value

        internal ExpressionEvaluator(MockerInstance<T> mockerInstance, Expression<Func<T, Z>> e)
        {
            _mockerInstance = mockerInstance;
            _e = e;
        }

        public MockerInstance<T> Returns(Z returnValue)
        {
            var constant = Expression.Constant(returnValue, typeof(Z));
            var targetExpression = _e.Body;

            var body = Expression.Assign(targetExpression, constant);
            var assign = Expression.Lambda<Action<T>>(body, _e.Parameters.Single());
            _mockerInstance.expressionObjIntializers.Add(assign);

            return _mockerInstance;
        }

        //this will not work because the expression is compiled with a const
        public MockerInstance<T> ReturnsRandom(IList<Z> zItems)
        {
            var randomSelection = GetRandom(zItems);
            var targetExpression = _e.Body;

            var methodsAvailable = this.GetType().GetMethods();
            var minfo = this.GetType().GetMethods().Where(x => x.Name.Contains("GetRandom")).FirstOrDefault();
            var method = minfo.MakeGenericMethod(typeof(Z));
            var value = Expression.Call(method, Expression.Constant(zItems));

            var body = Expression.Assign(targetExpression, value);
            var assign = Expression.Lambda<Action<T>>(body, _e.Parameters.Single());
            _mockerInstance.expressionObjIntializers.Add(assign);

            return _mockerInstance;
        }

        public static Y GetRandom<Y>(IList<Y> items)
        {
            var rand = new Random();
            var selection = rand.Next(0, items.Count - 1);
            return items[selection];
        }
    }
}
