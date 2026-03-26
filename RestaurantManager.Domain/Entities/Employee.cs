namespace RestaurantManager.Domain.Entities
{
    public class Employee : EntityBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public decimal Salary { get; set; }

        public string PhoneNumber { get; set; }

        public Guid RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
