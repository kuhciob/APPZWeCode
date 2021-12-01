using System;
using System.Collections.Generic;

#nullable disable

namespace WeCode
{
    public partial class ActualResult
    {
        public int ActualResultId { get; set; }
        public int? TaskResultId { get; set; }
        public int? CodeBlockId { get; set; }
        public int? Order { get; set; }

        public virtual CodeBlock CodeBlock { get; set; }
        public virtual TaskResult TaskResult { get; set; }
    }
}
