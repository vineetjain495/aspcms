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
    
    public partial class utilization_data_logs
    {
        public int ID { get; set; }
        public string MSP { get; set; }
        public string BatchID { get; set; }
        public string TicketID { get; set; }
        public string CBRID { get; set; }
        public string ATMID { get; set; }
        public Nullable<System.DateTime> EOD_Date { get; set; }
        public Nullable<double> Overage { get; set; }
        public Nullable<double> Overage_Usage { get; set; }
        public Nullable<double> Overage_Balance { get; set; }
        public Nullable<double> Mid_cash { get; set; }
        public Nullable<double> Mid_cash_Usage { get; set; }
        public Nullable<double> Mid_Cash_Balance { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<double> ATM_Deposit { get; set; }
        public Nullable<double> ATM_Usage { get; set; }
        public Nullable<double> ATM_Balance { get; set; }
    }
}
