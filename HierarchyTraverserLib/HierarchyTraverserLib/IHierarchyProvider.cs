using System.Collections.Generic;

namespace HierarchyTraverserLib
{
    public interface IHierarchyProvider<T>
    {
        IList<T> LoadRootNodes();
        IList<T> LoadChildren(T node);
    }
}
