using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHC_Hospital_Redesign.Models.ViewModels
{
    public class ListFaq
    {
        public bool IsAdmin { get; set; }

        public IEnumerable<FaqDto> Faqs { get; set; }
    }
}