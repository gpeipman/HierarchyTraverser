namespace HierarchyTraverserLib
{
    public class NodeWrapper<T>
    {
        public NodeWrapper<T> Parent { get; set; }
        public T Node { get; set; }
        public int Level { get; set; }
    }
}
