using System;

namespace Integrate.ModelValidator
{
    public class UserModel : IntegrateModel<UserModel>
    {
        public UserModel()
        {
            DerivedModel = this;
        }


        [ValidateGuid("This is a Guid error on UserModel.Id")]
        public Guid Id { get; set; }

        [ValidateRegex("This is a Regex error on UserModel.UserName", @"^[A-Za-z ]+$")]
        [ValidateRequired("This is a Required error on UserModel.UserName")]
        public string UserName { get; set; }

    }
}