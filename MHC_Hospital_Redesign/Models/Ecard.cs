using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace MHC_Hospital_Redesign.Models
{
    public class Ecard
    {
        [Key]
        public int EcardId { get; set; }
        public string SenderName { get; set; }
        public string PatientName { get; set; }
        public string Message { get; set; }
        
        //An ecard belongs to one template
        [ForeignKey("Template")]
        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }
    }

    public class EcardDto
    {
        public int EcardId { get; set; }
        public string SenderName { get; set; }
        public string PatientName { get; set; }
        public string Message { get; set; }

        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public bool TemplateHasPic { get; set; }
        public string TemplatePicExtension { get; set; }

        public string TemplateStyle { get; set; }

    }
  
}