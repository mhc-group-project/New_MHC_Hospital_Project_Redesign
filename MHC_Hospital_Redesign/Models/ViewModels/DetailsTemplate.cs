using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHC_Hospital_Redesign.Models.ViewModels
{
    public class DetailsTemplate
    {
        public TemplateDto SelectedTemplate { get; set; }

        public IEnumerable<EcardDto> TemplateForEcard { get; set; }


    }
}