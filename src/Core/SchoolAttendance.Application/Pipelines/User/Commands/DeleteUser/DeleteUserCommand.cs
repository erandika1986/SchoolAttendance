using MediatR;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Commands.DeleteUser
{
    public record DeleteUserCommand(int id) : IRequest<ResponseViewModel>
    {
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseViewModel>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        
        public DeleteUserCommandHandler(
            IUserCommandRepository userCommandRepository, 
            IUserQueryRepository userQueryRepository ,
            ILogger<DeleteUserCommandHandler> logger)
        {
            this._userCommandRepository = userCommandRepository;
            this._userQueryRepository = userQueryRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = await _userQueryRepository.GetById(request.id, cancellationToken);

                user.IsActive = false;

                await _userCommandRepository.UpdateAsync(user, cancellationToken);

                response.IsSuccess = true;
                response.Message = "User record has been deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the user record.";
            }


            return response;
        }
    }
}
