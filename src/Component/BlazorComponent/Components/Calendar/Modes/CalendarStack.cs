using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneOf;

namespace BlazorComponent
{
    public static class CalendarStack
    {
        private const int FullWidth = 100;
        private const int DefaultOffset = 5;
        private const double WidthMultiplter = 1.7;

        public static Func<List<CalendarEventParsed>, int, int,
            Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>>> Stack =>
            (events, firstWeekday, overlapThreshold) =>
            {
                var handler = CalendarCommon.GetOverlapGroupHandler(firstWeekday);

                return (day, dayEvents, timed, reset) =>
                {
                    if (!timed)
                        return handler.GetVisuals(day, dayEvents, timed, reset);

                    var dayStart = CalendarTimestampUtils.GetTimestampIdentifier(day);
                    var visuals = CalendarCommon.GetVisuals(dayEvents, dayStart);
                    var groups = GetGroups(visuals, dayStart);

                    foreach (var group in groups)
                    {
                        var nodes = new List<CalendarStackNode>();
                        foreach (var visual in group.Visuals)
                        {
                            var child = GetNode(visual, dayStart);
                            var index = GetNextIndex(child, nodes);

                            if (index.IsT1 && !index.AsT1)
                            {
                                var parent = GetParent(child, nodes);
                                if (parent != null)
                                {
                                    child.Parent = parent;
                                    child.Sibling = CalendarCommon.HasOverlap(child.Start, child.End,
                                        parent.Start, AddTime(parent.Start, overlapThreshold));
                                    child.Index = parent.Index + 1;
                                    parent.Children.Add(child);
                                }
                            }
                            else
                            {
                                var indexInt = index.AsT0;
                                var parent = GetOverlappingRange(child, nodes, indexInt - 1, indexInt - 1);
                                var parentFirst = parent.FirstOrDefault();
                                var children = GetOverlappingRange(child, nodes, indexInt + 1, indexInt + nodes.Count, true);

                                child.Children = children;
                                child.Index = indexInt;

                                if(parentFirst?.Visual != null)
                                {
                                    child.Parent = parent.First();
                                    child.Sibling = CalendarCommon.HasOverlap(child.Start, child.End,
                                        child.Parent.Start, AddTime(parentFirst.Start, overlapThreshold));
                                    parentFirst.Children.Add(child);
                                }

                                foreach (var grand in children)
                                {
                                    if (grand.Parent == parentFirst)
                                        grand.Parent = child;

                                    var grandNext = grand.Index - child.Index <= 1;
                                    if (grandNext && child.Sibling && CalendarCommon.HasOverlap(child.Start,
                                        AddTime(child.Start, overlapThreshold), grand.Start, grand.End))
                                        grand.Sibling = true;
                                }
                            }

                            nodes.Add(child);
                        }

                        CalculateBounds(nodes, overlapThreshold);
                    }

                    for (int i = 0; i < visuals.Count - 1; i++)
                    {
                        for (int j = 0; j < visuals.Count - 1 - i; j++)
                        {
                            if ((visuals[i].Left - visuals[j].Left) > 0 ||
                               (visuals[i].Event.StartTimestampIdentifier - visuals[j].Event.StartTimestampIdentifier) > 0)
                            {
                                var temp = visuals[j];
                                visuals[j] = visuals[j + 1];
                                visuals[j + 1] = temp;
                            }
                        }
                    }

                    return visuals;
                };
            };

        public static List<CalendarStackGroup> GetGroups(List<CalendarEventVisual> visuals, int dayStart)
        { 
            var groups = new List<CalendarStackGroup>();

            foreach (var visual in visuals)
            { 
                var (start, end) = CalendarCommon.GetNormalizedRange(visual.Event, dayStart);
                var added = false;

                foreach (var group in groups)
                {
                    if (CalendarCommon.HasOverlap(start, end, group.Start, group.End))
                    {
                        group.Visuals.Add(visual);
                        group.End = Math.Max(group.End, end);
                        added = true;
                        break;
                    }
                }

                if (!added)
                    groups.Add(new() { Start = start, End = end, Visuals = new() { visual } });
            }

            return groups;
        }

        public static CalendarStackNode GetNode(CalendarEventVisual visual, int dayStart)
        {
            var (start, end) = CalendarCommon.GetNormalizedRange(visual.Event, dayStart);

            return new CalendarStackNode
            {
                Sibling = true,
                Index = 0,
                Visual = visual,
                Start = start,
                End = end,
                Children = new List<CalendarStackNode>()
            };
        }

