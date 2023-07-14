using System.ComponentModel.DataAnnotations;
namespace ToDoList.Domain.Validation;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        DateTime dateTime = (DateTime)value;
        return dateTime >= DateTime.Now;
    }
}