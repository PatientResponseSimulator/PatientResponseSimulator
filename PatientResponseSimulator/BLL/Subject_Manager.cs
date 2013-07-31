//------------------------------------------------------------------------------------------
// <copyright file="Subject_Manager.cs" company="Tessella">
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
using PatientResponseSimulator.DAL;

namespace PatientResponseSimulator.BLL
{
    class Subject_Manager : ISubject_Manager<Subject>
    {

        #region Fields

        /// <summary>
        /// field to store the manager instance
        /// </summary>
        private static Subject_Manager instance;

        /// <summary>
        /// list of subjects
        /// </summary>
        private static List<Subject> subjectList;

        /// <summary>
        /// list of endpoints that will have to be simulated.
        /// </summary>
        private static List<EndPoint> endpoints;

        /// <summary>
        /// the total population size
        /// </summary>
        private static uint totalPopulationSize;

        /// <summary>
        ///  field to keep track of what visit number we are on
        /// </summary>
        private static uint visits;

        #endregion

        #region Constructors
        
        private Subject_Manager()
        {
            totalPopulationSize = 0;

            subjectList = new List<Subject>();

            endpoints = new List<EndPoint>();
        }

        #endregion

        #region Properties 

        public static Subject_Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Subject_Manager();
                }
                return instance;
            }
        }

        #endregion

        #region Methods

        public int GetNewID()
        {
           return 5;
        }

        public Subject GetSubject(int ID)
        {
            Subject myGroupedSub = new Subject(1,1);

            return myGroupedSub;
        }

        public Subject AddSubject(int DoseID)
        {
            Subject myGroupedSub = new Subject(1,1);

            return myGroupedSub;
        }

        public int GetNumSubjects()
        {
            return 5;
        }

        public int AddEndpoint(string Name, EndpointType Type, List<int> VisitOccurances)
        {
            return 5;
        }

        /// <summary>
        /// Creates a dose population. 
        /// </summary>
        /// <param name="populationSize">
        /// Size of the population.
        /// </param>
        public void CreateDose(uint dosePopulationSize, uint doseID)
        {
            for (uint i = 0; i < dosePopulationSize; i++)
            {
                totalPopulationSize += 1;
                subjectList.Add(new Subject(totalPopulationSize, doseID));
            }
        }

        /// <summary>
        /// Clears the population (subjectList)
        /// </summary>
        public void ClearPopulations()
        {
            subjectList.Clear();
            endpoints.Clear();

            totalPopulationSize = 0;
            visits = 0;
        }

        public void SimulateVisit(uint doseID, uint samples, double mean, double stdDev, StatisticType type, EndPoint endpoint)
        {
            Statistics_Manager StatisticsManager = Statistics_Manager.Instance;

            StatisticsManager.SetSampleSize(samples);

            // Unnecessary line of code, for now.
            // StatisticsManager.AddEndPoint(type, false);

            List<double> data = new List<double>();

            switch (type)
            {
                case StatisticType.Normal:
                    data = StatisticsManager.NormalDistribution(mean, stdDev);
                    break;
                case StatisticType.Exponential:
                    data = StatisticsManager.ExponentialDistribution(mean);
                    break;
            }

            int i = 0;
            foreach (Subject s in subjectList.FindAll(o => o.DoseID == doseID))
            {
                s.SubjectResponses.Add(new VisitEndpoint(visits, data[i], endpoint.Type, endpoint.EndpointID));
                i++;
            }
        }

        public uint NewVisit()
        {
            visits += 1;
            return visits;
        }

        /// <summary>
        /// Calls the output manager to print the subject list.
        /// </summary>
        /// <param name="outputFileName">
        /// string containing the name of the file, with extension
        /// </param>
        /// <param name="outputDirectory">
        /// output directory string
        /// </param>
        public void WriteResults(string outputDirectory, string outputFileName)
        {
            Output_Manager OM = Output_Manager.Instance;

            OM.OutputToFile(outputDirectory, outputFileName, subjectList);
        }

        #endregion
    }
}
