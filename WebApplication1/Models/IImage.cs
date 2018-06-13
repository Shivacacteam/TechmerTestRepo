using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechmerVision.Models
{
    interface IImage
    {
        long Id { get; set; }
        String Image { get; set; }
    }
}
