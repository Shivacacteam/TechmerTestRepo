using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechmerVisionManager.Models;

namespace TechmerVisionManager.Provider
{
    class AssetMover
    {
        private RuleType _rule;
        private String _userName;
        private String[] _assetArray;
        public AssetMover(RuleType rule, String UserName, String[] assetArray)
        {
            _rule = rule;
            _userName = UserName;
            _assetArray = assetArray;
        }

        public void Run()
        {
            int imageCount = 0;
            CloudStorageAccount _storageAccount = CloudStorageAccountProvider.getCloudStorageAccount();
            SqlConnection dbCon = WorkspaceProvider.GetWorkspaceContext(_rule);
            dbCon.Open();

            for (int i = _assetArray.Length; i > 0; i--)
            {
                String assetType = _assetArray[i];
                CloudFileDirectory assetDir = CloudStorageAccountProvider.getAssetDir(_storageAccount, _userName, assetType);
                SqlCommand command = GetAssetSQLQuery(dbCon, _userName, assetType);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    imageCount++;
                    String imageId = reader.GetInt64(0).ToString();
                    String imageString = reader.GetString(1);
                    if (imageString.StartsWith("data:image/png;base64,"))
                    {
                        imageString = imageString.Replace("data:image/png;base64,", "");
                    }
                    if (imageString.StartsWith("data:image/jpeg;base64,"))
                    {
                        imageString = imageString.Replace("data:image/jpeg;base64,", "");
                    }
                    byte[] imageBytes = Convert.FromBase64String(imageString);
                    CloudFile assetImage = assetDir.GetFileReference(imageId + ".png");
                    assetImage.UploadFromByteArray(imageBytes, 0, imageBytes.Length);
                    Console.WriteLine(assetImage.Uri.LocalPath);
                }
            }
            Console.WriteLine("Image Count: " + imageCount);
        }
        private static SqlCommand GetAssetSQLQuery(SqlConnection dbCon, String UserName, String assetType)
        {
            SqlCommand ret = new SqlCommand();
            ret.Connection = dbCon;
            ret.CommandType = CommandType.Text;
            switch (assetType)
            {
                case "Workspaces":
                    ret.CommandText = "Select Id, Image From Workspace Where Username = " + UserName;
                    break;
                case "Grids":
                    ret.CommandText = "Select Id, Image From Grid Inner Join Workspace On Grid.WorkspaceId = Workspace.Id Where Workspace.Username = " + UserName;
                    break;
                case "Products":
                    ret.CommandText = "Select Id, Image From Product Inner Join Workspace On Product.WorkspaceId = Workspace.Id Where Workspace.Username = " + UserName;
                    break;
                default:
                    break;
            }

            if (ret == null)
            {
                throw new Exception("Invalid Asset Type (SQL Query Unknown)");
            }

            return ret;
        }
    }
}
