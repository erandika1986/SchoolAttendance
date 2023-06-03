using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Helpers;
using SchoolAttendance.Domain.Repositories.Command;
using SchoolAttendance.Domain.Repositories.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Pipelines.LessonDesign.Commands.UploadTopicContentFile
{
    public class UploadTopicContentFileCommand : IRequest<LessonLectureViewModel>
    {
        public LessonLectureViewModel Vm { get; set; }
        public IFormFile File { get; set; }
    }

    public class UploadTopicContentFileCommandHandler : IRequestHandler<UploadTopicContentFileCommand, LessonLectureViewModel>
    {
        private readonly IAzureBlobService _azureBlobService;
        private readonly ILessonLectureQueryRepository _lessonLectureQueryRepository;
        private readonly ILessonLectureCommandRepository _lessonLectureCommandRepository;
        private readonly ILogger<UploadTopicContentFileCommandHandler> _logger;

        public UploadTopicContentFileCommandHandler(
            IAzureBlobService azureBlobService, 
            ILessonLectureQueryRepository lessonLectureQueryRepository, 
            ILessonLectureCommandRepository lessonLectureCommandRepository, 
            ILogger<UploadTopicContentFileCommandHandler> logger)
        {
            this._lessonLectureQueryRepository = lessonLectureQueryRepository;
            this._lessonLectureCommandRepository = lessonLectureCommandRepository;
            this._azureBlobService = azureBlobService;
            this._logger = logger;
        }


        public async Task<LessonLectureViewModel> Handle(UploadTopicContentFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fileName = ContentDispositionHeaderValue.Parse(request.File.ContentDisposition).FileName.Trim('"');
                FileInfo fi = new FileInfo(fileName);
                string fileURL = await _azureBlobService.UploadAsync(request.File.OpenReadStream(), fileName, request.File.ContentType);

                var record = await _lessonLectureQueryRepository.GetById(request.Vm.Id, cancellationToken);
                record.LectureContent = fileURL;
                record.LectureContentTypeId = request.Vm.ContentType;
                record.Mimetype = FileHelper.GetMimeType(fi.Extension);

                await _lessonLectureCommandRepository.UpdateAsync(record, cancellationToken);

                request.Vm.Content = fileURL;
                request.Vm.MimeType = record.Mimetype;
            }
            catch (Exception ex)
            {
                //Log error 
            }


            return request.Vm;
        }
    }
}
