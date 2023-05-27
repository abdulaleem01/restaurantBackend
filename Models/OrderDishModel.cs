using System;
namespace Models
{
    public class OrderDishModel
    {
        public int? OrderId { get; set; }

        public int? VisitId { get; set; }

        public int? DishId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? Price { get; set; }

        public string? ImageUrl { get; set; }

        public int? CookingTime { get; set; }
    }
}

