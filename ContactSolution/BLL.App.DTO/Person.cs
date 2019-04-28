using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Versioning;
using DAL.App.DTO.Identity;

namespace BLL.App.DTO
{
    public class Person
    {

        public int Id { get; set; }
        
        [MaxLength(64,ErrorMessageResourceName = "ErrorMessageMaxLength" ,ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Domain.Person))]
        public string FirstName { get; set; }

        [MaxLength(64, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Domain.Person))]
        public string LastName { get; set; }


        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        //public ICollection<Contact> Contacts { get; set; }

        
        public string FirstLastName => FirstName + " " + LastName;
    }
}