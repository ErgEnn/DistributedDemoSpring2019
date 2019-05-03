using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ContactType : DomainEntity
    {

        //[ForeignKey(nameof(ContactTypeValue))]
        public int ContactTypeValueId { get; set; }

        public MultiLangString ContactTypeValue { get; set; }


        public ICollection<Contact> Contacts { get; set; }
    }
}