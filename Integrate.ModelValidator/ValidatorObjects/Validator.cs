using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Nancy;
using Nancy.ModelBinding;

namespace Integrate.ModelValidator
{
    public static class Validator
    {
        private static Dictionary<Type, Dictionary<PropertyInfo, List<IntegrateAttribute>>> _propertyMappings;

        public static void Initialize()
        {
        }

        static Validator()
        {
            _propertyMappings = new Dictionary<Type, Dictionary<PropertyInfo, List<IntegrateAttribute>>>();
            GetModelMappings();
            SetMethodInfo();
        }


        private static void GetModelMappings()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = GetDerivedTypes(assembly, typeof(BaseIntegrateModel));

            types.ForEach(modelType =>
            {
                var dictionary = new Dictionary<PropertyInfo, List<IntegrateAttribute>>();

                var modelProperties = modelType.GetProperties().Where(m => m.GetCustomAttributes(typeof(IntegrateAttribute), true).Length > 0).ToList();

                modelProperties.ForEach(propertyInfo =>
                {
                    var attList = new List<IntegrateAttribute>();
                    propertyInfo.CustomAttributes.ToList().ForEach(p =>
                    {
                        attList.Add((IntegrateAttribute)Attribute.GetCustomAttribute(propertyInfo, p.AttributeType));
                    });

                    dictionary.Add(propertyInfo, attList);
                });
                _propertyMappings.Add(modelType, dictionary);
            });
        }
        #region CreateGenericMethodInfo

        public static MethodInfo BindMethodInfo { get; set; }
        public static MethodInfo GenericMethodInfo { get; set; }
        private static void SetMethodInfo()
        {
            BindMethodInfo = typeof(ModuleExtensions).GetMethods().First(p => p.Name == "Bind"
                && p.IsGenericMethod
                && p.GetParameters().Length == 1);
        }

        public static MethodInfo BindModel(BaseIntegrateModel model)
        {
            GenericMethodInfo = BindMethodInfo.MakeGenericMethod(model.DerivedType);
            MapMethods();
            return GenericMethodInfo;
        }

        private static void MapMethods()
        {
            var input = Expression.Parameter(typeof(Type), "input");
            var methodInfo = GenericMethodInfo;
            var result = Expression.Lambda<Func<object, BaseIntegrateModel>>(Expression.Call(null, methodInfo),input);
            var final = result.Compile();
            Console.Out.WriteLine();
        }

        #endregion






        public static List<ValidatorReturnObject> Validate(this BaseIntegrateModel model)
        {

            var returnList = new List<ValidatorReturnObject>();
            var modelProperties = _propertyMappings[model.DerivedType];


            var propinfolist = modelProperties.Select(p => p.Key).ToList();

            propinfolist.ForEach(p =>
            {
                var propertyValue = p.GetValue(model, null);

                var attList = modelProperties[p];
                attList.ForEach(x =>
                {

                    returnList.Add(new ValidatorReturnObject
                    {
                        PropertyName = p.Name,
                        PropertyValue = propertyValue,
                        IsValid = x.Valitade(propertyValue),
                        Error = x.ErrorMessage
                    });
                });
            });

            return returnList;
        }


        private static List<Type> GetDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t) && t != baseType && !t.Name.Contains("`1")).ToList();
        }

    }
}