
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Test.Models.CustomValidations
{
    public class CustomEmailValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PatientMangementDBContext dBUserEntities = new PatientMangementDBContext();
                string email = value.ToString();

                if (!dBUserEntities.Users.Any(emailid => emailid.Username == email))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Email already exists");
                }
            }
            else
            {
                ErrorMessage = ErrorMessage ?? validationContext.DisplayName + "is required";
                return new ValidationResult(ErrorMessage);
            }
        }
    }

}