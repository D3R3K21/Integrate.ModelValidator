using System;

namespace TestModule
{
    public class IntegrateModel<T> : BaseIntegrateModel
    {
        public IntegrateModel()
        {
            DerivedType = typeof(T);
        }

    }
}