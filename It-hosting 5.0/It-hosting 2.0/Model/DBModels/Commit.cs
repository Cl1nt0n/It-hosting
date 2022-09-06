using System;
using System.Collections.Generic;

namespace It_hosting_2._0.Models.DBModels
{
    public partial class Commit
    {
        public int Id { get; set; }
        public string File { get; set; } = null!;
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; } = null!;
    }
}
