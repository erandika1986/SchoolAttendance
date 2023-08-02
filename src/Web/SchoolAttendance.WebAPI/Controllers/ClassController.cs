using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Constants;
using SchoolAttendance.Infrastructure.Services;

namespace SchoolAttendance.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly IConfiguration _config;
        private readonly IClassService _classService;
        private readonly IIdentityService _identityService;

        public ClassController(ILogger<SubjectController> logger, IConfiguration config, IClassService classService, IIdentityService identityService)
        {
            this._logger = logger;
            this._config = config;
            this._classService = classService;
            this._identityService = identityService;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getClassList")]
        public PaginatedItemsViewModel<BasicClassDetailViewModel> GetClassList(string searchText, int currentPage, int pageSize, int academicYearId, int gradeId)
        {
            var response = _classService.GetClassList(searchText, currentPage, pageSize, academicYearId, gradeId);

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpPost]
        [Route("saveClassDetail")]
        public async Task<ResponseViewModel> SaveClassDetail(ClassViewModel vm)
        {
            var response = await _classService.SaveClassDetail(vm);

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpDelete]
        [Route("deleteClass/{id}")]
        public async Task<ResponseViewModel> DeleteClass(int id)
        {
            var response = await _classService.DeleteClass(id);

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getClassDetail/{gradeId}/{classId}")]
        public ClassViewModel GetClassDetail(int gradeId, int classId)
        {
            var response = _classService.GetClassDetail(gradeId, classId);

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getClassSubjectsForSelectedGrade/{gradeId}/{classId}")]
        public List<ClassSubjectViewModel> GetClassSubjectsForSelectedGrade(int gradeId)
        {
            var response = _classService.GetClassSubjectsForSelectedGrade(gradeId);

            return response;
        }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getClassMasterData")]
        public async Task<ClassMasterDataViewModel> GetClassMasterData()
        {
            var response = await _classService.GetClassMasterData();

            return response;
        }
    }
}
