using System;
using ClubMembershipApplication.Data;
using FieldValidatorAPI;

namespace ClubMembershipApplication.FieldValidators
{
    public class UserRegistrationValidator : IFieldValidator
	{
		const int UserName_Min_Length = 2;
		const int UserName_Max_Length = 100;

		delegate bool EmailExistsDel(string emailAddress);

		FieldValidatorDel _fieldValidatorDel = null;


        RequiredValidDel _requiredValidDel = null;
        StringLengthValidDel _stringLengthValidDel = null;
        DateValidDel _dateValidDel = null;
        PatternMatchValidDel _patternMatchValidDel = null;
        CompareFieldsValidDel _compateFieldsValidDel = null;

        EmailExistsDel _emailExistsDel = null;

        string[] _fieldArray;

        IRegister _register;

        public string[] FieldArray
        {
            get
            {
                if (_fieldArray == null)
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstant.UserRegistrationField)).Length];
                return _fieldArray;
            }

        }

        public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

        public FieldValidatorDel validatorDel => _fieldValidatorDel;

        public UserRegistrationValidator(IRegister register) {
            _register = register;
        }


        public void InitializeValidatorDelegates()
        {
            _fieldValidatorDel = new FieldValidatorDel(ValideField);

            _emailExistsDel = new EmailExistsDel(_register.EmailExists);

            _requiredValidDel = CommonFieldValidatorFunctions.RequiredFieldValidDel;
            _stringLengthValidDel = CommonFieldValidatorFunctions.StringLengthFieldValidDel;
            _dateValidDel = CommonFieldValidatorFunctions.DateFieldValidDel;
            _patternMatchValidDel = CommonFieldValidatorFunctions.PatternMatchValidDel;
            _compateFieldsValidDel = CommonFieldValidatorFunctions.FieldsCompareValidDel;

        }

        private bool ValideField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
        {
            fieldInvalidMessage = "";

            FieldConstant.UserRegistrationField userRegistrationField = (FieldConstant.UserRegistrationField)fieldIndex;

            switch(userRegistrationField){
                case FieldConstant.UserRegistrationField.EmailAddress:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for:{Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidPatterns.Email_Address_RegEx_Pattern)) ? $"You must enter a valid email address{Environment.NewLine}" : fieldInvalidMessage;
                    fieldInvalidMessage = (fieldInvalidMessage == "" && _emailExistsDel(fieldValue)) ? $"Email address already exist{Environment.NewLine}" : fieldInvalidMessage;
                    break;

                case FieldConstant.UserRegistrationField.FirstName:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLengthValidDel(fieldValue, UserName_Min_Length, UserName_Max_Length)) ? $"The length for field: {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)} must be between {UserName_Min_Length} and {UserName_Max_Lenth} " : "";
                    break;

                case FieldConstant.UserRegistrationField.LastName:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLengthValidDel(fieldValue, UserName_Min_Length, UserName_Max_Length)) ? $"The length for field: {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)} must be between {UserName_Min_Length} and {UserName_Max_Lenth} " : "";
                    break;
                case FieldConstant.UserRegistrationField.Password:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidPatterns.Strong_Password_RegEx_Pattern)) ? $"Your password must contain atleast 1 small-case letter, 1 capital letter, 1 special character and length must be between 6 - 10 characters {Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstant.UserRegistrationField.PasswordCompare:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_compateFieldsValidDel(fieldValue, fieldArray[(int)FieldConstant.UserRegistrationField.Password])) ? $"Your entry did not match your password{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstant.UserRegistrationField.DateOfBirth:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_dateValidDel(fieldValue, out DateTime validDateTime)) ? $"You did not enter a valid date{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstant.UserRegistrationField.PhoneNumber:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && _patternMatchValidDel(fieldInvalidMessage, CommonRegularExpressionValidPatterns.Uk_PhoneNumber_RegEx_Pattern)) ? $"You entered an invalid phone number{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstant.UserRegistrationField.PostCode:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && _patternMatchValidDel(fieldValue, CommonRegularExpressionValidPatterns.Uk_Post_Code_RegEx_Pattern)) ? $"Invalid postal code{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstant.UserRegistrationField.AddressFirstLine:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    break;
                case FieldConstant.UserRegistrationField.AddressSecondLine:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    break;
                case FieldConstant.UserRegistrationField.AddressCity:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for {Enum.GetName(typeof(FieldConstant.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    break;

                default:
                    throw new ArgumentException("This field does not exist");

            }

            return (fieldInvalidMessage == "");
        }
     }
}

