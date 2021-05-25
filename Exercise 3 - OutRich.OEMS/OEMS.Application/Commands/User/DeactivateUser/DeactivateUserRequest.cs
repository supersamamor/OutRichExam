using MediatR;
using OEMS.Application.Models.User;

namespace OEMS.Application.Commands.User.DeactivateUser
{
    public class DeactivateUserRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
        public string Username { get; set; }        
    }
}
