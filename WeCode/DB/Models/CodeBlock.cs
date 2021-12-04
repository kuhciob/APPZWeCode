using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CodeBlockId { get; set; }
        public string Code { get; set; }

        public virtual ICollection<ActualResult> ActualResults { get; set; }
        public virtual ICollection<ExpectedResult> ExpectedResults { get; set; }
    }
}
