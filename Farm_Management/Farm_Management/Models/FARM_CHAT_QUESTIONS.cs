//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Farm_Management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FARM_CHAT_QUESTIONS
    {
        public int CHAT_Q_ID { get; set; }
        public int CHAT_Q_ANSWER_ID { get; set; }
        public string CHAT_Q_DESCRIPTION { get; set; }
        public int CHAT_Q_STATUS { get; set; }
        public string CHAT_Q_CREATE_BY { get; set; }
        public System.DateTime CHAT_Q_CREATE_DATE { get; set; }
        public string CHAT_Q_MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> CHAT_Q_MODIFIED_DATE { get; set; }
    }
}