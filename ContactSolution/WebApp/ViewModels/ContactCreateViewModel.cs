using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ContactCreateViewModel
    {
        public Contact Contact { get; set; }
        public SelectList PersonSelectList { get; set; }
        public SelectList ContactTypeSelectList { get; set; }
        
    }
}