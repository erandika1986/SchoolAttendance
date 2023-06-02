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

namespace SchoolAttendance.Application.Pipelines.User.Commands.DeleteStudent
{
    public record DeleteStudentCommand(int id) : IRequest<ResponseViewModel>
    {
    }

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, ResponseViewModel>
    {

        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IStudentClassCommandRepository _studentClassCommandRepository;
        private readonly ILogger<DeleteStudentCommandHandler> _logger;

        public DeleteStudentCommandHandler(
            IUserCommandRepository userCommandRepository, 
            IUserQueryRepository userQueryRepository,
            IStudentClassCommandRepository studentClassCommandRepository,
            ILogger<DeleteStudentCommandHandler> logger)
        {
            this._userCommandRepository = userCommandRepository;
            this._userQueryRepository = userQueryRepository;
            this._studentClassCommandRepository = studentClassCommandRepository;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            try
            {
                var student =  await _userQueryRepository.GetById(request.id, cancellationToken);

                student.IsActive = true;

                var studentClass = student.StudentClasses.FirstOrDefault(x => x.IsActive == true);

                if (studentClass != null)
                {
                    studentClass.IsActive = false;
                    studentClass.RemovedDate = DateTime.UtcNow;

                    await _studentClassCommandRepository.UpdateAsync(studentClass, cancellationToken);
                }

                await _userCommandRepository.UpdateAsync(student, cancellationToken);

                response.IsSuccess = true;
                response.Message = "Student record has been deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the student record.";
            }

            return response;
        }
    }
}
