using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechmerVisionManager.Models;

namespace TechmerVisionManager.Provider
{
    class CORSSetter
    {
        private RuleType _rule;
        private CloudStorageAccount _storageAccount;

        public CORSSetter(RuleType rule)
        {
            _rule = rule;
            StorageCredentials creds = new StorageCredentials("techmervision", "G4RyETzpPxh5pB7FKzgs/1Jim6U2Xtb5FHg3gO1wqUKuMUwGm9XUiYhMRBirzS/VZEquod5hgH60hJUv5FWdrg==");
            _storageAccount = new CloudStorageAccount(creds, true);
            
        }
        public void Run()
        {

            //var fileClient = _storageAccount.CreateCloudFileClient();
            //var serviceProperties = fileClient.GetServiceProperties();

            var blobClient = _storageAccount.CreateCloudBlobClient();
            var blobServiceProps = blobClient.GetServiceProperties();

            //RemoveExistingCORSRules(serviceProperties);
            RemoveExistingCORSRules(blobServiceProps);

            var cors = new CorsRule();

            if (_rule == RuleType.Debug)
            {
                cors.AllowedOrigins.Add("*");
            }
            else if (_rule == RuleType.Production)
            {
                cors.AllowedOrigins.Add("techmervisionconnect.com,test-colorproject.azurewebsites.net,test-colorproject-preview.azurewebsites.net");
            }


            cors.AllowedMethods = CorsHttpMethods.Get;
            cors.MaxAgeInSeconds = 3600;

            //serviceProperties.Cors.CorsRules.Add(cors);
            //fileClient.SetServiceProperties(serviceProperties);
            blobServiceProps.Cors.CorsRules.Add(cors);
            blobClient.SetServiceProperties(blobServiceProps);
            Console.WriteLine("CORS Set");
        }

        private static void RemoveExistingCORSRules(ServiceProperties serviceProperties)
        {
            for (int i = serviceProperties.Cors.CorsRules.Count; i > 0; i--)
            {
                serviceProperties.Cors.CorsRules.RemoveAt(i - 1);
            }
        }
    }
}
