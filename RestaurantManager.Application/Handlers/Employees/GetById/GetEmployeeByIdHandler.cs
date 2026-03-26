using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Employees.GetById
{
    internal class GetEmployeeByIdHandler(
        IMapper mapper,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
    {
        public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await dbContext.Employees
                .AsNoTracking()
                .ProjectTo<GetEmployeeByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            return response;
        }
    }
}
