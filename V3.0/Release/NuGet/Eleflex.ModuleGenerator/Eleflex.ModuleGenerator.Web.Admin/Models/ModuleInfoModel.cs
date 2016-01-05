using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Eleflex.ModuleGenerator.Web.Admin.Models
{
    public class ModuleInfoModel
    {

        public ModuleInfoModel()
        {
            NuGetSingleRelease = true;
        }

        [Required]
        public string NamespacePrefix { get; set; }

        [Required]
        public string ModuleName { get; set; }

        [Required]
        public string EntityModelName { get; set; }

        public bool NuGetSingleRelease { get; set; }
    }
}
