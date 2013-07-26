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
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using PatientResponseSimulator.BLL;
using System.Windows;

namespace PatientResponseSimulator.DAL
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
        /// <param name="subjectsToWrite">
        /// Subject_Manager instance.
        /// </param>
        public void OutputToFile(string directoryPath, string fileName, List<Subject> subjectsToWrite)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(directoryPath
                    + fileName + ".txt"))
                {
                    foreach (Subject s in subjectsToWrite)
                    {
                    }
                }
            }

            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show("Exception: Access to the directory denied. "
                    + "Choose a different directory, or check the folder permissions.");
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("Exception: Invalid file name. ");
            }
            catch (PathTooLongException e)
            {
                MessageBox.Show("Exception: Choose a shorter directory path. "
                    + "Path is longer than system allowed maximum length.");
            }
            catch (IOException e)
            {
                MessageBox.Show("Exception: IO Error, check file system and permissions");
            }
            catch (SecurityException e)
            {
                MessageBox.Show("Exception: Security exception, check security settings.");
            }
            catch (Exception e)
            {
                throw e; //
            }

        }

        #endregion
    }
}
