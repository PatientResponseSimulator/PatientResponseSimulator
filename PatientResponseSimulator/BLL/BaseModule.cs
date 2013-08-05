using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace PatientResponseSimulator.Modules
{
    /// <summary>
    /// All Module classes must derive from this base class
    /// Contains name, description of the module (for the user)
    /// 
    /// </summary>
    abstract class BaseModule
    {
        #region Fields
        protected static string name;
        protected static string description;
        #endregion

        #region Methods
        /// <summary>
        /// returns the class type passed (used for reflection)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected static Type getType<T>()
        {
            Type t = typeof(T);
            return t;
        }
        #endregion
    }
}
