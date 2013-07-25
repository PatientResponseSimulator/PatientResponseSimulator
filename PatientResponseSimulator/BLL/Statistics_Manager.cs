//------------------------------------------------------------------------------------------
// <copyright file="Statistics_Manager.cs" company="Tessella">
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

namespace PatientResponseSimulator
{
    /// <summary>
    /// Definition of the Statistics_Manager class.
    /// </summary>
    public class Statistics_Manager
    {
        #region Fields

        /// <summary>
        /// Private instance of the Statistics_Manager class.
        /// This is used to ensure that there is only once
        /// instance, and that the Statistic_Manager is a
        /// Singleton.
        /// </summary>
        private static Statistics_Manager instance;

        /// <summary>
        /// This is the List that will store the statistic type
        /// for each endpoint that we are simulating.
        /// </summary>
        private List<StatisticType> endpointStatisticTypes;

        /// <summary>
        /// This is the List that will store the history dependence
        /// requirement for each endpoint simulated.
        /// </summary>
        private List<bool> historyDependencies;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the Statistics_Manager class from being created.
        /// </summary>
        private Statistics_Manager()
        {
            endpointStatisticTypes = new List<StatisticType>();

            historyDependencies = new List<bool>();
        }

        #endregion

        #region Enums

        /// <summary>
        /// This enum lists the different types of statistical
        /// simulations that can be run.
        /// </summary>
        public enum StatisticType
        {
            /// <summary>
            /// Normal Distribution type
            /// </summary>
            Normal,

            /// <summary>
            /// Exponential Rise type
            /// </summary>
            ExponentialRise,

            /// <summary>
            /// Exponential Decay type
            /// </summary>
            ExponentialDecay
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the single instance of the Statistics Manager.
        /// </summary>
        public static Statistics_Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Statistics_Manager();
                }

                return instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to add an endpoint to the statistics manager
        /// </summary>
        /// <param name="endpointStatisticType">
        /// Allowed Values are Normal, ExponentialRise, and ExponentialDecay
        /// </param>
        /// <param name="historyDependence">
        /// true is there is a history dependence.
        /// </param>
        public void AddEndPoint(StatisticType endpointStatisticType, bool historyDependence)
        {
            endpointStatisticTypes.Add(endpointStatisticType);
            historyDependencies.Add(historyDependence);
            return;
        }

        /// <summary>
        /// Box-Mueller random number generator that yields a specified 
        /// mean and standard deviation. The Box-Mueller scheme implemented
        /// is the polar method.
        /// </summary>
        /// <param name="mean">
        /// Mean of the normal distribution created.
        /// </param>
        /// <param name="standardDeviation">
        /// Standard deviation of the distribution created.
        /// </param>
        /// <param name="distributionSize">
        /// The size of the distribution created.
        /// </param>
        /// <returns>
        /// Returns the desired normal distribution.
        /// </returns>
        private List<double> NormalDistribution(double mean, double standardDeviation, int distributionSize)
        {
            // Seed for random number generation
            Random r = new Random();

            List<double> normalDistribution = new List<double>();

            double u, v, s;

            for (int i = 0; i < distributionSize; i++)
            {
                do
                {
                    u = (2.0 * r.NextDouble()) - 1.0;
                    v = (2.0 * r.NextDouble()) - 1.0;
                    s = (u * u) + (v * v);
                }
                while (s >= 1.0);

                normalDistribution.Add(((u * Math.Sqrt(-2.0 * Math.Log(s) / s)) * standardDeviation) + mean);
            }

            return normalDistribution;
        }

        #endregion
    }
}
