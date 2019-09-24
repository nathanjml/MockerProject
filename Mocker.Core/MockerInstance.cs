using Mocker.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mocker.Core
{
    public class MockerInstance<T>
        where T : class, new()
    {
        internal MockerInstance() { }

        public List<Expression<Action<T>>> expressionObjIntializers = new List<Expression<Action<T>>>();

        public ExpressionEvaluator<T, Z> Prop<Z>(Expression<Func<T, Z>> entityExpression)
        {
            var expEvaluator = new ExpressionEvaluator<T, Z>(this,  entityExpression);
            return expEvaluator;
        }

        public T Make()
        {
            var instance = new T();

            foreach(var e in expressionObjIntializers)
            {
                e.Compile().Invoke(instance);
            }

            return instance;
        }

        public IList<T> Make(int numberToMake)
        {
            var tList = new List<T>(numberToMake);

            for(var i = 0; i<numberToMake; i++)
            {
                var instance = new T();
                foreach(var e in expressionObjIntializers)
                {
                    e.Compile().Invoke(instance);
                }
                tList.Add(instance);
            }

            return tList;
        }
    }
}
