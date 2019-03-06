using System.Collections.Generic;

namespace HierarchyTraverserLib.Providers
{
    public class InMemoryHierarchyProvider : IHierarchyProvider<InMemoryNode>
    {
        private InMemoryNode _rootNode = new InMemoryNode { Title = "0" };

        public bool HasRootNode => true;

        public IList<InMemoryNode> LoadChildren(InMemoryNode node)
        {
            return node.Children;
        }

        public IList<InMemoryNode> LoadRootNodes()
        {
            return new List<InMemoryNode> { _rootNode, _rootNode.Children[0] };
        }

        public void LoadNodeTree()
        {
            AddNodes(_rootNode, 1);

        }

        private int _idCounter = 0;
        private int _maxLevel = 2;
        private void AddNodes(InMemoryNode parent, int level)
        {
            //Console.WriteLine("Level: " + level);
            if (level + 1 > _maxLevel)
            {
                return;
            }

            for (var i = 0; i < 4; i++)
            {
                _idCounter++;
                var node = new InMemoryNode { Title = _idCounter.ToString() };
                parent.Children.Add(node);
                AddNodes(node, level + 1);
            }
        }
    }
}
