namespace BlazorComponent
{
    public interface ISlideGroup : IHasProviderComponent
    {
        string ActiveClass { get; }

        bool CenterActive { get; }

        bool Mandatory { get; }

        StringNumber Max { get; }

        bool Multiple { get; }

        string NextIcon { get; }

        string PrevIcon { get; }

        internal void OnAffixClick(string direction);

        bool HasNext { get; }

        bool HasPrev { get; }

        StringBoolean ShowArrows { get; }
    }
}