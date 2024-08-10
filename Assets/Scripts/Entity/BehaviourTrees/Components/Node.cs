using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class NodeData
    {
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        private readonly Node parent;

        public NodeData(Node setParent)
        {
            parent = setParent;
        }

        public void Set(string key, object value)
        {
            _dataContext[key] = value;
        }

        public void SetRoot(string key, object value)
        {
            if (parent != null)
            {
                parent.data.SetRoot(key, value);
            }
            else
            {
                Set(key, value);
            }
        }

        public object Get(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;
            if (parent != null)
            {
                return parent.data.Get(key);
            }
            return null;
        }

        public bool Clear(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }
            Node node = parent;
            while (node != null)
            {
                bool cleared = node.data.Clear(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }

    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();
        public readonly NodeData data;

        public Node() 
        {
            parent = null;
            data = new NodeData(parent);
        }

        public Node(params Node[] children)
        {
            foreach (Node child in children)
                Attach(child);
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}

