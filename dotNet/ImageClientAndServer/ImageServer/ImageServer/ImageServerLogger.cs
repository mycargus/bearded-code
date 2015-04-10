using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServer
{
    class ImageServerLogger
    {
        public enum Destination 
        {
            All = 1,
            UserInterface = 2,
            LogFile = 3
        }

        private FormLogger _formLogger;
        private FileLogger _fileLogger;

        public ImageServerLogger(FormLogger a_FormLogger, FileLogger a_FileLogger)
        {
            _formLogger = a_FormLogger;
            _fileLogger = a_FileLogger;
        }

        public void WriteEntry(ArrayList a_Entry, Destination a_Destination)
        {
            switch (a_Destination)
            {
                case Destination.UserInterface:
                    _formLogger.WriteEntry(a_Entry);
                    break;
                case Destination.LogFile:
                    _fileLogger.WriteEntry(a_Entry);
                    break;
                case Destination.All:
                    _formLogger.WriteEntry(a_Entry);
                    _fileLogger.WriteEntry(a_Entry);
                    break;
            }
        }

        public void WriteEntry(string a_Entry, Destination a_Destination)
        {
            switch (a_Destination)
            {
                case Destination.UserInterface:
                    _formLogger.WriteEntry(a_Entry);
                    break;
                case Destination.LogFile:
                    _fileLogger.WriteEntry(a_Entry);
                    break;
                case Destination.All:
                    _formLogger.WriteEntry(a_Entry);
                    _fileLogger.WriteEntry(a_Entry);
                    break;
            }
        }

    }
}
