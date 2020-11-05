using System.ComponentModel.DataAnnotations;

namespace Camera_Shop.Tests.Validations
{
	public class GreaterThanZero : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			// return true if value is a non-null number > 0, otherwise return false
			int i;
			return value != null && int.TryParse(value.ToString(), out i) && i > 0;
		}
	}
}