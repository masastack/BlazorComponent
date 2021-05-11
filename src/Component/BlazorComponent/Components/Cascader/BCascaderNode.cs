using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class BCascaderNode
    {
        private List<BCascaderNode> _children;

        public string Value { get; set; }

        public string Label { get; set; }

        public List<BCascaderNode> Children
        {
            get
            {
                return _children;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                _children = value;
                foreach (var child in _children)
                {
                    child.Parent = this;
                }
            }
        }

        public BCascaderNode Parent { get; private set; }

        public List<BCascaderNode> GetAllNodes()
        {
            var nodes = new List<BCascaderNode>
            {
                this
            };

            GetParents(this, ref nodes);
            nodes.Reverse();

            return nodes;
        }

        private void GetParents(BCascaderNode node, ref List<BCascaderNode> parents)
        {
            if (node.Parent != null)
            {
                parents.Add(node.Parent);
                GetParents(node.Parent, ref parents);
            }
        }
    }
}
