using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Handlers.Employees.GetById;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Employees.ListByRestaurant
{
    internal class ListEmployeesByRestaurantHandler(
        IMapper mapper,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<ListEmployeesByRestaurantQuery, ListEmployeesByRestaurantResponse>
    {
        public async Task<ListEmployeesByRestaurantResponse> Handle(ListEmployeesByRestaurantQuery request, CancellationToken cancellationToken)
        {
            var employees = await dbContext.Employees
                .AsNoTracking()
                .ProjectTo<GetEmployeeByIdResponse>(mapper.ConfigurationProvider)
                .Where(e => e.RestaurantId == request.RestaurantId)
                .ToListAsync(cancellationToken);

            return new ListEmployeesByRestaurantResponse(employees);
        }
    }
}
