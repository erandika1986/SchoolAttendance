using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{
  public class FileContainerViewModel
  {
    public FileContainerViewModel()
    {
      Files = new List<IFormFile>();
    }
    public List<IFormFile> Files { get; set; }
    public long Id { get; set; }
    public int Type { get; set; }

  }
}
