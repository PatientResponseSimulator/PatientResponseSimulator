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

namespace PatientResponseSimulator.BLL
{
    class Subject_Manager : ISubject_Manager<Subject>
    {

        #region Fields

        private static Subject_Manager instance;

        private static List<Subject> subjectList;

        private static List<EndPoint> endpoints;

        private static uint populationSize;

        #endregion

        #region Constructors
        
        private Subject_Manager()
        {
            populationSize = 0;

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
        public void CreateDosePopulation(uint dosePopulationSize, uint doseID)
        {
            for (int i = 0; i < populationSize; i++)
            {
                subjectList.Add(new Subject(populationSize, doseID));
            }
        }

        /// <summary>
        /// Clears the population (subjectList)
        /// </summary>
        public void ClearPopulation()
        {
            subjectList.Clear();
            endpoints.Clear();

            populationSize = 0;
        }

        #endregion
    }
}
