using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MHC_Hospital_Redesign.Models
{
    public class Template
    {
        [Key]
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }

        public bool TemplateHasPic { get; set; }
        public string TemplatePicExtension { get; set; }

        public string TemplateStyle { get; set; }

    }

    public class TemplateDto
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }

        public bool TemplateHasPic { get; set; }
        public string TemplatePicExtension { get; set; }

        public string TemplateStyle { get; set; }
    }
}