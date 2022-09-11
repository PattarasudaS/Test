using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HomeCare_4_0.Models
{
    public class DropDownListModel
    {
        public long? SelectedItemId { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }

    }
   
}