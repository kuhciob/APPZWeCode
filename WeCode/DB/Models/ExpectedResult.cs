using System;
using System.Collections.Generic;

#nullable disable

namespace WeCode
{
    public partial class ExpectedResult
    {
        public int ExpectedResultId { get; set; }
        public int? TaskId { get; set; }
        public int? CodeBlockId { get; set; }
        public int? Order { get; set; }

        public virtual CodeBlock CodeBlock { get; set; }
        public virtual Task Task { get; set; }
    }
}
