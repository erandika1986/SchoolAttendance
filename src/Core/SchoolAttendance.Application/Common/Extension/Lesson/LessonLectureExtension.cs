
using SchoolAttendance.Application.Responses;
using SchoolAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class LessonLectureExtension
  {
    public static LessonLectureViewModel ToVM(this LessonLecture model, LessonLectureViewModel vm = null)
    {
      if (vm == null)
      {
        vm = new LessonLectureViewModel();
      }

      vm.Id = model.Id;
      vm.Name = model.Name;
      vm.ContentType = model.LectureContentTypeId.HasValue?model.LectureContentTypeId.Value:0;
      vm.TopicId = model.TopicId;
      vm.Content = model.LectureContent;
      vm.MimeType = model.Mimetype;

      return vm;
    }
  }
}
