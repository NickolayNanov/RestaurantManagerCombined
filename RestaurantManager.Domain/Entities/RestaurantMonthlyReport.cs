namespace RestaurantManager.Domain.Entities
{
    public class RestaurantMonthlyReport : EntityBase
    {
        public Guid RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal Revenue { get; set; }

        public decimal Rating { get; set; }
    }
}
