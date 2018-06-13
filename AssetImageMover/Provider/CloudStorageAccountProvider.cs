using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechmerVisionManager.Provider
{
    class CloudStorageAccountProvider
    {
        public static CloudStorageAccount getCloudStorageAccount()
        {
            return CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=techmervision;AccountKey=G4RyETzpPxh5pB7FKzgs/1Jim6U2Xtb5FHg3gO1wqUKuMUwGm9XUiYhMRBirzS/VZEquod5hgH60hJUv5FWdrg==;BlobEndpoint=https://techmervision.blob.core.windows.net/;TableEndpoint=https://techmervision.table.core.windows.net/;QueueEndpoint=https://techmervision.queue.core.windows.net/;FileEndpoint=https://techmervision.file.core.windows.net/");
        }
        public static CloudFileDirectory getDirByName(CloudFileDirectory parentDir, string dirName)
        {
            CloudFileDirectory userDir = parentDir.GetDirectoryReference(dirName);
            userDir.CreateIfNotExists();
            return userDir;
        }
        public static CloudFileDirectory getAssetDir(CloudStorageAccount _storageAccount, String UserName, String assetType)
        {
            CloudFileClient fileClient = _storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("techmervisionuserimages");
            if (!share.Exists())
            {
                throw new Exception("User Image Share Unavailable.");
            }

            CloudFileDirectory rootDir = share.GetRootDirectoryReference();
            CloudFileDirectory userDir = getDirByName(rootDir, UserName);
            CloudFileDirectory assetDir = getDirByName(userDir, assetType);
            return assetDir;
        }
    }
}
