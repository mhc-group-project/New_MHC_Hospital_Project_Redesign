using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MHC_Hospital_Redesign.Models
{
    public class Faq
    {
        [Key]
        public int FaqID { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public string FaqQuestions { get; set; }

        [AllowHtml]
        [Required]
        public string FaqAnswers { get; set; }
        [Required]
        public int FaqSort { get; set; }

        public ICollection<FaqCategory> FaqCategories { get; set; }

    }

    public class FaqDto
    {
        public int FaqID { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public string FaqQuestions { get; set; }
        [AllowHtml]
        [Required]
        public string FaqAnswers { get; set; }
        [Required]
        public int FaqSort { get; set; }
    }
}