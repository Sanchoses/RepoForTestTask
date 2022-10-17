using System.Net.Http.Headers;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using FilesUpload.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace FilesUpload.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase{

    //digitalocean example

    // private const int TIMEOUT = 2500;

    // // Digital Ocean settings
    // private static readonly string S3LoginRoot = "https://nyc3.digitaloceanspaces.com/";
    // private static readonly string S3BucketName = "";
    // private static readonly string AccessKey = "";
    // private static readonly string AccessKeySecret = "";
    // private static readonly string S3FolderName = "";

    // [HttpPost]
    // public IActionResult Post([FromForm] IFormFile file)
    // {
    //     bool Sucesso = false;

    //     try
    //     {
    //         AmazonS3Client? s3Client = new(new BasicAWSCredentials(AccessKey, AccessKeySecret), new AmazonS3Config
    //         {
    //             ServiceURL = S3LoginRoot,
    //             Timeout = TimeSpan.FromSeconds(TIMEOUT),
    //             MaxErrorRetry = 8,
    //         });

    //         TransferUtility fileTransferUtility = new(s3Client);

    //         TransferUtilityUploadRequest? fileTransferUtilityRequest = new()
    //         {
    //             BucketName = S3BucketName + @"/" + S3FolderName,
    //             Key = file.FileName,
    //             InputStream = file.OpenReadStream(),
    //             ContentType = file.ContentType,
    //             StorageClass = S3StorageClass.StandardInfrequentAccess,
    //             PartSize = 6291456,
    //             CannedACL = S3CannedACL.PublicRead,
    //         };

    //         fileTransferUtility.Upload(fileTransferUtilityRequest);

    //         Sucesso = true;
    //     }
    //     catch (Exception Ex)
    //     {
    //         Console.WriteLine(Ex.Message);
    //     }

    //     return Ok(new { Sucesso });
    // }


    [Route("uploadToPcUsingFormFile")]
    [HttpPost, DisableRequestSizeLimit]
    public async Task<IActionResult> Upload([FromForm]IFormFile file){
        try
        {
            var formCollection = await Request.ReadFormAsync();
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    [Route("uploadToPc")]
    [HttpPost, DisableRequestSizeLimit]
    public IActionResult Upload()
    {
        try
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }


    //Upload to Digital Ocean



    //Test Upload in GoogleDrive
    
    // [Route("Download")]
    // [HttpPost, DisableRequestSizeLimit]
    // public async Task<IActionResult> Download(string FileName){
    //     try
    //     {
    //         var formCollection = await Request.ReadFormAsync();
    //         var folderName = Path.Combine("Resources", "Images");
    //         var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
    //         if (file.Length > 0)
    //         {
    //             var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
    //             var fullPath = Path.Combine(pathToSave, fileName);
    //             var dbPath = Path.Combine(folderName, fileName);
    //             using (var stream = new FileStream(fullPath, FileMode.Create))
    //             {
    //                 file.CopyTo(stream);
    //             }
    //             return Ok(new { dbPath });
    //         }
    //         else
    //         {
    //             return BadRequest();
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex}");
    //     }
    // }
    
}