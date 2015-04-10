using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServer
{
    interface IServerLogger
    {
        void WriteEntry(ArrayList a_Entry, Enum a_Enum);
        void WriteEntry(String a_Entry, Enum a_Enum);
    }
}
