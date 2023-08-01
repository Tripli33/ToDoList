using System.ComponentModel.DataAnnotations;
namespace ToDoList.Domain.Validation;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        DateTime dateTime = (DateTime)(value ?? DateTime.Now);
        return dateTime > DateTime.Now;
    }
}