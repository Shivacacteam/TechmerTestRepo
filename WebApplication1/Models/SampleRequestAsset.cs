using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
namespace TechmerVision.Models
{
    public class SampleRequestAsset
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string AssetType { get; set; }
        public string Notes { get; set; }
        public long AssetId { get; set; }
        public long RequestId { get; set; }


        //public virtual ICollection<Product> Product { get; set; }
        //public virtual ICollection<ColorSelection> ColorSelection { get; set; }
        //public virtual ICollection<Grid> Grid { get; set; }
    }
}