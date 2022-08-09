using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public partial class BMobilePickerColumn<TColumnItem, TColumnItemValue>
{
    [CascadingParameter] public BMobilePicker<TColumnItem, TColumnItemValue> Parent { get; set; }

    [Parameter] public List<TColumnItem> Items { get; set; } = new();

    [Parameter] public int ItemHeight { get; set; }

    [Parameter] public Func<TColumnItem, string> ItemText { get; set; }

    [Parameter] public Func<TColumnItem, bool> ItemDisabled { get; set; } = _ => false;

    [Parameter] public int DefaultIndex { get; set; }

    [Parameter] public int SwipeDuration { get; set; }

    [Parameter] public StringNumber VisibleItemCount { get; set; }

    [Parameter] public EventCallback<int> OnChange { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        CurrentIndex = DefaultIndex;

        Parent.Register(this);

        SetIndex(CurrentIndex);
    }

    private const int DEFAULT_DURATION = 200;

    // 惯性滑动思路:
    // 在手指离开屏幕时，如果和上一次 move 时的间隔小于 `MOMENTUM_LIMIT_TIME` 且 move
    // 距离大于 `MOMENTUM_LIMIT_DISTANCE` 时，执行惯性滑动
    private const int MOMENTUM_LIMIT_TIME  = 300;
    private const int MOMENTUM_LIMIT_DISTANCE  = 15;

    public ElementReference Wrapper { get; set; }

    private int Count => Items.Count;

    protected int BaseOffset => (ItemHeight * (VisibleItemCount.ToInt32() - 1)) / 2;

    public async Task OnTouchstart(TouchEventArgs args)
    {
        // if (Readonly) return;

        Touchstart(args);

        if (_moving)
        {
            var translateY = await JsInvokeAsync<double>(JsInteropConstants.GetElementTranslateY, Wrapper);
            Console.WriteLine($"translateY:{translateY}");
            _offset = Math.Min(0, translateY - BaseOffset);
            _startOffset = _offset;
        }
        else
        {
            _startOffset = _offset;
        }

        _duration = 0;
        _transitionEndTrigger = null;
        _touchStartTime = DateTime.Now.Millisecond;
        _momentumOffset = _startOffset;
    }

    public void OnTouchmove(TouchEventArgs args)
    {
        // if (Readonly) return;

        Touchmove(args);

        if (_direction == "vertical")
        {
            _moving = true;
            // TODO: preventDefault(args, true);
        }

        _offset = Range(_startOffset + _deltaY, -(Count * ItemHeight), ItemHeight);

        var now = DateTime.Now.Millisecond;
        if (now - _touchStartTime > MOMENTUM_LIMIT_TIME)
        {
            _touchStartTime = now;
            _momentumOffset = _offset;
        }
    }

    public void OnTouchend(TouchEventArgs args)
    {
        // if (Readonly) return;

        var distance = _offset - _momentumOffset;
        var duration = DateTime.Now.Millisecond - _touchStartTime;
        var allowMomentum = duration < MOMENTUM_LIMIT_TIME && Math.Abs(distance) > MOMENTUM_LIMIT_DISTANCE;

        if (allowMomentum)
        {
            Momentum(distance, duration);
            return;
        }

        var index = GetIndexByOffset(_offset);
        _duration =  DEFAULT_DURATION;
        SetIndex((int)Math.Ceiling(index), true);

        // compatible with desktop scenario
        // use setTimeout to skip the click event Emitted after touchstart
        // setTimeout(() => { _moving = false; }, 0);
        _moving = false; // or next tick?
    }

    private void Momentum(double distance, long duration)
    {
        var speed = Math.Abs(distance / duration);
        distance = _offset + (speed / 0.003) * (distance < 0 ? -1 : 1);
        var index = GetIndexByOffset(distance);

        _duration = SwipeDuration;
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} _duration:{_duration}");
        StateHasChanged();
        SetIndex((int)Math.Ceiling(index), true);
    }

    private double GetIndexByOffset(double offset)
    {
        return Range(Math.Round(-offset / ItemHeight), 0, Count - 1);
    }

    private void SetIndex(int index, bool emitChange = false)
    {
        index = AdjustIndex(index) ?? 0;

        var offset  = -index * ItemHeight;

        var trigger = async () =>
        {
            if (index != CurrentIndex)
            {
                CurrentIndex = index;

                if (emitChange)
                {
                    await OnChange.InvokeAsync(index);
                }
            }
        };

        if (_moving && offset != _offset)
        {
            _transitionEndTrigger = trigger;
        }
        else
        {
            trigger();
        }

        _offset = offset;
    }

    private void OnClickItem(int index)
    {
        if (_moving /* ||  Readonly*/)
        {
            return;
        }

        _transitionEndTrigger = null;
        _duration = DEFAULT_DURATION;
        SetIndex(index, true);
    }

    private int? AdjustIndex(int index)
    {
        index = Range(index, 0, Count);

        for (int i = index; i < Count; i++)
        {
            if (!ItemDisabled(Items[i])) return i;
        }

        for (int i = index - 1; i >= 0; i--)
        {
            if (!ItemDisabled(Items[i])) return i;
        }

        return null;
    }

    private double Range(double num, double min, double max)
    {
        return Math.Min(Math.Max(num, min), max);
    }

    private int Range(int num, int min, int max)
    {
        return Math.Min(Math.Max(num, min), max);
    }

    private bool _moving;
    protected double _offset;
    private double _startOffset;
    protected int _duration;
    private Func<Task> _transitionEndTrigger;
    private long _touchStartTime;
    private double _momentumOffset;
    private double _startX;
    private double _startY;
    private double _deltaX;
    private double _deltaY;
    private double _offsetX;
    private double _offsetY;
    private string _direction;
    internal int CurrentIndex { get; set; }

    private void Touchstart(TouchEventArgs args)
    {
        ResetTouchStatus();
        _startX = args.Touches[0].ClientX;
        _startY = args.Touches[0].ClientY;
    }

    private void Touchmove(TouchEventArgs args)
    {
        var touch = args.Touches[0];

        _deltaX = touch.ClientX < 0 ? 0 : touch.ClientX - _startX;
        _deltaY = touch.ClientY - _startY;
        _offsetX = Math.Abs(_deltaX);
        _offsetY = Math.Abs(_deltaY);

        const int LOCK_DIRECTION_DISTANCE = 10;

        if (string.IsNullOrEmpty(_direction) || (_offsetX < LOCK_DIRECTION_DISTANCE && _offsetY < LOCK_DIRECTION_DISTANCE))
        {
            _direction = getDirection(_offsetX, _offsetY);
        }

        string getDirection(double x, double y)
        {
            if (x > y)
            {
                return "horizontal";
            }

            if (y > x)
            {
                return "vertical";
            }

            return string.Empty;
        }
    }

    private void ResetTouchStatus()
    {
        _direction = string.Empty;
        _deltaX = 0;
        _deltaY = 0;
        _offsetX = 0;
        _offsetY = 0;
    }

    private async Task OnTransitionEnd()
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} OnTransitionEnd");
        
        await StopMomentum();
    }

    public async Task StopMomentum()
    {
        _moving = false;
        _duration = 0;

        if (_transitionEndTrigger is not null)
        {
            await _transitionEndTrigger.Invoke();
            _transitionEndTrigger = null;
        }
    }

    public TColumnItem GetItem()
    {
        return Items.ElementAtOrDefault(CurrentIndex);
    }
}
