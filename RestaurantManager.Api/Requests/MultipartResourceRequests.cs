using RestaurantManager.Application.Services.Models;
using RestaurantManager.Domain;

namespace RestaurantManager.Api.Requests
{
    public class CreateRestaurantFormRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Cuisine { get; set; }
        public OpenClosed Status { get; set; }
        public IFormFile Image { get; set; }
    }

    public sealed class UpdateRestaurantFormRequest : CreateRestaurantFormRequest
    {
        public Guid Id { get; set; }
        public Guid? OwnerId { get; set; }
    }

    public class CreateMenuFormRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public MenuType Type { get; set; }
        public Guid? RestaurantId { get; set; }
        public IFormFile Image { get; set; }
    }

    public sealed class UpdateMenuFormRequest : CreateMenuFormRequest
    {
        public Guid? Id { get; set; }
    }

    public sealed class CreateMenuItemFormRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public Guid? MenuId { get; set; }
        public Guid? CategoryId { get; set; }
        public IFormFile Image { get; set; }
    }

    public sealed class UpdateMenuItemFormRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public Guid? CategoryId { get; set; }
        public IFormFile Image { get; set; }
    }

    public sealed class CreateEmployeeFormRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public EmployeeStatus Status { get; set; }
        public decimal Salary { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? RestaurantId { get; set; }
        public IFormFile Image { get; set; }
    }

    public sealed class UpdateEmployeeFormRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public EmployeeStatus Status { get; set; }
        public decimal Salary { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Image { get; set; }
    }

    public sealed class UpdateUserDetailsFormRequest
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Image { get; set; }
    }

    public static class FormFileExtensions
    {
        public static UploadFile ToUploadFile(this IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            return new UploadFile(file.OpenReadStream(), file.FileName, file.ContentType, file.Length);
        }
    }
}
