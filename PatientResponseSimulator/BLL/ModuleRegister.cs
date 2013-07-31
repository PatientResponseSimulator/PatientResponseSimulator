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
    /// <summary>
    /// class ModuleRegister is a static class that holds the Module names and descriptions to prompt the user for selection
    /// </summary>
    static class ModuleRegister
    {
        #region Fields
        
        private static List<string> moduleDescriptions;
        private static List<string> moduleNames;

        #endregion
        #region Methods

        public static void InsertModule(string ModuleName, string ModuleDescription)
        {
            moduleNames.Add(ModuleName);
            moduleDescriptions.Add(ModuleDescription);
        }

        public static int GetNumModules()
        {
            return moduleDescriptions.Count();
        }

        public static string GetModulesName(int moduleIdx)
        {
            return moduleNames[moduleIdx];
        }

        public static string GetModulesDescription(int moduleIdx)
        {
            return moduleDescriptions[moduleIdx];
        }

        #endregion
    }
}
