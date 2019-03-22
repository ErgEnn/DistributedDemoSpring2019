using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class PersonCreateEditViewModel
    {
        public Person Person { get; set; }
        public SelectList AppUserSelectList { get; set; }
    }
}