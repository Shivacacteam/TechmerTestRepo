using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
namespace TechmerVision.Models
{
    public class SampleRequest
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

       
        public string Owner { get; set; }

        public string ProjectName { get; set; }

        public string Notes { get; set; }

        public bool Status { get; set; }
       
        public DateTime SubmissionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        //public virtual ICollection<SampleRequestAsset> SampleRequestAsset { get; set; }
    }
}