using System.Text.RegularExpressions;

namespace FieldValidatorAPI
{
    public delegate bool RequiredValidDel(string fieldValue);
    public delegate bool StringLengthValidDel(string fieldValue, int min, int max);
    public delegate bool DateValidDel(string fieldValue, out DateTime validDateTime);
    public delegate bool PatternMatchValidDel(string fieldValue, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldValue, string fieldValCompare);


    public class CommonFieldValidatorFunctions
    {
        private static RequiredValidDel _requiredValidDel = null;
        private static StringLengthValidDel _stringLengthValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchValidDel _patternMatchValidDel = null;
        private static CompareFieldsValidDel _compateFieldsValidDel = null;


        public static RequiredValidDel RequiredValidDel
        {
            get
            {
                if (_requiredValidDel == null)
                    _requiredValidDel = new RequiredValidDel(RequiredFieldValid);
                return _requiredValidDel;
            }
        }

        public static StringLengthValidDel StringLengthValidDel
        {
            get
            {
                if (_stringLengthValidDel == null)
                    _stringLengthValidDel = new StringLengthValidDel(StringFieldLengthValid);
                return _stringLengthValidDel;
            }
        }

        public static DateValidDel DateValidDel
        {
            get
            {
                if (_dateValidDel == null)
                    _dateValidDel = new DateValidDel(DateFieldValid);
                return _dateValidDel;
            }
        }

        public static PatternMatchValidDel PatternMatchValidDel
        {
            get
            {
                if (_patternMatchValidDel == null)
                    _patternMatchValidDel = new PatternMatchValidDel(FieldPatternValid);
                return _patternMatchValidDel;
            }
        }

        public static CompareFieldsValidDel CompareFieldsValidDel
        {
            get
            {
                if (_compateFieldsValidDel == null)
                    _compateFieldsValidDel = new CompareFieldsValidDel(FieldComparisonValid);
                return _compateFieldsValidDel;
            }
        }

        private static bool RequiredFieldValid(string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue)) return true;
            return false;
        }

        private static bool StringFieldLengthValid(string fieldValue, int min, int max)
        {
            if (fieldValue.Length >= min && fieldValue.Length <= max) return true;
            return false;
        }

        private static bool DateFieldValid(string dateTime, out DateTime validDateTime)
        {
            return (DateTime.TryParse(dateTime, out validDateTime)) ? true : false;
        }

        private static bool FieldPatternValid(string fieldValue, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(fieldValue);
        }

        private static bool FieldComparisonValid(string field1, string field2) => field1.Equals(field2);
        
    }
}





