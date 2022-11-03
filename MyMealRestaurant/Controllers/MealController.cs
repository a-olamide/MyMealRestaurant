using ErrorOr;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMealRestaurant.Models;
using MyMealRestaurant.ServiceErrors;
using MyMealRestaurant.Services;
using MyMealRestaurantContract.MyMeal;

namespace MyMealRestaurant.Controllers
{
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class MealController : ApiController
    {
        private readonly IMealService _mealService;
        private readonly ILogger<MealController> _logger;

        public MealController(IMealService mealService, ILogger<MealController> logger)
        {
            _mealService = mealService;
            _logger = logger;
        }

        [HttpPost()]
        public IActionResult CreateMeal(CreateMealRequest request)
        {
            _logger.LogWarning("Testing the mic");
            ErrorOr<Meal> requestToMealResult = Meal.From(request);

            if (requestToMealResult.IsError)
                return Problem(requestToMealResult.Errors);

            var meal = requestToMealResult.Value;
            //save to DB
            ErrorOr<Created> createMealResult = _mealService.CreateMeal(meal);

            return createMealResult.Match(
                created => CreatedAtGetMeal(meal),
                errors => Problem(errors));
        }

       
        [HttpGet("{id:guid}")]
        public IActionResult GetMeal(Guid id)
        {
            _logger.LogWarning("Testing the mic");
            //this is bcos we are using the ErrorOr package to handle error
            ErrorOr<Meal> getMealResult = _mealService.GetMeal(id);
            return getMealResult.Match(
                meal => Ok(MapMealResponse(meal)),
                errors => Problem(errors));

            //here is another method of implementing
            //if (getMealResult.IsError &&
            //    getMealResult.FirstError == Errors.Meal.NotFound)
            //    return NotFound();
            //var meal = getMealResult.Value;
            //MealResponse response = MapMealResponse(meal);
            //return Ok(response);
        }

        [HttpGet()]
        public IActionResult GetMeals()
        {
            _logger.LogWarning("Testing the mic");
            var meals = _mealService.GetMeals();
            //var response = new MealResponse()
            //{
            //    Id = meal.Id,
            //    Name = meal.Name,
            //    Description = meal.Description,
            //    StartDateTime = meal.StartDateTime,
            //    EndDateTime = meal.EndDateTime,
            //    LastModifiesDateTime = meal.LastModifiesDateTime,
            //    Savory = meal.Savory,
            //    Sweet = meal.Sweet
            //};


            return Ok(meals.ToList());
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpsertMeal(Guid id, UpSertMealRequest request)
        {
            ErrorOr<Meal> requestToMealResult =  Meal.From(id, request);
            if (requestToMealResult.IsError)
                return Problem(requestToMealResult.Errors);

            var meal = requestToMealResult.Value;

           ErrorOr<UpsertedMeal> upsertedResult =  _mealService.UpsertMeal(meal);
            //return 201 if a new meal is created
            return upsertedResult.Match(
                upserted => upserted.IsNewlyCreated ? CreatedAtGetMeal(meal) : NoContent(),
                errors => Problem(errors));
        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteMeal(Guid id)
        {
           ErrorOr<Deleted> deletedResult = _mealService.DeleteMeal(id);

            return deletedResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }

        private IActionResult CreatedAtGetMeal(Meal meal)
        {
            return CreatedAtAction(
               actionName: nameof(GetMeal),
               routeValues: new { id = meal.Id },
               value: MapMealResponse(meal));
        }

        private static MealResponse MapMealResponse(Meal meal)
        {
            return new MealResponse()
            {
                Id = meal.Id,
                Name = meal.Name,
                Description = meal.Description,
                StartDateTime = meal.StartDateTime,
                EndDateTime = meal.EndDateTime,
                LastModifiesDateTime = meal.LastModifiesDateTime,
                Savory = meal.Savory,
                Sweet = meal.Sweet
            };
        }

    }
}
