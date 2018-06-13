using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using TechmerVision;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TechmerVision.Models;
using System.IO;
using System.Threading.Tasks;

namespace TechmerVision.Providers
{
    enum CloudStorageType
    {
        File,
        Blob
    }
    interface iStorageProivder
    {
        List<Product> getImages(string UserName, string assetType, List<Product> products);
        List<Workspace> getImages(string UserName, string assetType, List<Workspace> workspaces);
        List<Grid> getImages(string UserName, string assetType, List<Grid> grids);
        List<SampleInspiration> getImages(string UserName, string assetType, List<SampleInspiration> sampleInspirations);
        List<ProductTemplate> getImages(string UserName, string assetType, List<ProductTemplate> productTemplates, Boolean includeProductTemplateColorImages);
        List<ProductTemplateColor> getImages(string UserName, string assetType, List<ProductTemplateColor> productTemplateColors);
        String getImage(String UserName, String assetType, Asset asset);
        Product getImage(String UserName, String assetType, Product product);
        String getImage(String UserName, String assetType, SampleInspiration sampleInspiration);
        ProductTemplate getImage(String UserName, String assetType, ProductTemplate productTemplate, Boolean includeProductTemplateColorImage);
        ProductTemplateColor getImage(String UserName, String asseType, ProductTemplateColor productTemplateColor);


        List<Grid> saveImages(string UserName, List<Grid> grids);
        List<Product> saveImages(string UserName, List<Product> products);
        List<Workspace> saveImages(string UserName, List<Workspace> workspaces);
        
        Product saveImage(string UserName, string assetType, Product product, Boolean copyFromLink);
        string saveImage(string UserName, string assetType, Asset asset, Boolean copyFromLink);
        string saveImage(string UserName, string assetType, SampleInspiration sampleInspiration);
        string saveImage(string UserName, string assetType, ProductTemplate productTemplate);
        string saveImage(string UserName, string assetType, ProductTemplateColor productTemplateColor);
        string saveImage(string UserName, string assetType, HttpPostedFileBase image);

