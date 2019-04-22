using Microsoft.AspNetCore.Mvc.Rendering;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO; 

namespace WebApp.ViewModels
{
    public class ContactCreateEditViewModel
    {
        public BLLAppDTO.Contact Contact { get; set; }
        public SelectList PersonSelectList { get; set; }
        public SelectList ContactTypeSelectList { get; set; }
        
    }
}