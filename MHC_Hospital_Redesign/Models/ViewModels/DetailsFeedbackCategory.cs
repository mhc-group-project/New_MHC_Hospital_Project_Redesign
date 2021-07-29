using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DetailsFeedbackCategory
    {
        public FeedbackCategoryDto SelectedFeedbackCategory { get; set; }
        public IEnumerable<FeedbackDto> RelatedFeedbacks { get; set; }
    }
}