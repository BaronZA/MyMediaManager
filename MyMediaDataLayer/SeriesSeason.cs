//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyMediaDataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class SeriesSeason
    {
        public int Id { get; set; }
        public int SeriesID { get; set; }
        public int SeasonNumber { get; set; }
        public string StorageLocation { get; set; }
    
        public virtual Series Series { get; set; }
    }
}
