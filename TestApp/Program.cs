using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices; //P/Invoke

//
//http://www.codeproject.com/Articles/14180/Using-Unmanaged-C-Libraries-DLLs-in-NET-Applicatio
//

namespace TestApp
{
    class Program
    {

        [DllImport("msvcrt.dll")]
        public static extern int puts(string c);
        [DllImport("msvcrt.dll")]
        internal static extern int _flushall();

        static void Main(string[] args)
        {
            puts("Test");
            _flushall();
        }
    }
}
