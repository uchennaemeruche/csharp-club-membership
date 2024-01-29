using System;
using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views
{
    public class UserRegistrationView : IView
	{
		IFieldValidator _fieldValidator;

		IRegister _register;

        public IFieldValidator FieldValidator { get => _fieldValidator; }

        public UserRegistrationView(IRegister register, IFieldValidator fieldValidator)
        {
            _fieldValidator = fieldValidator;
            _register = register;

        }

        public void RunView()
        {

            CommonOutputText.WriteMainHeading();

            CommonOutputText.WriteRegistrationHeading();

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.EmailAddress] =
                GetInputFromUser(FieldConstant.UserRegistrationField.EmailAddress, "Please enter your email address: ");


            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.FirstName] =
                GetInputFromUser(FieldConstant.UserRegistrationField.FirstName, "Please enter your firstname: ");

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.LastName] =
              GetInputFromUser(FieldConstant.UserRegistrationField.LastName, "Please enter your lastname: ");

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.PhoneNumber] =
              GetInputFromUser(FieldConstant.UserRegistrationField.PhoneNumber, "Please enter your phone number: ");

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.DateOfBirth] =
              GetInputFromUser(FieldConstant.UserRegistrationField.DateOfBirth, "Please enter your dob: ");

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.Password] =
              GetInputFromUser(FieldConstant.UserRegistrationField.Password, "Please enter your password: ");

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.PasswordCompare] =
              GetInputFromUser(FieldConstant.UserRegistrationField.PasswordCompare, "Re-enter your password: ");

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.PostCode] =
              GetInputFromUser(FieldConstant.UserRegistrationField.PostCode, "Please enter your post code: ");

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.AddressFirstLine] =
              GetInputFromUser(FieldConstant.UserRegistrationField.AddressFirstLine, "Please enter your Address Line 1: ");

            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.AddressSecondLine] =
              GetInputFromUser(FieldConstant.UserRegistrationField.AddressSecondLine, "Please enter your Address Line 2: ");


            _fieldValidator.FieldArray[(int)FieldConstant.UserRegistrationField.AddressCity] =
              GetInputFromUser(FieldConstant.UserRegistrationField.AddressCity, "Please enter your city address: ");


            RegisterUser();

        }


        private void RegisterUser()
        {

            _register.Register(_fieldValidator.FieldArray);

            CommonOutputFormat.ChangeFontColor(FontTheme.Success);
            Console.WriteLine("User successfully registered. Please press any key to login");

            CommonOutputFormat.ChangeFontColor(FontTheme.Default);

            Console.ReadKey();

        }
        private string GetInputFromUser(FieldConstant.UserRegistrationField field, string promptText)
        {
            string fieldValue = "";

            do
            {
                Console.Write(promptText);
                fieldValue = Console.ReadLine()!;

            } while (!FieldValid(field, fieldValue));

            return fieldValue;
        }

        private bool FieldValid(FieldConstant.UserRegistrationField field, string fieldValue)
        {
            if(!_fieldValidator.validatorDel((int)field, fieldValue, _fieldValidator.FieldArray, out string invalidMessage))
            {
                CommonOutputFormat.ChangeFontColor(FontTheme.Danger);

                Console.WriteLine(invalidMessage);

                CommonOutputFormat.ChangeFontColor(FontTheme.Default);
                return false;
            }
            return true;
        }
    }
}

