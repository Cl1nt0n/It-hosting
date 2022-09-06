using System;
using System.Collections.Generic;

namespace It_hosting_2._0.Models.DBModels
{
    public partial class Branch
    {
        public Branch()
        {
            Commits = new HashSet<Commit>();
            PullRequestFromBranches = new HashSet<PullRequest>();
            PullRequestToBranches = new HashSet<PullRequest>();
            Repositories = new HashSet<Repository>();
        }

        public int Id { get; set; }
        public string? File { get; set; }
        public int RepositoryId { get; set; }

        public virtual Repository Repository { get; set; } = null!;
        public virtual ICollection<Commit> Commits { get; set; }
        public virtual ICollection<PullRequest> PullRequestFromBranches { get; set; }
        public virtual ICollection<PullRequest> PullRequestToBranches { get; set; }
        public virtual ICollection<Repository> Repositories { get; set; }
    }
}
