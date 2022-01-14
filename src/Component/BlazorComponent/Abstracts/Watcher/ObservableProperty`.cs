using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ObservableProperty<TValue> : ObservableProperty
    {
        private bool _hasValue;
        private TValue _oldValue;
        private TValue _value;
        private IObservableProperty _internalProperty;

        public ObservableProperty(string name, TValue value)
            : base(name)
        {
            Value = value;
            _internalProperty = this;
        }

        public ObservableProperty(IObservableProperty property, TValue value)
            : base(property.Name)
        {
            _internalProperty = property;
            Value = value;
        }

        public TValue Value
        {
            get
            {
                return _value;
            }
            set
            {
                //First time set value
                if (!_hasValue && value is INotifyPropertyChanged notify)
                {
                    notify.PropertyChanged += (sender, args) =>
                    {
                        NotifyChange(_value, _oldValue);
                    };
                }

                //We can't detect whether reference type has changed
                //Only if we copy a new instance
                //We will think it over
                //
                //We just assume list always changed
                //This will be changed when we finished data-collect and deep watch
                if (!EqualityComparer<TValue>.Default.Equals(_value, value) || value is IList)
                {
                    _oldValue = _value;
                    _value = value;
                    _hasValue = true;

                    NotifyChange(_value, _oldValue);
                }
            }
        }

        public Func<TValue> ValueFactory { get; set; }

        /// <summary>
        /// If value has set return true,else false
        /// </summary>
        public bool HasValue => _hasValue;

        public event Action<TValue, TValue> OnValueChange;

        public void NotifyChange(TValue newValue, TValue oldValue)
        {
            OnValueChange?.Invoke(newValue, oldValue);
            _internalProperty?.NotifyChange();
        }
    }
}
