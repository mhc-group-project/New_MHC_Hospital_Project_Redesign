using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHC_Hospital_Redesign.Models.ViewModels
{
    public class DetailsFaqCategory
    {
        public bool IsAdmin { get; set; }

        [AllowHtml]
        public FaqCategoryDto SelectedFaqCategory { get; set; }
        [AllowHtml]
            public IEnumerable<FaqDto> LinkedFaqs { get; set; }
       
    }
}