using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ContactCreateEditViewModel
    {
        public BLL.App.DTO.Contact Contact { get; set; }
        public SelectList PersonSelectList { get; set; }
        public SelectList ContactTypeSelectList { get; set; }
        
    }
}