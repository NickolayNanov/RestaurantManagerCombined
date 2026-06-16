using RestaurantManager.Application.Handlers.Employees.Shared;

namespace RestaurantManager.Application.Handlers.Employees.Update
{
    public record UpdateEmployeeResponse : EmployeeBase
    {
        public Guid Id { get; set; }

        public Guid RestaurantId { get; set; }

        public string ImgUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
