//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReportingWebForms
{
    using System;
    using System.Collections.Generic;
    
    public partial class Campaign
    {
        public Campaign()
        {
            this.Response_Report = new HashSet<Response_Report>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int customer_id { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Response_Report> Response_Report { get; set; }
    }
}
