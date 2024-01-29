using System;
using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;

namespace ClubMembershipApplication.Data
{
    public class RegisterUser : IRegister
    {
        public bool EmailExists(string emailAddress)
        {
            bool emailExists = false;
            using (var dbContext = new ClubMembershipDbContext())
            {
                emailExists = dbContext.Users.Any(u => u.EmailAddress.ToLower().Trim() == emailAddress.ToLower().Trim());
            }
            return emailExists;
        }

        public bool Register(string[] fields)
        {
           using (var dbContext = new ClubMembershipDbContext())
            {
                User user = new User
                {
                    EmailAddress = fields[(int)FieldConstant.UserRegistrationField.EmailAddress],
                    FirstName = fields[(int)FieldConstant.UserRegistrationField.FirstName],
                    Lastname = fields[(int)FieldConstant.UserRegistrationField.LastName],
                    Password = fields[(int)FieldConstant.UserRegistrationField.Password],
                    PhoneNumber = fields[(int)FieldConstant.UserRegistrationField.PhoneNumber],
                    DateOfBirth = DateTime.Parse(fields[(int)FieldConstant.UserRegistrationField.DateOfBirth]),
                    Postcode = fields[(int)FieldConstant.UserRegistrationField.PostCode],
                    AddressFirstLine = fields[(int)FieldConstant.UserRegistrationField.AddressFirstLine],
                    AddressSecondLine = fields[(int)FieldConstant.UserRegistrationField.AddressSecondLine],
                    AddressCity = fields[(int)FieldConstant.UserRegistrationField.AddressCity]
                };

                dbContext.Users.Add(user);


                dbContext.SaveChanges();
                
            }
            return true;
        }


    }
}

