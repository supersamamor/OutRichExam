using MediatR;
using OEMS.Application.Models.User;

namespace OEMS.Application.Commands.User.UpdateUser
{
    public class UpdateUserRequest : IRequest<UserModel>
    {
        public UserModel User { get; set; }
        public string Username { get; set; }        
    }
}
