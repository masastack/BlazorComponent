﻿using System.Text.Json;

namespace BlazorComponent;

public class FileChangeCallBack
{
    public string JsCallback { get; init; }

    public EventCallback<IReadOnlyList<IBrowserFile>> EventCallback { get; init; }

    public Func<IReadOnlyList<IBrowserFile>, Task> DelegateCallback { get; init; }

    public bool IsJsCallback => JsCallback is not null;

    public bool IsEventCallback => EventCallback.Equals(default);

    public bool IsDelegateCallback => DelegateCallback is not null;

    public JsonElement JsCallbackValue { get; internal set; }

    public static implicit operator FileChangeCallBack(string callback)
    {
        return new FileChangeCallBack { JsCallback = callback };
    }

    public static implicit operator FileChangeCallBack(EventCallback<IReadOnlyList<IBrowserFile>> callback)
    {
        return new FileChangeCallBack { EventCallback = callback };
    }

    public static FileChangeCallBack CreateCallback(string callback)
    {
        return new FileChangeCallBack { JsCallback = callback };
    }

    public static FileChangeCallBack CreateCallback(ComponentBase receiver, Action<IReadOnlyList<IBrowserFile>> callback)
    {
        return new FileChangeCallBack
        {
            EventCallback = Microsoft.AspNetCore.Components.EventCallback.Factory.Create(receiver, callback)
        };
    }

    public static FileChangeCallBack CreateCallback(Func<IReadOnlyList<IBrowserFile>, Task> callback)
    {
        return new FileChangeCallBack { DelegateCallback = callback };
    }
}

