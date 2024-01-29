using System;
namespace ClubMembershipApplication.FieldValidators
{
	public delegate bool FieldValidatorDel(int fieldIndex, string fieldValue, string[] fieldArray, out string InvalidMessage);
	public interface IFieldValidator
	{
		void InitializeValidatorDelegates();

		string[] FieldArray { get; }

		FieldValidatorDel validatorDel { get; }

	}
}

