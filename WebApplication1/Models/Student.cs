//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int SID { get; set; }
        public string Email { get; set; }
        public string First_name { get; set; }
        public string Last_Name { get; set; }
        public string Adress { get; set; }
        public Nullable<int> Phone_number { get; set; }
        public string code { get; set; }
        public Nullable<int> FID { get; set; }
        public string profile { get; set; }
        public string pass { get; set; }
    
        public virtual courses courses { get; set; }
        public virtual fees fees { get; set; }
    }
}