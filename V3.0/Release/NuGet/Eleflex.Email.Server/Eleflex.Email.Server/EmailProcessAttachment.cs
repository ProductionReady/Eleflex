//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eleflex.Email.Server
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailProcessAttachment
    {
        public long EmailProcessAttachmentKey { get; set; }
        public long EmailProcessKey { get; set; }
        public string Filename { get; set; }
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
    
        public virtual EmailProcess EmailProcess { get; set; }
    }
}
