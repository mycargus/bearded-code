using System.Collections;
using System.Reflection;
using System.Threading;

namespace mycargus.Core
{
    public class FormLogger : ILogger
    {
        private static Mutex _mutex = new Mutex();
        private FieldInfo _output;
        public bool AppendToLog { get; set; }      

        public FormLogger(FieldInfo a_FormFieldInfo, bool a_AppendToLog = true)
        {
            _output = a_FormFieldInfo;
            AppendToLog = a_AppendToLog;
        }



        public void WriteEntry(ArrayList a_Entry)
        {
            _mutex.WaitOne();

            IEnumerator line = a_Entry.GetEnumerator();

            _mutex.ReleaseMutex();
        }

        public void WriteEntry(string a_Entry)
        {
            _mutex.WaitOne();



            _mutex.ReleaseMutex();
        }
    }
}