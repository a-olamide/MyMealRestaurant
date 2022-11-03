using Microsoft.AspNetCore.Mvc;

namespace MyMealRestaurant.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        [HttpGet()]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