        public static OneOf<int, bool> GetNextIndex(CalendarStackNode node, List<CalendarStackNode> nodes)
        {
            var indices = GetOverlappingIndices(node, nodes);
            indices.Sort();

            for (int i = 0; i < indices.Count; i++)
            {
                if (i < indices[i])
                    return i;
            }

            return false;
        }

        public static List<int> GetOverlappingIndices(CalendarStackNode node, List<CalendarStackNode> nodes)
        { 
            var indices = new List<int>();
            foreach (var other in nodes)
            {
                if(CalendarCommon.HasOverlap(node.Start, node.End, other.Start, other.End))
                    indices.Add(other.Index);
            }

            return indices;
        }

        public static CalendarStackNode GetParent(CalendarStackNode node, List<CalendarStackNode> nodes)
        {
            CalendarStackNode parent = null;
            foreach (var other in nodes)
            {
                if(CalendarCommon.HasOverlap(node.Start, node.End, other.Start, other.End) &&
                    (parent == null || other.Index > parent.Index))
                    parent = other;
            }

            return parent;
        }

        public static int AddTime(int identifier, int minutes)
        {
            var removeMinutes = identifier % 100;
            var totalMinutes = removeMinutes + minutes;
            var addHours = (int)Math.Floor((double)totalMinutes / 60);
            var addMinutes = totalMinutes % 60;

            return identifier - removeMinutes + addHours * 100 + addMinutes;
        }

        public static List<CalendarStackNode> GetOverlappingRange(CalendarStackNode node, 
            List<CalendarStackNode> nodes, int indexMin, int indexMax, bool returnFirstColumn = false)
        { 
            var overlapping = new List<CalendarStackNode>();
            foreach (var other in nodes)
            {
                if(other.Index >= indexMin && other.Index <= indexMax &&
                    CalendarCommon.HasOverlap(node.Start, node.End, other.Start, other.End))
                    overlapping.Add(other);
            }
            if (returnFirstColumn && overlapping.Count > 0)
            {
                var first = overlapping.Min(x => x.Index);
                return overlapping.Where(x => x.Index == first).ToList();
            }

            return overlapping;
        }

        public static void CalculateBounds(List<CalendarStackNode> nodes, int overlapThreshold)
        {
            foreach (var node in nodes)
            {
                var columns = GetMaxChildIndex(node) + 1;
                var spaceLeft = node.Parent?.Visual != null ? node.Parent.Visual.Left : 0;
                var spaceWidth = FullWidth - spaceLeft;
                var offset = Math.Min(DefaultOffset, FullWidth / columns);
                var columnWidthMultiplier = GetColumnWidthMultiplier(node, nodes);
                var columnOffset = spaceWidth / (columns - node.Index + 1);
                var columnWidth = spaceWidth / (columns - node.Index + (node.Sibling ? 1 : 0)) * columnWidthMultiplier;

                if (node.Parent?.Visual != null)
                    node.Visual.Left = node.Sibling ? spaceLeft + columnOffset : spaceLeft + offset;

                node.Visual.Width = HasFullWidth(node, nodes, overlapThreshold) ? FullWidth - node.Visual.Left :
                    Math.Min(FullWidth - node.Visual.Left, columnWidth * WidthMultiplter);
            }
        }

        public static int GetMaxChildIndex(CalendarStackNode node)
        {
            var max = node.Index;
            foreach (var child in node.Children)
            {
                var childMax = GetMaxChildIndex(child);
                if(childMax > max)
                    max = childMax;
            }

            return max;
        }

        public static int GetColumnWidthMultiplier(CalendarStackNode node, List<CalendarStackNode> nodes)
        {
            if ((node?.Children?.Count ?? 0) == 0)
                return 1;

            var maxColumn = node.Index + nodes.Count;
            var minColumn = Math.Min(node.Children.Min(x => x.Index), maxColumn);

            return minColumn - node.Index;
        }

        public static bool HasFullWidth(CalendarStackNode node,
            List<CalendarStackNode> nodes, int overlapThreshold)
        {
            foreach (var other in nodes)
            {
                if (other != node && other.Index > node.Index &&  CalendarCommon.HasOverlap(
                    node.Start, AddTime(node.Start, overlapThreshold), other.Start, other.End))
                    return false;
            }

            return true;
        }
    }
}
