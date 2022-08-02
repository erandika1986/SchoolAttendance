using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using SchoolAttendance.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Services
{
  public class AzureBlobService : IAzureBlobService
  {
    private readonly string _storageConnectionString;

    public AzureBlobService(IConfiguration configuration)
    {
      _storageConnectionString = configuration["AzureStorage"];
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
      var container = new BlobContainerClient(_storageConnectionString, "lesson-contents");
      var createResponse = await container.CreateIfNotExistsAsync();
      if (createResponse != null && createResponse.GetRawResponse().Status == 201)
      {
        await container.SetAccessPolicyAsync(PublicAccessType.Blob);
      }
   
      var blob = container.GetBlobClient(fileName);

      await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

      await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });

      return blob.Uri.ToString();
    }
  }
}
