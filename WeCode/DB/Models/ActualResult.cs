using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WeCode
{
    public partial class ActualResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActualResultId { get; set; }
        public int? TaskResultId { get; set; }
        public int? CodeBlockId { get; set; }
        public int? Order { get; set; }

        public virtual CodeBlock CodeBlock { get; set; }
        public virtual TaskResult TaskResult { get; set; }
    }
}
