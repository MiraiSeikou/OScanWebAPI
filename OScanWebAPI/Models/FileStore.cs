//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OScanWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileStore
    {
        public int Id { get; set; }
        public Nullable<long> Total { get; set; }
        public Nullable<long> Available { get; set; }
        public Nullable<System.DateTime> Momentum { get; set; }
        public Nullable<int> IdMaquina { get; set; }
    
        public virtual Maquina Maquina { get; set; }
    }
}
