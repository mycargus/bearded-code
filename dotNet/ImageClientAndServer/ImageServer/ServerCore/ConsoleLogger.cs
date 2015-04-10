using System;
using System.Collections;
using System.Threading;

namespace ServerCore
{
    public class ConsoleLogger : ILogger
    {
        private static Mutex _mutex = new Mutex();

        public void WriteEntry(ArrayList a_Entry)
        {
            _mutex.WaitOne();

            IEnumerator line = a_Entry.GetEnumerator();
            while (line.MoveNext())
                Console.WriteLine(line.Current);

            Console.WriteLine();

            _mutex.ReleaseMutex();
        }

        public void WriteEntry(string a_Entry)
        {
            _mutex.WaitOne();

            Console.WriteLine(a_Entry);
            Console.WriteLine();

            _mutex.ReleaseMutex();
        }
    }
}
