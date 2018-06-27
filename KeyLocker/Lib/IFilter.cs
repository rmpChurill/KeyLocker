namespace KeyLocker.Lib
{
    public interface IFilter<T>
    {
        bool IsValid(T item);
    }
}
