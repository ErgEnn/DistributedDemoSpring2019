using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ContactTypeDTO
    {
        public int Id { get; set; }
        public string ContactTypeValue { get; set; }

        public int ContactCount { get; set; }
    }
}