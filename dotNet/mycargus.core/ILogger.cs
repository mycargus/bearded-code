using System;
using System.Collections;

namespace mycargus.Core
{
    public interface ILogger
    {
        void WriteEntry(ArrayList a_Entry);
        void WriteEntry(String a_Entry);
    }
}
