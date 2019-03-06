using System.Collections.Generic;

namespace HierarchyTraverserLib
{
    public class HierarchyTraverser<T>
    {
        public delegate void NodeLoadedEventHandler(object sender, NodeLoadedEventArgs<T> e);
        public event NodeLoadedEventHandler NodeLoaded;

        private readonly IHierarchyProvider<T> _provider;
        private bool _stop = false;

        public HierarchyTraverser(IHierarchyProvider<T> provider)
        {
            _provider = provider;
        }

        public void Start()
        {
            var rootNodes = _provider.LoadRootNodes();
            var stack = new Stack<NodeWrapper<T>>();
            var visitedNodes = new HashSet<T>();

            foreach (var rootNode in rootNodes)
            {
                stack.Push(new NodeWrapper<T> { Node = rootNode, Level = 0, Parent = null });
            }

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (visitedNodes.Contains(node.Node))
                {
                    continue;
                }

                OnLoadNode(node);
                visitedNodes.Add(node.Node);

                var children = _provider.LoadChildren(node.Node);
                if (children == null)
                {
                    continue;
                }

                foreach (var childNode in children)
                {
                    if (_stop)
                    {
                        break;
                    }

                    stack.Push(new NodeWrapper<T>
                    {
                        Node = childNode,
                        Level = node.Level + 1,
                        Parent = node
                    });
                }
            }
        }

        public void Stop()
        {
            _stop = true;
        }

        protected void OnLoadNode(NodeWrapper<T> node)
        {
            var args = new NodeLoadedEventArgs<T>
            {
                Node = node
            };

            NodeLoaded?.Invoke(this, args);
        }
    }
}