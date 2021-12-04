using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WeCode
{
    public partial class ExpectedResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpectedResultId { get; set; }
        public int? TaskId { get; set; }
        public int? CodeBlockId { get; set; }
        public int? Order { get; set; }

        public virtual CodeBlock CodeBlock { get; set; }
        public virtual Task Task { get; set; }
    }
}
