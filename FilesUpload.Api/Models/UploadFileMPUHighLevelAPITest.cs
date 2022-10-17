using Amazon.S3;
using Amazon.S3.Transfer;


namespace FilesUpload.Api.Models;

public static  class UploadFileMPUHighLevelAPITest
    {
        public static  string bucketName = "your spaces name";
        //public static string filePath = "d:\\test upload.txt";
        public static string endpoingURL = "https://fra1.digitaloceanspaces.com";
        public static IAmazonS3 s3Client;

        public static  bool UploadFile(string filePath, string fileName, string folderName)
        {
            var s3ClientConfig = new AmazonS3Config
            {
                ServiceURL = endpoingURL
            };
            s3Client = new AmazonS3Client(s3ClientConfig);
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName+ @"/" + folderName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 6291456, // 6 MB
                    Key = fileName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtility.Upload(fileTransferUtilityRequest);
                return true;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered ***. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
                if (e.Message.Contains("disposed"))
                    return true;
            }
            return false;
         }
    }