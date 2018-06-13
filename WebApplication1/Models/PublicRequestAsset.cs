using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechmerVision.Models
{
    public class PublicRequestAsset
    {


        public long AssetId { get; set; }

        public long Id { get; set; }
        public string AssetTitle { get; set; }
        public string AssetType { get; set; }
        public string Assetbackground { get; set; }

        public PublicRequestAsset()
        {

        }
        

    }
}