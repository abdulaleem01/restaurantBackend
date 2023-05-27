using System;
namespace Models
{
    public class DishesModel
    {
        public int DishId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Price { get; set; }

        public string? ImageUrl { get; set; }

        public int? CookingTime { get; set; }
    }
}

