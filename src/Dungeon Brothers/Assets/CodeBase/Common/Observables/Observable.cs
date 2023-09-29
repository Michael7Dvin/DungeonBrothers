using System;
using CodeBase.Common.Referables;


namespace CodeBase.Common.Observables
{
    public class Observable<T> : Referable<T>, IReadOnlyObservable<T> 
    {
        private T _value;
        public event Action<T> Changed;

        public new T Value
        {
            get => _value;

            set
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }
    }
}