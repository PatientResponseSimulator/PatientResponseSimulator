using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientResponseSimulator.BLL
{
    /// <summary>
    /// Interface for Modules. All Modules must derive from this interface
    /// </summary>
    interface IModule
    {
        public static string Name
        {
            get;
        }

        public static string Description
        {
            get;
        }
    }
}
