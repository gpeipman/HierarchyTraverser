using System.Collections.Generic;

namespace HierarchyTraverserLib.Providers
{
    public class InMemoryNode
    {
        int Id { get; set; }
        public string Title { get; set; }

        public IList<InMemoryNode> Children = new List<InMemoryNode>();
    }
}