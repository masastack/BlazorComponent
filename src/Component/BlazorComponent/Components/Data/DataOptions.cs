using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class DataOptions : ObservableObject
    {
        public int Page
        {
            get
            {
                return GetValue<int>();
            }
            set
            {
                SetValue(value);
            }
        }

        public int ItemsPerPage
        {
            get
            {
                return GetValue<int>();
            }
            set
            {
                SetValue(value);
            }
        }

        public IList<bool> SortDesc
        {
            get
            {
                return GetValue<IList<bool>>();
            }
            set
            {
                SetValue(value);
            }
        }

        public IList<bool> GroupDesc
        {
            get
            {
                return GetValue<IList<bool>>();
            }
            set
            {
                SetValue(value);
            }
        }

        public bool MustSort
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        public bool MultiSort
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        public IList<string> SortBy
        {
            get
            {
                return GetValue<IList<string>>();
            }
            set
            {
                SetValue(value);
            }
        }

        public IList<string> GroupBy
        {
            get
            {
                return GetValue<IList<string>>();
            }
            set
            {
                SetValue(value);
            }
        }
    }
}
