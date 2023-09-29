namespace CodeBase.Common.Referables
{
    public class Referable<T> : IReadOnlyReferable<T>
    {
        public T Value { get; set; }
    }
}