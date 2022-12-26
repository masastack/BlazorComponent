using OneOf;

namespace BlazorComponent;

public partial class StringNumberOrMore : OneOfBase<StringNumber, List<StringNumber>>
{
    protected StringNumberOrMore(OneOf<StringNumber, List<StringNumber>> input) : base(input)
    {
    }

    public string AsString => AsT0.AsT0;

    public int AsInt => AsT0.AsT1;

    public double AsDouble => AsT0.AsT2;

    public StringNumber? FirstOrDefault()
    {
        if (IsT0)
        {
            return (StringNumber)Value;
        }

        return ((List<StringNumber>)Value).FirstOrDefault();
    }

    public StringNumber? LastOrDefault()
    {
        if (IsT0)
        {
            return (StringNumber)Value;
        }

        return ((List<StringNumber>)Value).LastOrDefault();
    }

    public List<StringNumber> ToList()
    {
        if (IsT0)
        {
            return new List<StringNumber>() { (StringNumber)Value };
        }

        return (List<StringNumber>)Value;
    }

    public static implicit operator StringNumberOrMore(int _) => new((StringNumber)_);
    public static implicit operator StringNumberOrMore(string _) => new((StringNumber)_);
    public static implicit operator StringNumberOrMore(double _) => new((StringNumber)_);

    public static implicit operator StringNumberOrMore(int[] _) => new(_.Select(i => (StringNumber)i).ToList());
    public static implicit operator StringNumberOrMore(string[] _) => new(_.Select(i => (StringNumber)i).ToList());
    public static implicit operator StringNumberOrMore(double[] _) => new(_.Select(i => (StringNumber)i).ToList());

    public static implicit operator StringNumberOrMore(List<int> _) => new(_.Select(i => (StringNumber)i).ToList());
    public static implicit operator StringNumberOrMore(List<string> _) => new(_.Select(i => (StringNumber)i).ToList());
    public static implicit operator StringNumberOrMore(List<double> _) => new(_.Select(i => (StringNumber)i).ToList());

    public static implicit operator StringNumberOrMore(StringNumber _) => new(_);
    public static implicit operator StringNumber(StringNumberOrMore _) => _.AsT0;

    public static implicit operator StringNumberOrMore(List<StringNumber> _) => new(_);
    public static explicit operator List<StringNumber>(StringNumberOrMore _) => _.AsT1;
}
