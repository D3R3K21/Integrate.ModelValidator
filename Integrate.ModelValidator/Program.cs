﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Integrate.ModelValidator
{
    class Program
    {
        public class TestType
        {
            public string GetName() { return "hello world!"; }
        }

        public static string TestMethod2()
        {
            object o = new TestType();

            var input = Expression.Parameter(typeof(object), "input");
            var method = o.GetType().GetMethod("GetName",
              System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            //you should check for null *and* make sure the return type is string here.

            //now build a dynamic bit of code that does this:
            //(object o) => ((TestType)o).GetName();
            Func<object, string> result = Expression.Lambda<Func<object, string>>(
              Expression.Call(Expression.Convert(input, o.GetType()), method), input).Compile();

            return result(o);
        }
        static void Main(string[] args)
        {


            var u = TestMethod2();
















            //validator can be initialized in IntegrateNancyBootstrapper
            Validator.Initialize();
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                UserName = "JimmyJohn1",
                SomeBoolProperty = null,
                PhoneNumber = "1234567894"
            };
            
            //Validate extension method inside of validator, accepts 'this BaseIntegrateModel',
            //calls the validate method on each individual custom attribute specefied on the property and passes in the value of that property,
            //since we are using reflection the Validate method does not need to be declared on Interface, 
            //we really just need the interface to allow for any model to access the extension method
            var validationResponse = model.Validate();

            var allValidCheck = validationResponse.All(p => p.IsValid);

            var finalResponse = new List<ValidatorReturnObject>();

            if (!allValidCheck)
            {
                finalResponse = validationResponse.Where(p => !p.IsValid).ToList();
            }
            var output = JsonConvert.SerializeObject(new { AllProperties = validationResponse, FailedProperties = finalResponse }, Formatting.Indented);

            Console.Out.WriteLine(output);
            Console.Read();


        }


    }
}
