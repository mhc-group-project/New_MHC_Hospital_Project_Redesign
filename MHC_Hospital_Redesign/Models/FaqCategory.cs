using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MHC_Hospital_Redesign.Models
{
    public class FaqCategory
    {
        [Key]
        public int FaqCategoryID { get; set; }
        [Required]
        public DateTime CategoryDateAdded { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [AllowHtml]
        [Required]
        public string CategoryDescription { get; set; }
        [Required]
        public string CategoryColor { get; set; }

        public ICollection<Faq> Faqs { get; set; }
    }

    public class FaqCategoryDto
    {
        public int FaqCategoryID { get; set; }
        [Required]
        public DateTime CategoryDateAdded { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [AllowHtml]
        [Required]
        public string CategoryDescription { get; set; }
        [Required]
        public string CategoryColor { get; set; }

    }
}