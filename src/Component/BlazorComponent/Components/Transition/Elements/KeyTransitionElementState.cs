using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public class KeyTransitionElementState<TValue>
    {
        private TValue _key;

        public KeyTransitionElementState(KeyTransitionElement<TValue> element)
        {
            Element = element;
        }

        protected object Value => Element.Value;

        protected Transition Transition => Element.Transition;

        protected string Class => Element.Class;

        protected string Style => Element.Style;

        protected KeyTransitionElement<TValue> Element { get; }

        /// <summary>
        /// Save transition state for element
        /// </summary>
        public TransitionState TransitionState { get; set; }

        public TValue Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                IsEmpty = false;
            }
        }

        public bool IsEmpty { get; set; } = true;

        public string ComputedClass
        {
            get
            {
                var transitionName = Transition.Name;
                if (transitionName == null || TransitionState == TransitionState.None)
                {
                    return Class;
                }

                var transitionClass = TransitionState switch
                {
                    TransitionState.Enter => $"{transitionName}-enter {transitionName}-enter-active",
                    TransitionState.EnterTo => $"{transitionName}-enter-active {transitionName}-enter-to",
                    TransitionState.Leave => $"{transitionName}-leave {transitionName}-leave-active",
                    TransitionState.LeaveTo => $"{transitionName}-leave-active {transitionName}-leave-to",
                    _ => throw new InvalidOperationException()
                };
                return string.Join(" ", Class, transitionClass);
            }
        }

        public string ComputedStyle
        {
            get
            {
                var styles = new List<string>();

                if (Style != null)
                {
                    styles.Add(Style);
                }

                return string.Join(';', styles);
            }
        }

        public void CopyTo(KeyTransitionElementState<TValue> state)
        {
            state.Key = Key;
            state.TransitionState = TransitionState;
        }

        public void Reset()
        {
            IsEmpty = true;
        }
    }
}
