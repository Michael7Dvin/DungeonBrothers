using System;
using CodeBase.Common.Referables;

namespace CodeBase.Common.Observables
{
    public interface IReadOnlyObservable<out T> : IReadOnlyReferable<T>
    {
        event Action<T> Changed;
    }
}