﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PRS_Server {

    public partial class RequestLines {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("RequestLines")]
        public virtual Products Product { get; set; }
        [ForeignKey("RequestId")]
        [InverseProperty("RequestLines")]
        [JsonIgnore] // I think this is where it's supposed to go to avoid circular reference?
        public virtual Requests Request { get; set; }
    }
}
