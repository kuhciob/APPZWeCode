using System;
using System.Collections.Generic;

#nullable disable

namespace WeCode
{
    public partial class CodeBlock
    {
        public CodeBlock()
        {
            ActualResults = new HashSet<ActualResult>();
            ExpectedResults = new HashSet<ExpectedResult>();
        }

        public int CodeBlockId { get; set; }
        public string Code { get; set; }

        public virtual ICollection<ActualResult> ActualResults { get; set; }
        public virtual ICollection<ExpectedResult> ExpectedResults { get; set; }
    }
}
