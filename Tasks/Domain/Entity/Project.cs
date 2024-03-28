using System.Collections.Generic;

namespace Tasks.Domain.Entity
{
    public class Project
    {
        public string Name { get; set; }
        
        public IList<Task> Tasks { get; set; } = new List<Task>();
    }
}