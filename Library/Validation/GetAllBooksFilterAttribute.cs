using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Library.Validation
{
	public class GetAllBooksFilterAttribute : ValidationAttribute, IActionFilter
	{
		private readonly Type _enumType;
		private readonly string _propertyName = "order";

		public GetAllBooksFilterAttribute(Type enumType)
		{
			_enumType = enumType;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.ActionArguments.TryGetValue(_propertyName, out var myParamObj) && myParamObj is string myParam)
			{
				if (!IsValidEnumValue(myParam))
				{
					context.Result = new BadRequestResult();
					return;
				}
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}

		private bool IsValidEnumValue(string value)
		{
			return Enum.TryParse(_enumType, value, true, out _) && Enum.IsDefined(_enumType, value);
		}
	}
}
