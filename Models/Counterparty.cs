using System;
using System.ComponentModel.DataAnnotations;

namespace sbs_api_2.Models
{

    public enum type_company
    {
    Юр_лицо, 
    ИП 
    } ;

    public class Counterparty
    {
        public Guid Id;
        [Required]
        public string Name { get; set; }
        // public List<User> Users { get; set; }
        public string FullName;
        [Required]
        public type_company Type {get; set;} 
        [Required]
        public string INN { get; set; }
        public string KPP { get; set; }
    }
}