using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MHC_Hospital_Redesign.Models
{
    public class Ecard
    {
        [Key]
        public int EcardId { get; set; }
        [Required]
        public string SenderName { get; set; }
        [Required]
        public string PatientName { get; set; }
        [Required]
        [AllowHtml]
        public string Message { get; set; }
        
        //An ecard belongs to one template
        [ForeignKey("Template")]
        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

      // [ForeignKey("ApplicationUser")]
      //public string UserId { get; set; }
     // public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class EcardDto
    {
        public int EcardId { get; set; }
        [Required(ErrorMessage ="Please enter your name.")]
        public string SenderName { get; set; }
        [Required(ErrorMessage = "Please enter the patients name.")]
        public string PatientName { get; set; }
        [Required(ErrorMessage = "Please enter a message. ")]
        [AllowHtml]
        public string Message { get; set; }

        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public bool TemplateHasPic { get; set; }
        public string TemplatePicExtension { get; set; }

        public string TemplateStyle { get; set; }

        public bool TemplateHasStyle { get; set; }

        public string TemplateStyleExtension { get; set; }
        

    }
  
}