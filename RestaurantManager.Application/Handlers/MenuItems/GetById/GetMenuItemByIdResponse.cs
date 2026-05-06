using RestaurantManager.Application.Handlers.Categories.Shared;
using RestaurantManager.Application.Handlers.MenuItems.Shared;

namespace RestaurantManager.Application.Handlers.MenuItems.GetById
{
    public record GetMenuItemByIdResponse : MenuItemBase
    {
        public Guid Id { get; set; }

        public string ImgUrl { get; set; }

        public Guid MenuId { get; set; }

        public CategoryBaseWithId Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }

    public record CategoryBaseWithId : CategoryBase
    {
        public Guid Id { get; set; }
    }
}
