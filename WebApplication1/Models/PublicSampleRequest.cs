using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechmerVision.Models
{
    public class PublicSampleRequest
    {



        public long Id { get; set; }


        public string Owner { get; set; }

        public string ProjectName { get; set; }

        public string Notes { get; set; }

        public bool Status { get; set; }

        public DateTime SubmissionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<PublicRequestAsset> PublicRequestAssetlist { get; set; }

        public PublicSampleRequest()
        {

        }

    }
}