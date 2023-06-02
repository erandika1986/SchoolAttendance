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

namespace SchoolAttendance.Application.Pipelines.User.Commands.UpdateUserPassword
{
    public record UpdateUserPasswordCommand(PasswordUpdateViewModel vm) : IRequest<ResponseViewModel>
    {
    }

    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, ResponseViewModel>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ILogger<UpdateUserPasswordCommandHandler> _logger;
        public UpdateUserPasswordCommandHandler(
            IUserCommandRepository userCommandRepository, 
            IUserQueryRepository userQueryRepository,
            ILogger<UpdateUserPasswordCommandHandler> logger
            )
        {
            this._userQueryRepository = userQueryRepository;
            this._userCommandRepository = userCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                if (!request.vm.NewPassword.Equals(request.vm.ConfirmPassword))
                {
                    response.IsSuccess = false;
                    response.Message = "Unable to update the password. Provided new password and confrim password does not match.";

                    return response;
                }

                var user = await _userQueryRepository.GetById(request.vm.Id, cancellationToken);

                if (user != null)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.vm.NewPassword);

                    await _userCommandRepository.UpdateAsync(user, cancellationToken);

                    response.IsSuccess = true;
                    response.Message = "User password has been updated successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Operation failed. User does not exists.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                response.IsSuccess = false;
                response.Message = "Operation failed. Exception has been occured.";
            }

            return response;
        }
    }
}
