using System.ComponentModel.DataAnnotations;

namespace Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is DateTime birthDate)
            {
                var today = DateTime.Today;
                var age = today.Year - birthDate.Year;

                // Корректировка, если ДР ещё не наступил в этом году
                if (birthDate.Date > today.AddYears(-age))
                    age--;

                if (age < _minimumAge)
                {
                    return new ValidationResult($"Минимальный возраст: {_minimumAge} лет");
                }
            }

            return ValidationResult.Success;
        }
    }
}
