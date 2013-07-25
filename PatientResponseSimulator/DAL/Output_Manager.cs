//------------------------------------------------------------------------------------------
// <copyright file="Output_Manager.cs" company="Tessella">
//     Copyright © Tessella 2013. All rights reserved.
// </copyright>
// <project>Tessella C# training exercise, Assay Scheduler</project>
// <summary></summary>
// <svn>
//  <lastChanged>$Date$</lastChanged>
//  <by>BAYA</by>
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
    /// Definition of the Output_Manager class.
    /// </summary>
    public class Output_Manager
    {
        #region Fields

        /// <summary>
        /// Singleton instance of the Output_Manager class.
        /// </summary>
        private static Output_Manager instance;

        /// <summary>
        /// String used for formatting the output
        /// </summary>
        private static string outputString;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the Output_Manager class from being created.
        /// </summary>
        private Output_Manager()
        {
            outputString = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the Output_Manager.
        /// </summary>
        public static Output_Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Output_Manager();
                }

                return instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the output string
        /// </summary>
        /// <param name="desiredString">
        /// An example string would be {0}, {1}, ...
        /// </param>
        public void SetOutputString(string desiredString)
        {
            outputString = desiredString;
        }

        /// <summary>
        /// Method for creating the output file with the list of
        /// subjects.
        /// </summary>
        /// <param name="directoryPath">
        /// Path to write the file.
        /// </param>
        /// <param name="fileName">
        /// Name of the file
        /// </param>
        /// <param name="SM">
        /// Subject_Manager instance.
        /// </param>
        public void OutputToFile(string directoryPath, string fileName, Subject subjectToWrite)
        {

        }

        #endregion
    }
}
