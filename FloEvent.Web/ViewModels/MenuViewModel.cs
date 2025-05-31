using FloEvent.Catering.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FloEvent.Web.ViewModels
{
    public class MenuViewModel
    {
        public Menu Menu { get; set; } = new Menu();
    }
}
