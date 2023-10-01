namespace CodeBase.Common.Referables
{
    public interface IReadOnlyReferable<out T>
    {
        T Value { get; }
    }
}