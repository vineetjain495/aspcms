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
    
    public partial class atmmaster
    {
        public int pkid { get; set; }
        public string atmid { get; set; }
        public string atmcode { get; set; }
        public string MSP { get; set; }
        public string RoCode { get; set; }
        public string RoName { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string CityName { get; set; }
        public string HubLocationCode { get; set; }
        public string HubLocationName { get; set; }
        public string SubLocationCode { get; set; }
        public string SubLocationName { get; set; }
        public string Zone { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> TakeOverDate { get; set; }
        public Nullable<System.DateTime> HandOverDate { get; set; }
        public string Company { get; set; }
        public string status { get; set; }
        public string AtmType { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string RuleErrorCode { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public Nullable<long> CBRID { get; set; }
    }
}
