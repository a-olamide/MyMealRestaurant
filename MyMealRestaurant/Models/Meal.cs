using ErrorOr;
using MyMealRestaurant.ServiceErrors;
using MyMealRestaurantContract.MyMeal;

namespace MyMealRestaurant.Models
{
    public class Meal
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public const int MinDescriptionLength = 50;
        public const int MaxDescriptionLength = 150;
        public Guid Id { get;  }
        public string Name { get;  }
        public string Description { get;  }
        public DateTime StartDateTime { get;  }
        public DateTime EndDateTime { get;  }
        public DateTime LastModifiesDateTime { get;  }
        public List<string> Savory { get;  }
        public List<string> Sweet { get;  }

        private Meal(Guid id, string name, string description, 
            DateTime startDateTime, DateTime endDateTime, 
            DateTime lastModifiesDateTime, List<string> savory, 
            List<string> sweet)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LastModifiesDateTime = lastModifiesDateTime;
            Savory = savory;
            Sweet = sweet;
        }
        public static ErrorOr<Meal> Create(
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            List<string> savory,
            List<string> sweet,
            Guid? id = null)
        {
            List<Error> errors = new();
            if (name.Length is < MinNameLength or > MaxNameLength)
            {
                errors.Add(Errors.Meal.InvalidName);
            }
            if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.Meal.InvalidDescription);
            }
            if(errors.Count > 0)
            {
                return errors;
            }

            return new Meal(
                id ?? Guid.NewGuid(),
                name,
                description,
                startDateTime,
                endDateTime,
                DateTime.UtcNow,
                savory,
                sweet);
        }

        public static ErrorOr<Meal> From(CreateMealRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                request.Savory,
                request.Sweet);
        }

        public static ErrorOr<Meal> From(Guid id, UpSertMealRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                request.Savory,
                request.Sweet,
                id);
        }
    }
}
