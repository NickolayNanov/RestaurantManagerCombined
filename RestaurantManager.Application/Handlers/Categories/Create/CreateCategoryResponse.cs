using RestaurantManager.Application.Handlers.Categories.Shared;

namespace RestaurantManager.Application.Handlers.Categories.Create
{
    public record CreateCategoryResponse : CategoryBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
