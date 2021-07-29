using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHC_Hospital_Redesign.Models.ViewModels
{
    public class UpdateEcard
    {

        public EcardDto SelectedEcard { get; set; }

        public IEnumerable<TemplateDto> TemplateOptions { get; set; }

    }
}