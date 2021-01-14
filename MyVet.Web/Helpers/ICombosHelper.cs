using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MyVet.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPetTypes();

        IEnumerable<SelectListItem> GetComboServiceTypes();
    }
}