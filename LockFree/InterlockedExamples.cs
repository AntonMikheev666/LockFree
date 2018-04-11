using System;
using System.Threading;

namespace LockFree
{
    public class InterlockedExamples
    {
        public void AtomicWrite(ref long location, long value)
        {
            if (IntPtr.Size == 4)
                Interlocked.Exchange(ref location, value);
            else
                location = value;
        }

        public long AtomicReadWrong(ref long location)
        {
            return IntPtr.Size == 4 ? Interlocked.Exchange(ref location, location) : location;
        }

        public long AtomicReadInefficient(ref long location)
        {
            return IntPtr.Size == 4 ? Interlocked.CompareExchange(ref location, 0L, 0L) : location;
        }

        public long AtomicRead(ref long location)
        {
            return IntPtr.Size == 4 ? Interlocked.Read(ref location) : location;
        }

        public long InterlockedAdd(ref long location, long value)
        {
            long currentValue, newValue;

            do
            {
                currentValue = Interlocked.Read(ref location);
                newValue = currentValue + value;
            } while (Interlocked.CompareExchange(ref location, newValue, currentValue) != currentValue);

            return newValue;
        }
    }
}