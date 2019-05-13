using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class TestViewModel
    {
        [DataType(DataType.Date)]
        public DateTime JustDate { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime JustTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAndTime { get; set; }

        public decimal DecimalNumber { get; set; }
        
    }
}