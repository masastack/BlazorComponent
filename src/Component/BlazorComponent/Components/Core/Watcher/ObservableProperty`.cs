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
                //TODO:support ObservableCollection<>
                if (!EqualityComparer<TValue>.Default.Equals(_value, value) || value is IEnumerable)
                {
                    var oldValue = _value;
                    _value = value;
                    _hasValue = true;

                    NotifyChange(oldValue, value);

                    //Deep watch,is this ok?
                    if (_value is INotifyPropertyChanged notify)
                    {
                        notify.PropertyChanged += (sender, args) =>
                        {
                            NotifyChange(_value, _value);
                        };
                    }
                }
            }
        }

        public Func<TValue> ValueFactory { get; set; }

        /// <summary>
        /// If value has set return true,else false
        /// </summary>
        public bool HasValue => _hasValue;

        public event Action<TValue, TValue> OnValueChange;

        public void NotifyChange(TValue oldValue, TValue newValue)
        {
            OnValueChange?.Invoke(oldValue, newValue);
            _internalProperty?.NotifyChange();
        }
    }
}
