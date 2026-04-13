using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Employees.GetById
{
    public class GetEmployeeByIdQueryValidator : ApplicationValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();
        }
    }
}
