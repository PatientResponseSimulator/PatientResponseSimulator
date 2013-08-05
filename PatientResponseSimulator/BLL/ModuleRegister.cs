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

// This class implements a static register for all the available modules

namespace PatientResponseSimulator.Modules
{
    /// <summary>
    /// this could have been a struct but added the override to ToString, so became a class
    /// it's used to collect data about Modules
    /// </summary>
    public class ModuleData
    {
        #region Fields
        public string moduleDescription;
        public string moduleName;
        public Type ModuleClass;
        #endregion

        public override string ToString()
        {
            return moduleName;

        }
    }
    /// <summary>
    /// class ModuleRegister is a static class that holds the Module names and descriptions to prompt the user for selection
    /// </summary>
    static class ModuleRegister
    {
        #region Fields
        
        private static List<ModuleData> modules;

        #endregion

        #region Constructors
        static ModuleRegister()
        {
            modules = new List<ModuleData>();
            DoseFindingContinuous.SelfRegister();
        }
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
