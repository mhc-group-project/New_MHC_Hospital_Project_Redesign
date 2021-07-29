using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateFeedback
    {
      

        public FeedbackDto SelectedFeedback { get; set; }

        public IEnumerable<FeedbackCategoryDto> FeedbackCategoryOptions { get; set; }
        public IEnumerable<DepartmentDto> DepartmentOptions { get; set; }
    }
}