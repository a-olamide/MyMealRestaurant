using ErrorOr;

namespace MyMealRestaurant.ServiceErrors
{
    public static class Errors
    {
        public static class Meal
        {
            public static Error InvalidName => Error.Validation(
                code: "Meal.InvalidName",
                description: $"Meal name must be at least {Models.Meal.MinNameLength} character long and at most {Models.Meal.MaxNameLength} character long"
                );
            public static Error InvalidDescription => Error.Validation(
                code: "Meal.InvalidDescription",
                description: $"Meal description must be at least {Models.Meal.MinNameLength} character long and at most {Models.Meal.MaxNameLength} character long"
            );
            public static Error NotFound => Error.NotFound(
                code: "Meal.NotFound",
                description: "Meal not found"
                );
        }
    }
}
