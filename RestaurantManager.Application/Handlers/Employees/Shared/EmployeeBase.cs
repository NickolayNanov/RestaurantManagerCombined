using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Employees.Shared
{
    public record EmployeeBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public decimal Salary { get; set; }

        public string PhoneNumber { get; set; }
    }
}
