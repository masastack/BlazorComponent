using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract class TransitionElementBase<TValue> : Element
    {
        private TValue _value;
        private bool _hasValue;
        private CancellationTokenSource _cancellationTokenSource;

        [Parameter]
        public TValue Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (EqualityComparer<TValue>.Default.Equals(_value, value) && _hasValue)
                {
                    return;
                }

                _value = value;
                _hasValue = true;

                TryCancelTransition();
                StartTransition();
            }
        }

        [CascadingParameter]
        public Transition Transition { get; set; }

        protected bool FirstRender { get; set; } = true;

        private void TryCancelTransition()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected abstract void StartTransition();

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                FirstRender = false;
            }
        }

        protected async Task RequestAnimationFrameAsync(Func<Task> callback)
        {
            await Delay(16);
            await callback();
        }

        protected Task Delay(int duration)
        {
            return Task.Delay(duration, _cancellationTokenSource.Token);
        }
    }
}
