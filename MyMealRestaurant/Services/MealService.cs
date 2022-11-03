using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MyMealRestaurant.Models;
using MyMealRestaurant.ServiceErrors;

namespace MyMealRestaurant.Services
{
    public class MealService : IMealService
    {
        //let store in memory. Ideally this would go into the DB
        private static readonly Dictionary<Guid, Meal> _meals = new Dictionary<Guid, Meal>();
        public ErrorOr<Created> CreateMeal(Meal meal)
        {
            _meals[meal.Id] = meal;
            return Result.Created;
        }

        ErrorOr<Deleted> IMealService.DeleteMeal(Guid id)
        {
            _meals.Remove(id);
            return Result.Deleted;
        }

        ErrorOr<Meal> IMealService.GetMeal(Guid id)
        {
            if (_meals.TryGetValue(id, out var meal))
                return meal;
            return Errors.Meal.NotFound;
        }


        ErrorOr<UpsertedMeal> IMealService.UpsertMeal(Meal meal)
        {
            var IsNewlyCreated = !_meals.ContainsKey(meal.Id);
            _meals[meal.Id] = meal;

            return new UpsertedMeal(IsNewlyCreated);
        }

        List<Meal> IMealService.GetMeals()
        {
            
            return _meals.Values.ToList();
        }
    }
}
