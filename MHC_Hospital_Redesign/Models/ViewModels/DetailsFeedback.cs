using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DetailsFeedback
    {

        public FeedbackDto SelectedFeedback { get; set; }
        public DepartmentDto SelectedDepartment { get; set; }
        public FeedbackCategoryDto RelatedFeedbackCategory { get; set; }

    }
}