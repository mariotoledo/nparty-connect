using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Helpers
{
    public class AmazonS3Manager
    {
        private AmazonS3Client client;

        private static string bucketName = "connecteventimages";

        /// <summary>
        /// 
        /// </summary>
        public AmazonS3Manager()
        {
            this.client = new AmazonS3Client(Amazon.RegionEndpoint.SAEast1);
        }

        /// <summary>
        /// Checking the bucket existence, submits an file to Amazon S3 database
        /// </summary>
        /// <param name="OriginalImage">The original inputStream to submit</param>
        /// <param name="fileType">the type refered to the file</param>
        public void Save(string key, System.IO.Stream stream)
        {
            // Create a PutObject request
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = key,
                InputStream = stream,
                CannedACL = S3CannedACL.PublicRead
            };

            client.PutObject(request);
        }

        private void CreateBucket(string bucketName)
        {
            PutBucketRequest bucketRequest = new PutBucketRequest();
            bucketRequest.BucketName = bucketName;
            bucketRequest.BucketRegion = S3Region.SAE1;
            bucketRequest.CannedACL = S3CannedACL.PublicRead;

            client.PutBucket(bucketRequest);
        }

        private bool Exists(string bucketName)
        {
            return Amazon.S3.Util.AmazonS3Util.DoesS3BucketExist(bucketName, client);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        public void Delete(string photoUrl)
        {
            string[] splited = photoUrl.Split(new char[] { '/' });
            string key = "";
            if (splited.Length > 0)
                key = splited[splited.Length - 1];

            DeleteObjectRequest request = new DeleteObjectRequest()
            {
                BucketName = bucketName,
                Key = key
            };

            if (Exists(request.BucketName))
                client.DeleteObject(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetUrl(string key)
        {
            return string.Concat("https://s3-sa-east-1.amazonaws.com/", bucketName, "/", key);
        }

        public string[] List(string key)
        {
            List<string> objectsFound = new List<string>();

            ListObjectsRequest request = new ListObjectsRequest()
            {
                BucketName = bucketName,
                Prefix = key
            };

            if (!Exists(request.BucketName))
                return new string[] { };

            ListObjectsResponse response = client.ListObjects(request);

            foreach (S3Object objFound in response.S3Objects)
                objectsFound.Add(GetUrl(objFound.Key));

            return objectsFound.ToArray();
        }
    }
}