using System;
using System.ComponentModel.DataAnnotations;

namespace IronXLSample.ExcelToDB
{
    /// <summary>
    /// Entity framework Entity representation of the Country table
    /// </summary>
    public class Country
    {
        [Key]
        public Guid Key { get; set; }
        public string Name { get; set; }
        public decimal GDP { get; set; }
    }
}
