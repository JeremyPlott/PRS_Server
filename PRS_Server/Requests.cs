﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRS_Server {

    public partial class Requests {

        public Requests() 
        {
            RequestLines = new HashSet<RequestLines>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Description { get; set; }
        [Required]
        [StringLength(80)]
        public string Justification { get; set; }
        [StringLength(80)]
        public string RejectionReason { get; set; }
        [Required]
        [StringLength(20)]
        public string DeliveryMode { get; set; }
        [Required]
        [StringLength(10)]
        public string Status { get; set; }
        [Required]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; }
        public int UserId { get; set; } // int? in first file

        [ForeignKey("UserId")]
        [InverseProperty("Requests")]
        public virtual Users User { get; set; }
        [InverseProperty("Request")]
        public virtual ICollection<RequestLines> RequestLines { get; set; }
    }
}
