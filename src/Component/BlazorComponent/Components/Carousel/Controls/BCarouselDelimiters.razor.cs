﻿namespace BlazorComponent;

public partial class BCarouselDelimiters<TCarousel> : ComponentPartBase<TCarousel> where TCarousel : ICarousel
{
    public bool HideDelimiters => Component.HideDelimiters;

    public string Icon => Component.DelimiterIcon ?? "mdi-circle";

    public StringNumber InternalValue => Component.InternalValue;

    public bool Mandatory => Component.Mandatory;

    public List<IGroupable> Items => Component.Items;

    public EventCallback<StringNumberOrMore> InternalValueChanged => EventCallback.Factory.Create<StringNumberOrMore>(Component, Component.InternalValueChanged);
}
