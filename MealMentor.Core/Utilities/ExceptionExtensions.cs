namespace MealMentor.Core.Utilities
    {
        public static class InvalidOperationExceptionExtensions
        {
            public static void ThrowIfNull(object? value, string? message = null)
            {
                if (value is null)
                    throw new InvalidOperationException(message ?? "Value cannot be null.");
            }
        }
    }
