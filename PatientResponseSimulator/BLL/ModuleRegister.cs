//------------------------------------------------------------------------------------------
// <copyright file="ModuleRegister.cs" company="Tessella">
//     Copyright © Tessella 2013. All rights reserved.
// </copyright>
// <project>Tessella C# training exercise, Assay Scheduler</project>
// <summary></summary>
// <svn>
//  <lastChanged>$Date$</lastChanged>
//  <by>DIAT</by>
//  <version>$Revision$</version>
//  <source>$HeadURL$</source>
// </svn>
//------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientResponseSimulator.BLL
{
    public struct ModuleData
    {
        public string moduleDescription;
        public string moduleName;
        public Type ModuleClass;
    }
    /// <summary>
    /// class ModuleRegister is a static class that holds the Module names and descriptions to prompt the user for selection
    /// </summary>
    static class ModuleRegister
    {
        #region Fields
        
        private static List<ModuleData> modules;

        #endregion
        #region Methods
        /// <summary>
        /// Inserts a new module in the register
        /// </summary>
        /// <param name="dt"> Module data to be inserted </param>
        public static void InsertModule(ModuleData dt)
        {
            modules.Add(dt);
        }
        /// <summary>
        /// Get the number of modules in the register
        /// </summary>
        /// <returns></returns>
        public static int GetNumModules()
        {
            return modules.Count();
        }
        /// <summary>
        /// Gets the Module name 
        /// </summary>
        /// <param name="moduleIdx"> module index </param>
        /// <returns></returns>
        public static ModuleData GetModule(int moduleIdx)
        {
            return modules[moduleIdx];
        }

        #endregion
    }
}
