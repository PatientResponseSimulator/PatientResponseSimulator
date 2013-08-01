using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientResponseSimulator.BLL
{
    /// <summary>
    /// All Module classes must derive from this base class
    /// </summary>
    class BaseModule
    {
        private static string name;
        private static string description;
        private static Type getType<T>()
        {
            Type t = typeof(T);
            return t;
        }
    }
}
