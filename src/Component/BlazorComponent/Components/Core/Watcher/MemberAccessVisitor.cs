using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    internal class MemberAccessVisitor : ExpressionVisitor
    {
        public List<PropertyInfo> PropertyInfos { get; } = new();

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member is PropertyInfo propertyInfo)
            {
                if (!PropertyInfos.Any(prop => prop.PropertyType == propertyInfo.PropertyType && prop.Name == propertyInfo.Name))
                {
                    PropertyInfos.Add(propertyInfo);
                }
            }

            return base.VisitMember(node);
        }
    }
}
