namespace LockFree
{
    public interface ILockFreeQueue<T>
    {
        void Enqueue(T obj);
        bool TryDequeue(out T result);
    }
}