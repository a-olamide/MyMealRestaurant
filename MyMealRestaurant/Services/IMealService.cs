using ErrorOr;
using MyMealRestaurant.Models;

namespace MyMealRestaurant.Services
{
    public interface IMealService
    {
        ErrorOr<Created> CreateMeal(Meal meal);
        ErrorOr<Deleted> DeleteMeal(Guid id);
        ErrorOr<Meal> GetMeal(Guid id);
        List<Meal> GetMeals();
        ErrorOr<UpsertedMeal> UpsertMeal(Meal meal);
    }
}
