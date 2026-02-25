using MediatR;

namespace RestaurantManager.Application.Handlers.Users.GetById
{
    public record GetUserByIdQuery(string Id) : IRequest<GetUserByIdResponse>;
}
