using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHC_Hospital_Redesign.Models.ViewModels
{
    public class DetailsDepartment
    {
        public bool isadmin { get; set; }
        public DepartmentDto Department { get; set; }
        public IEnumerable<ListingDto> Jobs { get; set; }
    }
}