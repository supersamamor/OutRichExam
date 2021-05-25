using MediatR;
using OEMS.Application.Models.User;

namespace OEMS.Application.Queries.User.GetUserItem
{
    public class GetUserItemRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
    }
}
