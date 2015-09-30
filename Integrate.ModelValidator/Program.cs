using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Integrate.ModelValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            Validator.Initialize();
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                UserName = "Some User1"
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
