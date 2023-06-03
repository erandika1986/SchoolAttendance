using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Enums;
using SchoolAttendance.Domain.Helpers;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Queries.GetNotPublishedLesson
{
    public record GetNotPublishedLessonQuery(LessonFilter filter) : IRequest<PaginatedItemsViewModel<BasicLessonViewModel>>
    {
    }

    public class GetNotPublishedLessonQueryHandler : IRequestHandler<GetNotPublishedLessonQuery, PaginatedItemsViewModel<BasicLessonViewModel>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILessonQueryRepository _lessonQueryRepository;
        public GetNotPublishedLessonQueryHandler(ICurrentUserService currentUserService, ILessonQueryRepository lessonQueryRepository)
        {
            this._currentUserService = currentUserService;
            this._lessonQueryRepository = lessonQueryRepository;
        }

        public async Task<PaginatedItemsViewModel<BasicLessonViewModel>> Handle(GetNotPublishedLessonQuery request, CancellationToken cancellationToken)
        {
            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var vms = new List<BasicLessonViewModel>();

      

            var lessons = _lessonQueryRepository.GetLessonsByOwnerId(_currentUserService.UserId.Value);

            if (request.filter.AcademicYear > 0)
            {
                lessons = lessons.Where(l => l.AcademicYearId == request.filter.AcademicYear).OrderByDescending(x => x.CreatedOn);
            }

            if (request.filter.GradeId > 0)
            {
                lessons = lessons.Where(l => l.GradeId == request.filter.GradeId).OrderByDescending(x => x.CreatedOn);
            }

            if (request.filter.SubjectId > 0)
            {
                lessons = lessons.Where(l => l.SubjectId == request.filter.SubjectId).OrderByDescending(x => x.CreatedOn);
            }

            if (!string.IsNullOrEmpty(request.filter.SearchText))
            {
                lessons = lessons.Where(x => x.Name.Contains(request.filter.SearchText)).OrderByDescending(x => x.CreatedOn);
            }

            totalRecordCount = lessons.Count();
            totalPages = (double)totalRecordCount / request.filter.PageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var lessonList = await lessons.Skip((request.filter.CurrentPage - 1) * request.filter.PageSize).Take(request.filter.PageSize).ToListAsync();

            foreach (var item in lessonList)
            {
                var vm = new BasicLessonViewModel();

                if (item.AcademicYearId.HasValue)
                {
                    vm.AcademicYear = item.AcademicYearId.Value;
                }

                if (item.GradeId.HasValue)
                {
                    vm.GradeName = item.Grade.Name;
                }

                if (item.SubjectId.HasValue)
                {
                    vm.Subject = item.Subject.Name;
                }

                vm.Id = item.Id;
                vm.Name = item.Name;
                vm.Owner = item.LessonOwner.FullName;
                vm.Status = EnumHelper.GetEnumDescription((LessonStatus)item.Status);
                vm.CreatedOn = item.CreatedOn.ToString("MMM/dd/yyyy");

                vms.Add(vm);
            }

            var container = new PaginatedItemsViewModel<BasicLessonViewModel>(request.filter.CurrentPage, request.filter.PageSize, totalPageCount, totalRecordCount, vms);

            return container;
        }
    }
}
