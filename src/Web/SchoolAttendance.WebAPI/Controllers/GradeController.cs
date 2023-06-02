using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.Grade.Commands.SaveGradeDetail;
using SchoolAttendance.Application.Pipelines.Grade.Queries.GetGradeList;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAttendance.WebAPI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class GradeController : ControllerBase
  {
        private readonly IMediator _mediator;

    public GradeController(IMediator mediator)
    {
      this._mediator = mediator;
    }

        [Authorize(Roles = AuthorizedRoles.Admin)]
        [HttpGet]
        [Route("getGradeList")]
        public async Task<List<GradeViewModel>> GetGradeList()
        {
            var response = await _mediator.Send(new GetGradeListQuery());

            return response;
        }

    [Authorize(Roles = AuthorizedRoles.Admin)]
    [HttpPost]
    [Route("saveGradeDetail")]
    public async Task<ResponseViewModel> SaveGradeDetail(GradeViewModel vm)
    {
            var response = await _mediator.Send(new SaveGradeDetailCommand(vm));

            return response;
        }
  }
}
