//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrderMenu
    {
        public int OrderMenuId { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
    
        public virtual tblMenu tblMenu { get; set; }
        public virtual tblOrder tblOrder { get; set; }
    }
}
