using System;
namespace Models
{
    public class DeliveriesModel
    {
        public int VisitId { get; set; }

        public int CustomerId { get; set; }

        public int TableId { get; set; }

        public string Date { get; set; } = null!;

        public string Time { get; set; } = null!;

        public IEnumerable<OrderDishModel>? orderDishModelsList { get; set; }
    }
}

