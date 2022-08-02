using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Application.Responses
{

  public class MasterDataUploadResponse
  {
    public MasterDataUploadResponse()
    {
      Results = new List<MasterDataFileValidateResult>();
    }

    public bool IsSucccess { get; set; }
    public List<MasterDataFileValidateResult> Results { get; set; }

  }

  public class MasterDataFileValidateResult
  {
    public bool IsSuccess { get; set; }
    public string ValidateMessage { get; set; }
  }
}