        void deleteImage(string UserName, string assetType, Asset asset);
        void deleteImage(string UserName, string assetType, Product product);
        void deleteImage(string UserName, string assetType, SampleInspiration sampleInspiration);
        void deleteImage(string UserName, string assetType, ProductTemplate productTemplate);
        void deleteImage(string UserName, string assetType, ProductTemplateColor productTemplateColor);

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A cloud storage provider. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class CloudStorageProvider
    {
        private CloudStorageAccount _storageAccount;
        private ApplicationUserManager _userManager;

        private const CloudStorageType StorageType = CloudStorageType.Blob;
        private iStorageProivder store;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="userManager">  Manager for user. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public CloudStorageProvider(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            store = initStore(_storageAccount, UserManager);
        }

        private iStorageProivder initStore(CloudStorageAccount account, ApplicationUserManager UserManager)
        {
            iStorageProivder ret;
            switch (StorageType)
            {
                case CloudStorageType.File:
                    //ret = new AzureFileStorageProvider(account, UserManager);
                    break;
                case CloudStorageType.Blob:
                    ret = new AzureBlobStorageProvider(account, UserManager);
                    break;
            }
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the manager for user. </summary>
        ///
        /// <value> The user manager. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        

        #region Get

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="products">     The products. </param>
        ///
        /// <returns>   The images. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Product> getImages(string UserName, string assetType, List<Product> products)
        {
            List<Product> ret = new List<Product>();
            ret = store.getImages(UserName, assetType, products);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="workspaces">   The workspaces. </param>
        ///
        /// <returns>   The images. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Workspace> getImages(string UserName, string assetType, List<Workspace> workspaces)
        {
            List<Workspace> ret = new List<Workspace>();
            ret = store.getImages(UserName, assetType, workspaces);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="grids">        The grids. </param>
        ///
        /// <returns>   The images. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Grid> getImages(string UserName, string assetType, List<Grid> grids)
        {
            List<Grid> ret = new List<Grid>();
            ret = store.getImages(UserName, assetType, grids);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">             Name of the user. </param>
        /// <param name="assetType">            Type of the asset. </param>
        /// <param name="sampleInspirations">   The sample inspirations. </param>
        ///
        /// <returns>   The images. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<SampleInspiration> getImages(string UserName, string assetType, List<SampleInspiration> sampleInspirations)
        {
            List<SampleInspiration> ret = new List<SampleInspiration>();
            ret = store.getImages(UserName, assetType, sampleInspirations);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">                             Name of the user. </param>
        /// <param name="assetType">                            Type of the asset. </param>
        /// <param name="productTemplates">                     The product templates. </param>
        /// <param name="includeProductTemplateColorImages">    True to include, false to exclude the
        ///                                                     product template color images. </param>
        ///
        /// <returns>   The images. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<ProductTemplate> getImages(string UserName, string assetType, List<ProductTemplate> productTemplates, Boolean includeProductTemplateColorImages)
        {
            List<ProductTemplate> ret = new List<ProductTemplate>();
            ret = store.getImages(UserName, assetType, productTemplates, includeProductTemplateColorImages);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">                 Name of the user. </param>
        /// <param name="assetType">                Type of the asset. </param>
        /// <param name="productTemplateColors">    List of colors of the product templates. </param>
        ///
        /// <returns>   The images. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<ProductTemplateColor> getImages(string UserName, string assetType, List<ProductTemplateColor> productTemplateColors)
        {
            List<ProductTemplateColor> ret = new List<ProductTemplateColor>();
            ret = store.getImages(UserName, assetType, productTemplateColors);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="asset">        The asset. </param>
        ///
        /// <returns>   The image. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String getImage(String UserName, String assetType, Asset asset)
        {
            String ret = String.Empty;
            ret = store.getImage(UserName, assetType, asset);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="product">      The product. </param>
        ///
        /// <returns>   The image. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Product getImage(String UserName, String assetType, Product product)
        {
            product = store.getImage(UserName, assetType, product);
            return product;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">             Name of the user. </param>
        /// <param name="assetType">            Type of the asset. </param>
        /// <param name="sampleInspiration">    The sample inspiration. </param>
        ///
        /// <returns>   The image. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String getImage(String UserName, String assetType, SampleInspiration sampleInspiration)
        {
            String ret = String.Empty;
            ret = store.getImage(UserName, assetType, sampleInspiration);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">                         Name of the user. </param>
        /// <param name="assetType">                        Type of the asset. </param>
        /// <param name="productTemplate">                  The product template. </param>
        /// <param name="includeProductTemplateColorImage"> True to include, false to exclude the product
        ///                                                 template color image. </param>
        ///
        /// <returns>   The image. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ProductTemplate getImage(String UserName, String assetType, ProductTemplate productTemplate, Boolean includeProductTemplateColorImage)
        {
            productTemplate = store.getImage(UserName, assetType, productTemplate, includeProductTemplateColorImage);
            return productTemplate;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">             Name of the user. </param>
        /// <param name="assetType">            Type of the asset. </param>
        /// <param name="productTemplateColor"> The product template color. </param>
        ///
        /// <returns>   The image. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ProductTemplateColor getImage(String UserName, String assetType, ProductTemplateColor productTemplateColor)
        {
            productTemplateColor = store.getImage(UserName, assetType, productTemplateColor);
            return productTemplateColor;
        }
        #endregion


        #region Save

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName"> Name of the user. </param>
        /// <param name="grids">    The grids. </param>
        ///
        /// <returns>   A List&lt;Grid&gt; </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Grid> saveImages(string UserName, List<Grid> grids)
        {
            List<Grid> ret = new List<Grid>();
            ret = store.saveImages(UserName, grids);
            return ret;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName"> Name of the user. </param>
        /// <param name="products"> The products. </param>
        ///
        /// <returns>   A List&lt;Grid&gt; </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Product> saveImages(string UserName, List<Product> products)
        {
            List<Product> ret = new List<Product>();
            ret = store.saveImages(UserName, products);
            return ret;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves the images. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="workspaces">   The workspaces. </param>
        ///
        /// <returns>   A List&lt;Grid&gt; </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Workspace> saveImages(string UserName, List<Workspace> workspaces)
        {
            List<Workspace> ret = new List<Workspace>();
            ret = store.saveImages(UserName, workspaces);
            return ret;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="product">      The product. </param>
        /// <param name="copyFromLink"> True to copy from link. </param>
        ///
        /// <returns>   A Product. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Product saveImage(string UserName, string assetType, Product product, Boolean copyFromLink)
        {
            product = store.saveImage(UserName, assetType, product, copyFromLink);
            return product;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="asset">        The asset. </param>
        /// <param name="copyFromLink"> True to copy from link. </param>
        ///
        /// <returns>   A Product. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string saveImage(string UserName, string assetType, Asset asset, Boolean copyFromLink)
        {
            string ret = "";
            ret = store.saveImage(UserName, assetType, asset, copyFromLink);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">             Name of the user. </param>
        /// <param name="assetType">            Type of the asset. </param>
        /// <param name="sampleInspiration">    The sample inspiration. </param>
        ///
        /// <returns>   A Product. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string saveImage(string UserName, string assetType, SampleInspiration sampleInspiration)
        {
            string ret = "";
            ret = store.saveImage(UserName, assetType, sampleInspiration);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">         Name of the user. </param>
        /// <param name="assetType">        Type of the asset. </param>
        /// <param name="productTemplate">  The product template. </param>
        ///
        /// <returns>   A Product. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string saveImage(string UserName, string assetType, ProductTemplate productTemplate)
        {
            string ret = "";
            ret = store.saveImage(UserName, assetType, productTemplate);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">             Name of the user. </param>
        /// <param name="assetType">            Type of the asset. </param>
        /// <param name="productTemplateColor"> The product template color. </param>
        ///
        /// <returns>   A Product. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string saveImage(string UserName, string assetType, ProductTemplateColor productTemplateColor)
        {
            string ret = "";
            ret = store.saveImage(UserName, assetType, productTemplateColor);
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves an image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="image">        The image. </param>
        ///
        /// <returns>   A Product. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string saveImage(string UserName, string assetType, HttpPostedFileBase image)
        {
            string ret = "";
            ret = store.saveImage(UserName, assetType, image);
            return ret;
        }
        #endregion Save


        #region Delete

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="asset">        The asset. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void deleteImage(string UserName, string assetType, Asset asset)
        {
            store.deleteImage(UserName, assetType, asset);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">     Name of the user. </param>
        /// <param name="assetType">    Type of the asset. </param>
        /// <param name="product">      The product. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void deleteImage(String UserName, string assetType, Product product)
        {
            store.deleteImage(UserName, assetType, product);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">             Name of the user. </param>
        /// <param name="assetType">            Type of the asset. </param>
        /// <param name="sampleInspiration">    The sample inspiration. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void deleteImage(string UserName, string assetType, SampleInspiration sampleInspiration)
        {
            store.deleteImage(UserName, assetType, sampleInspiration);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">         Name of the user. </param>
        /// <param name="assetType">        Type of the asset. </param>
        /// <param name="productTemplate">  The product template. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void deleteImage(string UserName, string assetType, ProductTemplate productTemplate)
        {
            store.deleteImage(UserName, assetType, productTemplate);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes the image. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="UserName">             Name of the user. </param>
        /// <param name="assetType">            Type of the asset. </param>
        /// <param name="productTemplateColor"> The product template color. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void deleteImage(string UserName, string assetType, ProductTemplateColor productTemplateColor)
        {
            store.deleteImage(UserName, assetType, productTemplateColor);
        }
        #endregion

    }


    class AzureBlobStorageProvider : iStorageProivder
    {
        private ApplicationUserManager _userManger;
        private CloudStorageAccount _storageAccount;
        private Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPolicy _policy;
        public AzureBlobStorageProvider(CloudStorageAccount StorageAccount, ApplicationUserManager UserManager)
        {
            _storageAccount = StorageAccount;
            _userManger = UserManager;
            _policy = new Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddDays(1),
            }; 
        }

        #region Get
        public List<Workspace> getImages(string UserName, string assetType, List<Workspace> workspaces)
        {
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            for (int i = 0; i < workspaces.Count; i++)
            {
                tasks.Add(Task.Factory.StartNew((Object obj) =>
                {
                    var data = (dynamic)obj;
                    workspaces[data.i].Image = getImage(assetDir, workspaces[data.i].Filename.ToString());
                }, new { i = i }));
            }
            Task.WaitAll(tasks.ToArray());
            return workspaces;
        }
        public List<Grid> getImages(string UserName, string assetType, List<Grid> grids)
        {
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            for (int i = 0; i < grids.Count; i++)
            {
                tasks.Add(Task.Factory.StartNew((Object obj) =>
                {
                    var data = (dynamic)obj;
                    grids[data.i].Image = getImage(assetDir, grids[data.i].Filename.ToString());
                }, new { i = i }));
            }
            Task.WaitAll(tasks.ToArray());

            return grids;
        }
        public List<Product> getImages(string UserName, string assetType, List<Product> products)
        {
            for(int i = 0; i < products.Count; i++)
            {
                products[i] = getImage(UserName, assetType, products[i]);
            }
            return products;
        }
        public List<ProductColor> getImages(string UserName, string assetType, List<ProductColor> productColors)
        {
            //List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            for (int i = 0; i < productColors.Count; i++)
            {

                /*tasks.Add(Task.Factory.StartNew((Object obj) =>
                {
                    var data = (dynamic)obj;
                    productColors[data.i].Image = getImage(assetDir, productColors[data.i].Filename.ToString());
                }, new { i = i }));
                */
                productColors[i].Image = getImage(assetDir, productColors[i].Filename.ToString());
            }
            //Task.WaitAll(tasks.ToArray());

            return productColors;
        }
        public List<SampleInspiration> getImages(string UserName, string assetType, List<SampleInspiration> sampleInspirations)
        {
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            for(int i = 0; i< sampleInspirations.Count; i++)
            {
                tasks.Add(Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        sampleInspirations[data.i].Image = getImage(assetDir, sampleInspirations[data.i].Filename.ToString());
                    }, new { i = i}));
            }
            Task.WaitAll(tasks.ToArray());

            return sampleInspirations;
        }
        public List<ProductTemplate> getImages(string UserName, string assetType, List<ProductTemplate> productTemplates, Boolean includeProductTemplateColorImages)
        {
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            for (int i = 0; i < productTemplates.Count; i++)
            {
                /*tasks.Add(Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        productTemplates[data.i].Image = getImage(assetDir, productTemplates[data.i].FileName.ToString());
                        if (includeProductTemplateColorImages && productTemplates[data.i] != null && productTemplates[data.i].ProductTemplateColors != null)
                        {
                            if(productTemplates[data.i].ProductTemplateColors.Count > 0)
                            {
                                productTemplates[data.i].ProductTemplateColors = getImages("Techmer", "ProductTemplateColors", productTemplates[data.i].ProductTemplateColors);
                            }
                        }
                    }, new { i = i }));
                    */

                productTemplates[i].Image = getImage(assetDir, productTemplates[i].FileName.ToString());
                if (includeProductTemplateColorImages && productTemplates[i] != null && productTemplates[i].ProductTemplateColors != null)
                {
                    if (productTemplates[i].ProductTemplateColors.Count > 0)
                    {
                        productTemplates[i].ProductTemplateColors = getImages("Techmer", "ProductTemplateColors", productTemplates[i].ProductTemplateColors.ToList());
                    }
                }
            }
            //Task.WaitAll(tasks.ToArray());

            return productTemplates;
        }
        public List<ProductTemplateColor> getImages(string UserName, string assetType, List<ProductTemplateColor> productTemplateColors)
        {
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            for(int i = 0; i < productTemplateColors.Count; i++)
            {
                if(productTemplateColors[i].FileName != null) { 
                    tasks.Add(Task.Factory.StartNew((Object obj) =>
                        {
                            var data = (dynamic)obj;
                            productTemplateColors[data.i].Image = getImage(assetDir, productTemplateColors[data.i].FileName.ToString());
                        }, new { i = i }));
                }
            }
            Task.WaitAll(tasks.ToArray());

            return productTemplateColors;
        }
        public String getImage(String UserName, String assetType, Asset asset)
        {

            String ret = String.Empty;
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            ret = getImage(assetDir, asset.Filename.ToString());
            return ret;
        }
        public Product getImage(string UserName, string assetType, Product product)
        {
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            product.Image = getImage(assetDir, product.Filename.ToString());
            product.ProductColors = getImages(UserName, product.ProductColors.First().GetType().Name.ToString(), product.ProductColors.ToList());
            product.ProductTemplate = getImage("Techmer", "ProductTemplates", product.ProductTemplate, true);
            return product;
        }
        public String getImage(String UserName, String assetType, SampleInspiration sampleInspiration)
        {
            String ret = String.Empty;
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            ret = getImage(assetDir, sampleInspiration.Filename);
            return ret;
        }
        public ProductTemplate getImage(String UserName, String assetType, ProductTemplate productTemplate, Boolean includeProductTemplateColorImage)
        {
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            productTemplate.Image = getImage(assetDir, productTemplate.FileName);
            if (includeProductTemplateColorImage)
            {
                productTemplate.ProductTemplateColors = getImages(UserName, "ProductTemplateColors", productTemplate.ProductTemplateColors.ToList());
            }
            return productTemplate;
        }
        public ProductTemplateColor getImage(String UserName, String assetType, ProductTemplateColor productTemplateColor)
        {
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            productTemplateColor.Image = getImage(assetDir, productTemplateColor.FileName);
            return productTemplateColor;
        }
        private string getImage(CloudBlobDirectory assetDir, string imageName)
        {
            try
            {
                string ret = "";
                CloudBlob assetImage = assetDir .GetBlobReference(imageName + ".png");
                if (assetImage.Exists())
                {
                    ret = assetImage.Uri.AbsoluteUri + assetImage.GetSharedAccessSignature(_policy);
                }
                return ret;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        #endregion


        #region Save
        public List<Workspace> saveImages(string UserName, List<Workspace> workspaces)
        {
            List<Workspace> ret = new List<Workspace>();
            CloudBlobContainer share = getContainerByName("techmervision");
            if (!share.Exists())
            {
                throw new Exception("User Image Share Unavailable.");
            }

            CloudBlobDirectory assetDir = getAssetDir(UserName, "Workspaces");

            foreach (Workspace workspace in workspaces)
            {

                workspace.Image = saveImage(assetDir, workspace.Image, workspace.Id.ToString());
                ret.Add(workspace);
            }

            return ret;

        }
        public List<Grid> saveImages(string UserName, List<Grid> grids)
        {
            List<Grid> ret = new List<Grid>();
            CloudBlobContainer container = getContainerByName("techmervision");
            if (!container.Exists())
            {
                throw new Exception("User Image Share Unavailable.");
            }


            CloudBlobDirectory assetDir = getAssetDir(UserName, "Grids");

            foreach (Grid grid in grids)
            {

                grid.Image = saveImage(assetDir, grid.Image, grid.Id.ToString());
                ret.Add(grid);
            }

            return ret;

        }
        public List<Product> saveImages(string UserName, List<Product> products)
        {

            for (int i = 0; i < products.Count; i++)
            {

                products[i] = saveImage(UserName, "Products", products[i], false);
            }

            return products;

        }
        public List<ProductColor> saveImages(string UserName, string assetType, List<ProductColor> productColors, Boolean copyImageFromLink)
        {
            for(int i = 0; i < productColors.Count; i++)
            {
                productColors[i].Filename = saveImage(UserName, "ProductColor", productColors[i], copyImageFromLink);
            }

            return productColors;
        }
        public string saveImage(string UserName, string assetType, ProductColor productColor, Boolean copyFromLink)
        {
            string filename = "";
            if (productColor.Filename != null && !productColor.Filename.Equals(String.Empty))
            {
                filename = productColor.Filename;
            }
            else
            {
                filename = Guid.NewGuid().ToString();
            }
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            if (copyFromLink && productColor.Image.StartsWith("http"))
            {
                CloudBlockBlob file = getBlobFromLink(productColor.Image);
                CloudBlockBlob destination = assetDir.GetBlockBlobReference(filename + ".png");
                destination.StartCopy(file);

            }
            else
            {
                filename = saveImage(assetDir, productColor.Image, filename);
            }
            return filename;
        }
        public string saveImage(string UserName, string assetType, Asset asset, Boolean copyFromLink = false)
        {

            string filename;

            if(asset.Filename != null && !asset.Filename.Equals(String.Empty))
            {
                filename = asset.Filename;
            }
            else
            {
                filename = Guid.NewGuid().ToString();
            }
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            if (copyFromLink && asset.Image.StartsWith("http")) {
                CloudBlockBlob file = getBlobFromLink(asset.Image);
                CloudBlockBlob destination = assetDir.GetBlockBlobReference(filename + ".png");
                destination.StartCopy(file);

            }
            else { 
                filename = saveImage(assetDir, asset.Image, filename);
            }
            return filename;
        }
        public Product saveImage(string UserName, string assetType, Product product, Boolean copyFromLink)
        {
            product.Filename = saveImage(UserName, assetType, (Asset) product, copyFromLink);
            product.ProductColors = saveImages(UserName, product.ProductColors.First().GetType().Name.ToString(), product.ProductColors.ToList(), copyFromLink);
            return product;
        }

        public string saveImage(string UserName, string assetType, SampleInspiration sampleInspiration)
        {
            string ret = "";
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            ret = saveImage(assetDir, sampleInspiration.Image, sampleInspiration.Filename.ToString());
            return ret;
        }
        public string saveImage(string UserName, string assetType, ProductTemplate productTemplate)
        {
            string ret = "";
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            ret = saveImage(assetDir, productTemplate.Image, productTemplate.FileName);
            return ret;
        }
        public string saveImage(string UserName, string assetType, ProductTemplateColor productTemplateColor)
        {
            string ret = "";
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            ret = saveImage(assetDir, productTemplateColor.Image, productTemplateColor.FileName);
            return ret;
        }
        public string saveImage(string UserName, string assetType, HttpPostedFileBase image)
        {
            CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
            String filename = Guid.NewGuid().ToString();

            CloudBlockBlob assetImage = assetDir.GetBlockBlobReference(filename + ".png");
            assetImage.UploadFromStream(image.InputStream);
            return filename;
        }
        private string saveImage(CloudBlobDirectory parentDir, string image, string filename)
        {
            try
            {
                
                if (!image.StartsWith("data:image") && !image.Equals(""))
                {
                    //return image; //No need to save file.
                    var arrAddress = image.Split('/');
                    var ret = arrAddress[arrAddress.Length - 1].Split('.')[0];
                    return ret;
                }

                CloudBlockBlob assetImage = parentDir.GetBlockBlobReference(filename + ".png");

                if (image.Equals(""))
                {
                    assetImage.DeleteIfExistsAsync();
                    return "";
                }

                if (image.StartsWith("data:image/png;base64,"))
                {
                    image = image.Replace("data:image/png;base64,", "");
                }
                if (image.StartsWith("data:image/jpeg;base64,"))
                {
                    image = image.Replace("data:image/jpeg;base64,", "");
                }
                byte[] imageBytes = Convert.FromBase64String(image);
                assetImage.UploadFromByteArray(imageBytes, 0, imageBytes.Length);
                return FileNameFromURI(assetImage.Uri.ToString());

            }
            catch (Exception e)
            {
                //TODO: Log Error
                throw new Exception("CloudSaveFailed");
            }
        }
        #endregion Save


        #region Delete
        public void deleteImage(string UserName, string assetType, Asset asset)
        {
            deleteImage(UserName, assetType, asset.Image, asset.Filename);
        }
        public void deleteImage(string UserName, string assetType, Product product)
        {
            deleteImage(UserName, assetType, product.Image, product.Filename);
            deleteImages(UserName, assetType, product.ProductColors.ToList());
        }
        public void deleteImages(string UserName, string assetType, List<ProductColor> productColors)
        {
            for(int i = 0; i < productColors.Count; i++)
            {
                deleteImage(UserName, "ProductColor", productColors[i].Image, productColors[i].Filename);
            }
        }
        public void deleteImage(string UserName, string assetType, SampleInspiration sampleInspiration)
        {
            deleteImage(UserName, assetType, sampleInspiration.Image, sampleInspiration.Filename);
        }
        public void deleteImage(string UserName, string assetType, ProductTemplate productTemplate)
        {
            deleteImage(UserName, assetType, productTemplate.Image, productTemplate.FileName);
        }
        public void deleteImage(string UserName, string assetType, ProductTemplateColor productTemplateColor)
        {
            deleteImage(UserName, assetType, productTemplateColor.Image, productTemplateColor.FileName);
        }
        private void deleteImage(string UserName, string assetType, string image, String filename)
        {
            try
            {
                CloudBlobDirectory assetDir = getAssetDir(UserName, assetType);
                deleteImage(assetDir, filename);
            }
            catch (Exception e)
            {

            }

        }
        private void deleteImage(CloudBlobDirectory assetDir, string imageName)
        {
            try
            {
                CloudBlockBlob assetImage = assetDir.GetBlockBlobReference(imageName + ".png");
                assetImage.DeleteIfExistsAsync();
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region Util
        private CloudBlobDirectory getAssetDir(String UserName, String assetType)
        {

            CloudBlobContainer container = getContainerByName("techmervision");
            if (!container.Exists())
            {
                throw new Exception("User Image Container Unavailable.");
            }

            CloudBlobDirectory assetDir = container.GetDirectoryReference(String.Concat(UserName, "/", assetType));
            return assetDir;
        }
        private CloudBlobDirectory getAssetDir(String UserName, Type type)
        {

            CloudBlobContainer container = getContainerByName("techmervision");
            if (!container.Exists())
            {
                throw new Exception("User Image Container Unavailable.");
            }

            CloudBlobDirectory assetDir = container.GetDirectoryReference(String.Concat(UserName, "/", type.Name.ToString() + "s"));
            return assetDir;
        }
        private CloudBlobContainer getContainerByName(string name)
        {
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(name);

            return container;
        }

        private CloudBlockBlob getBlobFromLink(string image)
        {
            var arr = image.Split('/');
            image = String.Concat(arr[4], '/', arr[5], '/', arr[6].Split('?')[0]);
            //StorageUri uri = new StorageUri(new Uri(image),);

            CloudBlobContainer sourceContainer = getContainerByName("techmervision");
            CloudBlockBlob sourceBlob = sourceContainer.GetBlockBlobReference(image);
            return sourceBlob;

        }
        private String FileNameFromURI(String link) { 
        //return image; //No need to save file.
            var arrAddress = link.Split('/');
            var ret = arrAddress[arrAddress.Length - 1].Split('.')[0];
            return ret;
        }
        #endregion Util
    }
}

#region FileStorageLegacy
//No Longer Used 
/*
class AzureFileStorageProvider 
{
    private ApplicationUserManager _userManger;
    private CloudStorageAccount _storageAccount;
    private SharedAccessFilePolicy _policy;
    public AzureFileStorageProvider(CloudStorageAccount StorageAccount, ApplicationUserManager UserManager)
    {
        _storageAccount = StorageAccount;
        _userManger = UserManager;
        _policy = new SharedAccessFilePolicy()
        {
            Permissions = SharedAccessFilePermissions.Read,
            SharedAccessExpiryTime = DateTime.UtcNow.AddDays(1),
        };
    }

    #region Get
    public List<Product> getImages(string UserName, string assetType, List<Product> products)
    {
        List<Product> ret = new List<Product>();
        List<Task> tasks = new List<System.Threading.Tasks.Task>();
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        foreach (Product product in products)
        {
            tasks.Add(Task.Run(
                () => {
                    product.Image = getImage(assetDir, product.Id.ToString());
                    ret.Add(product);
                }));
        }
        Task.WaitAll(tasks.ToArray());
        return ret;
    }
    public List<Workspace> getImages(string UserName, string assetType, List<Workspace> workspaces)
    {
        List<Workspace> ret = new List<Workspace>();
        List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        foreach (Workspace workspace in workspaces)
        {
            tasks.Add(Task.Run(
                () => {
                    workspace.Image = getImage(assetDir, workspace.Id.ToString());
                    ret.Add(workspace);
                }));
        }
        Task.WaitAll(tasks.ToArray());
        return ret;
    }
    public List<Grid> getImages(string UserName, string assetType, List<Grid> grids)
    {
        List<Grid> ret = new List<Grid>();
        List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        foreach (Grid grid in grids)
        {
            tasks.Add(Task.Run(
                () => {
                    grid.Image = getImage(assetDir, grid.Id.ToString());
                    ret.Add(grid);
                }));
        }
        Task.WaitAll(tasks.ToArray());

        return ret;
    }
    public List<SampleInspiration> getImages(string UserName, string assetType, List<SampleInspiration> sampleInspirations)
    {
        List<SampleInspiration> ret = new List<SampleInspiration>();
        List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        foreach (SampleInspiration sampleInspiration in sampleInspirations)
        {
            tasks.Add(Task.Run(
                () => {
                    sampleInspiration.Image = getImage(assetDir, sampleInspiration.Image.ToString());
                    ret.Add(sampleInspiration);
                }));
        }
        Task.WaitAll(tasks.ToArray());

        return ret;
    }
    public List<ProductTemplate> getImages(string UserName, string assetType, List<ProductTemplate> productTemplates, Boolean includeProductTemplateColorImages)
    {
        List<ProductTemplate> ret = new List<ProductTemplate>();
        List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        foreach (ProductTemplate productTemplate in productTemplates)
        {
            tasks.Add(Task.Run(
                () => {
                    productTemplate.Image = getImage(assetDir, productTemplate.Image.ToString());
                    ret.Add(productTemplate);
                }));
        }
        Task.WaitAll(tasks.ToArray());

        return ret;
    }
    public List<ProductTemplateColor> getImages(string UserName, string assetType, List<ProductTemplateColor> productTemplateColors)
    {
        List<ProductTemplateColor> ret = new List<ProductTemplateColor>();
        List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        foreach (ProductTemplateColor productTemplateColor in productTemplateColors)
        {
            tasks.Add(Task.Run(
                () => {
                    productTemplateColor.Image = getImage(assetDir, productTemplateColor.Image.ToString());
                    ret.Add(productTemplateColor);
                }));
        }
        Task.WaitAll(tasks.ToArray());

        return ret;
    }
    public String getImage(String UserName, String assetType, Asset asset)
    {
        String ret = String.Empty;
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        ret = getImage(assetDir, asset.Id.ToString());
        return ret;
    }

    public String getImage(String UserName, String assetType, SampleInspiration sampleInspiration)
    {
        String ret = String.Empty;
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        ret = getImage(assetDir, sampleInspiration.Image);
        return ret;
    }
    public String getImage(String UserName, String assetType, ProductTemplate productTemplate, Boolean includeProductTemplateColorImage)
    {
        String ret = String.Empty;
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        ret = getImage(assetDir, productTemplate.Image);
        return ret;
    }
    public String getImage(String UserName, String assetType, ProductTemplateColor productTemplateColor)
    {
        String ret = String.Empty;
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        ret = getImage(assetDir, productTemplateColor.Image);
        return ret;
    }

    private string getImage(CloudFileDirectory assetDir, string imageName)
    {
        try
        {
            string ret = "";
            CloudFile assetImage = assetDir.GetFileReference(imageName + ".png");
            if (assetImage.Exists())
            {
                ret = assetImage.Uri.AbsoluteUri + assetImage.GetSharedAccessSignature(_policy);
            }
            return ret;
        }
        catch (Exception e)
        {
            return "";
        }
    }
    #endregion


    #region Save

    public List<Grid> saveImages(string UserName, List<Grid> grids)
    {
        List<Grid> ret = new List<Grid>();
        CloudFileClient fileClient = _storageAccount.CreateCloudFileClient();
        CloudFileShare share = fileClient.GetShareReference("techmervisionuserimages");
        if (!share.Exists())
        {
            throw new Exception("User Image Share Unavailable.");
        }

        CloudFileDirectory rootDir = share.GetRootDirectoryReference();
        CloudFileDirectory userDir = getDirByName(rootDir, UserName);
        CloudFileDirectory assetDir = getDirByName(userDir, "Grids");

        foreach (Grid grid in grids)
        {

            grid.Image = saveImage(assetDir, grid.Image, grid.Id);
            ret.Add(grid);
        }

        return ret;

    }
    public List<Product> saveImages(string UserName, List<Product> products)
    {
        List<Product> ret = new List<Product>();
        CloudFileClient fileClient = _storageAccount.CreateCloudFileClient();
        CloudFileShare share = fileClient.GetShareReference("techmervisionuserimages");
        if (!share.Exists())
        {
            throw new Exception("User Image Share Unavailable.");
        }

        CloudFileDirectory rootDir = share.GetRootDirectoryReference();
        CloudFileDirectory userDir = getDirByName(rootDir, UserName);
        CloudFileDirectory assetDir = getDirByName(userDir, "Products");

        foreach (Product product in products)
        {

            product.Image = saveImage(assetDir, product.Image, product.Id);
            ret.Add(product);
        }

        return ret;

    }

    public List<Workspace> saveImages(string UserName, List<Workspace> workspaces)
    {
        List<Workspace> ret = new List<Workspace>();
        CloudFileClient fileClient = _storageAccount.CreateCloudFileClient();
        CloudFileShare share = fileClient.GetShareReference("techmervisionuserimages");
        if (!share.Exists())
        {
            throw new Exception("User Image Share Unavailable.");
        }

        CloudFileDirectory rootDir = share.GetRootDirectoryReference();
        CloudFileDirectory userDir = getDirByName(rootDir, UserName);
        CloudFileDirectory assetDir = getDirByName(userDir, "Workspaces");

        foreach (Workspace workspace in workspaces)
        {

            workspace.Image = saveImage(assetDir, workspace.Image, workspace.Id);
            ret.Add(workspace);
        }

        return ret;

    }

    public string saveImage(string UserName, string assetType, Asset asset, Boolean copyFromLink)
    {
        string ret = "";
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        ret = saveImage(assetDir, asset.Image, asset.Id);
        return ret;
    }

    public string saveImage(string UserName, string assetType, SampleInspiration sampleInspiration)
    {
        string ret = "";
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        ret = saveImage(assetDir, sampleInspiration.Image, sampleInspiration.Id);
        return ret;
    }
    public string saveImage(string UserName, string assetType, ProductTemplate productTemplate)
    {
        string ret = "";
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        ret = saveImage(assetDir, productTemplate.Image, productTemplate.Id);
        return ret;
    }
    public string saveImage(string UserName, string assetType, ProductTemplateColor productTemplateColor)
    {
        string ret = "";
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        ret = saveImage(assetDir, productTemplateColor.Image, productTemplateColor.Id);
        return ret;
    }

    public string saveImage(string UserName, string assetType, HttpPostedFileBase image)
    {
        string ret = "";
        CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
        Guid id = Guid.NewGuid();

        CloudFile assetImage = assetDir.GetFileReference(id.ToString() + ".png");
        assetImage.UploadFromStream(image.InputStream);
        return id.ToString();
    }
    private string saveImage(CloudFileDirectory parentDir, string image, long id)
    {
        try
        {
            if (!image.StartsWith("data:image") && !image.Equals(""))
            {
                return image; //No need to save file.
            }

            CloudFile assetImage = parentDir.GetFileReference(id.ToString() + ".png");

            if (image.Equals(""))
            {
                assetImage.DeleteIfExistsAsync();
                return "";
            }

            if (image.StartsWith("data:image/png;base64,"))
            {
                image = image.Replace("data:image/png;base64,", "");
            }
            if (image.StartsWith("data:image/jpeg;base64,"))
            {
                image = image.Replace("data:image/jpeg;base64,", "");
            }
            byte[] imageBytes = Convert.FromBase64String(image);
            assetImage.UploadFromByteArray(imageBytes, 0, imageBytes.Length);
            return assetImage.Uri.ToString();

        }
        catch (Exception e)
        {
            //TODO: Log Error
            throw new Exception("CloudSaveFailed");
        }
    }

    #endregion Save


    #region Delete
    public void deleteImage(string UserName, string assetType, Asset asset)
    {
        deleteImage(UserName, assetType, asset.Image, asset.Id);
    }
    public void deleteImage(string UserName, string assetType, SampleInspiration sampleInspiration)
    {
        deleteImage(UserName, assetType, sampleInspiration.Image, sampleInspiration.Id);
    }
    public void deleteImage(string UserName, string assetType, ProductTemplate productTemplate)
    {
        deleteImage(UserName, assetType, productTemplate.Image, productTemplate.Id);
    }
    public void deleteImage(string UserName, string assetType, ProductTemplateColor productTemplateColor)
    {
        deleteImage(UserName, assetType, productTemplateColor.Image, productTemplateColor.Id);
    }
    private void deleteImage(string UserName, string assetType, string image, long id)
    {
        try
        {
            CloudFileDirectory assetDir = getAssetDir(UserName, assetType);
            deleteImage(assetDir, id.ToString());
        }
        catch (Exception e)
        {

        }

    }
    private void deleteImage(CloudFileDirectory assetDir, string imageName)
    {
        try
        {
            CloudFile assetImage = assetDir.GetFileReference(imageName + ".png");
            assetImage.DeleteIfExistsAsync();
        }
        catch (Exception e)
        {

        }
    }
    #endregion

    #region Util
    private CloudFileDirectory getDirByName(CloudFileDirectory parentDir, string dirName)
    {
        CloudFileDirectory userDir = parentDir.GetDirectoryReference(dirName);
        userDir.CreateIfNotExists();
        return userDir;
    }
    private CloudFileDirectory getAssetDir(String UserName, String assetType)
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
    private CloudBlobContainer getContainerByName(string name)
    {
        CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference("techmervision");
        return container;
    }
    #endregion Util
}
*/
#endregion