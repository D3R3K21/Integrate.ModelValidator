using System;

namespace TestModule
{
    public abstract class BaseIntegrateModel
    {
        public Type DerivedType { get; set; }

        protected BaseIntegrateModel()
        {
        }
        public static T Bind<T>(T obj) where T : class
        {
            var type = obj.GetType();
            return Activator.CreateInstance(type) as T;
        }
    }
}