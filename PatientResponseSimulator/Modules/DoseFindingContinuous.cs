﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatientResponseSimulator.BLL;
using System.Windows.Forms.DataVisualization.Charting;

namespace PatientResponseSimulator.Modules
{
    struct dose
        {
            public uint doseID;
            public uint populationSize;
            public double initialMean;
            public double finalMean;
            public double stdDev;
        }

    class DoseFindingContinuous
    {
        #region Fields

        /// <summary>
        /// private field to keep track of doses
        /// </summary>
        private uint numDoses;

        /// <summary>
        /// private field to store the list of doses being simulated.
        /// </summary>
        private List<dose> dosesToSimulate;

        /// <summary>
        /// field to store the number of visits that will occur
        /// </summary>
        private uint numVisits;

        /// <summary>
        /// The single EndPoint that will occur for this module.
        /// </summary>
        private EndPoint doseFindingEndpoint;

        /// <summary>
        /// Mean for the baseline data set
        /// </summary>
        private double baselineMean;

        /// <summary>
        /// Standard deviation for the basline data set.
        /// </summary>
        private double baselineStdDev;

        /// <summary>
        /// bool for determining if we need to simulate a baseline dataset
        /// </summary>
        private bool baseline;

        /// <summary>
        /// output directory string
        /// </summary>
        private string outputDirectory;

        #endregion 

        #region Constructors

        public DoseFindingContinuous()
        {
            numDoses = 0;

            dosesToSimulate = new List<dose>();

            numVisits = 0;

            doseFindingEndpoint = new EndPoint();

            doseFindingEndpoint.EndpointID = 0;
            doseFindingEndpoint.Type = EndpointType.Continous;
            doseFindingEndpoint.VisitOccurances = new List<int>();

            baselineMean = 10;

            baselineStdDev = 1;

            baseline = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Produces the data for a single population, and outputs the data
        /// to a single file specified by the paramter outputFile.
        /// </summary>
        /// <param name="outputFileName">
        /// string containing the name of the file, with extension
        /// </param>
        public void RunSinglePopulation(string outputFileName)
        {
            var SubjectManager = Subject_Manager.Instance;
            uint visitID;

            double visitMean;

            SubjectManager.ClearPopulations();
            foreach (dose d in dosesToSimulate)
            {
                SubjectManager.CreateDose(d.populationSize, d.doseID);
            }

            if (baseline)
            {
                foreach (dose d in dosesToSimulate)
                {
                    SubjectManager.SimulateVisit(d.doseID, d.populationSize, baselineMean, baselineStdDev, StatisticType.Normal, doseFindingEndpoint);
                }

            }

            for (uint i = 0; i < numVisits; i++)
            {
                // Create new visit
                visitID = SubjectManager.NewVisit();
                foreach (dose d in dosesToSimulate)
                {
                    // Linear interpolation of the mean based on the visit.
                    visitMean = (d.finalMean-d.initialMean)/(numVisits-1)*i + d.initialMean;
                    SubjectManager.SimulateVisit(d.doseID, d.populationSize, visitMean, d.stdDev, StatisticType.Normal, doseFindingEndpoint);
                }

            }

            // Write the output file
            SubjectManager.WriteResults(outputDirectory, outputFileName);
        }

        /// <summary>
        /// Sets the output directory where the files will be written.
        /// </summary>
        /// <param name="directory">
        /// directory where files will be written
        /// </param>
        public void SetOutputDirectory(string directory)
        {
            outputDirectory = directory;
        }

        /// <summary>
        /// Adds a new dose to the list of doses to be tested.
        /// </summary>
        /// <param name="dosePopulationSize">
        /// Size of the dose population
        /// </param>
        /// <param name="doseInitialMean">
        /// Initial mean of the data
        /// </param>
        /// <param name="doseFinalMean">
        /// Final mean of the data
        /// </param>
        /// <param name="doseStandardDeviation">
        /// Standard deviation of the data
        /// </param>
        public void AddDose(uint dosePopulationSize, double doseInitialMean, double doseFinalMean, double doseStandardDeviation)
        {
            // Incrament the dose count (also ID number)
            numDoses += 1;

            dose doseToInput = new dose();

            doseToInput.doseID = numDoses;
            doseToInput.populationSize = dosePopulationSize;
            doseToInput.initialMean = doseInitialMean;
            doseToInput.finalMean = doseFinalMean;
            doseToInput.stdDev = doseStandardDeviation;

            // Add the new dose to the dose List.
            dosesToSimulate.Add(doseToInput);
        }

        /// <summary>
        /// clears the list of doses that will be siumulated
        /// and sets the numer of doses to 0. Fresh start.
        /// </summary>
        public void RemoveAllDoses()
        {
            dosesToSimulate.Clear();
            numDoses = 0;
        }

        /// <summary>
        /// Sets the number of visits
        /// </summary>
        /// <param name="numVisitsInput">
        /// Number of visits
        /// </param>
        public void SetNumberVisits(uint numVisitsInput)
        {
            // Set the internal field.
            numVisits = numVisitsInput;

            // For the single doseFindingEndpoint endpoint, 
            // set the VisitOccurances to each visit.
            for (int i = 0; i < numVisits; i++)
            {
                doseFindingEndpoint.VisitOccurances.Add(i + 1);
            }

        }

        /// <summary>
        /// Turns on the requirement to produce a baseline data set. 
        /// </summary>
        /// <param name="baselineMeanInput">
        /// Baseline mean
        /// </param>
        /// <param name="baselineStdDevInput">
        /// Baseline standard deviation
        /// </param>
        public void TurnOnBaseline(double baselineMeanInput, double baselineStdDevInput)
        {
            baseline = true;
            baselineMean = baselineMeanInput;
            baselineStdDev = baselineStdDevInput;
        }

        /// <summary>
        /// Turns off the requirement to produce a baseline data set.
        /// </summary>
        public void TurnOffBaseline()
        {
            baseline = false;
        }
 
        #endregion
    }
}
