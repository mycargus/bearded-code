using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace ServerCore
{
    public class FormLogger : ILogger
    {
        private static Mutex _mutex = new Mutex();
        private Control _output;
        public bool AppendToLog { get; set; }      

        public FormLogger(Control a_FormControl, bool a_AppendToLog = true)
        {
            _output = a_FormControl;
            AppendToLog = a_AppendToLog;
        }

        public void WriteEntry(ArrayList a_Entry)
        {
            _mutex.WaitOne();

            if (!AppendToLog)
                _output.ResetText();

            IEnumerator line = a_Entry.GetEnumerator();
            while (line.MoveNext())
                _output.Text = String.Format("{0}\n{1}", _output.Text, line.Current);

            _mutex.ReleaseMutex();
        }

        public void WriteEntry(string a_Entry)
        {
            _mutex.WaitOne();

            _output.Text = AppendToLog ? String.Format("{0}\n{1}", _output.Text, a_Entry) : a_Entry;

            _mutex.ReleaseMutex();
        }
    }
}