using RestaurantManager.Application.Handlers.MenuItems.Shared;

namespace RestaurantManager.Application.Handlers.MenuItems.Update
{
    public record UpdateMenuItemResponse : MenuItemBase
    {
        public Guid Id { get; set; }

        public Guid MenuId { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryText { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
