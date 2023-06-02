using MediatR;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.User.Queries.GetStudentById
{
    public record GetStudentByIdQuery(int id) : IRequest<StudentViewModel>
    {
    }

    public class GetStundetByIdQUeryHandler : IRequestHandler<GetStudentByIdQuery, StudentViewModel>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetStundetByIdQUeryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }

        public async Task<StudentViewModel> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new StudentViewModel();

            var student = await _userQueryRepository.GetById(request.id, cancellationToken);

            if (student != null)
            {
                response.Id = student.Id;
                response.AcademicYearId = student.StudentClasses.FirstOrDefault(x => x.IsActive == true).Class.AcademicYear;
                response.FullName = student.FullName;
                response.ClassId = student.StudentClasses.FirstOrDefault(x => x.IsActive == true).ClassId;
                response.Gender = student.Gender;
                response.GradeId = student.StudentClasses.FirstOrDefault(x => x.IsActive == true).Class.GradeId;
                response.Username = student.Username;
            }

            return response;
        }
    }
}
