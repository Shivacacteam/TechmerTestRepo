using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
namespace TechmerVision.Models
{
    public class Sharing
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string UserId { get; set; }

       
        public string AssetType { get; set; }


        public long AssetId { get; set; }

    }
}