using MediatR;
using OEMS.Application.Models.User;

namespace OEMS.Application.Commands.User.ActivateUser
{
    public class ActivateUserRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
        public string Username { get; set; }        
    }
}
