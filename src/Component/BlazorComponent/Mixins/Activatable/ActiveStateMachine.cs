using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ActiveStateMachine
    {
        private bool _activeRevertState = false;
        private volatile bool _active = false;
        private object _lock = new object();

        public void ForceSetActive(bool active)
        {
            _active = active;
        }

        public bool SetActive(bool active)
        {
            var isFired = false;

            if (!(isFired = TrySetRevert(active)))
                isFired = TrySetActive(active);

            return isFired;
        }

        private bool TrySetRevert(bool active)
        {
            bool isFired = false; ;

            if (_active == active)
            {
                lock (_lock)
                {
                    if (_active == active)
                    {
                        _activeRevertState = true;
                    }
                    else
                    {
                        TrySetActive(active);
                        isFired = true;
                    }
                }
            }

            return isFired;
        }

        private bool TrySetActive(bool active)
        {
            bool isFired = false;

            if (_activeRevertState)
            {
                lock (_lock)
                {
                    if (_activeRevertState)
                    {
                        _activeRevertState = false;
                    }
                    else
                    {
                        _active = active;
                        isFired = true;
                    }
                }
            }
            else
            {
                _active = active;
                isFired = true;
            }

            return isFired;
        }
    }
}
