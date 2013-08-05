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
using System.Windows.Forms.DataVisualization.Charting;

namespace PatientResponseSimulator.BLL
{
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
        /// Exponential distribution
        /// </summary>
        Exponential,
    }

    #endregion

    #region Structs

    public struct CDF
    {
        public List<double> Q;
        public List<double> x;
    }

    #endregion

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

        /// <summary>
        /// This integer sotres the size of the sample we are 
        /// generating statistics for.
        /// </summary>
        private uint sampleSize;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the Statistics_Manager 
        /// class from being created external to the class. Also
        /// initializes default values.
        /// </summary>
        private Statistics_Manager()
        {
            endpointStatisticTypes = new List<StatisticType>();

            historyDependencies = new List<bool>();

            sampleSize = 0;
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

        public uint SampleSize
        {
            get
            {
                return sampleSize;
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
        /// This method sets the internal field used for 
        /// storing the size of the population we are
        /// generating samples for.
        /// </summary>
        /// <param name="desiredSampleSize">
        /// Desired sample/population size.
        /// </param>
        /// <returns>
        /// Confirms desired sample/population size.
        /// </returns>
        public uint SetSampleSize(uint desiredSampleSize)
        {
            sampleSize = desiredSampleSize;

            return sampleSize;
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
        public List<double> NormalDistribution(double mean, double standardDeviation)
        {
            // Seed for random number generation
            Random r = new Random();

            List<double> normalDistribution = new List<double>();

            double u, v, s;

            for (int i = 0; i < sampleSize/2; i++)
            {
                do
                {
                    u = (2.0 * r.NextDouble()) - 1.0;
                    v = (2.0 * r.NextDouble()) - 1.0;
                    s = (u * u) + (v * v);
                }
                while (s >= 1.0);

                normalDistribution.Add(u);
                normalDistribution.Add(v);
            }

            // If the value of the distribution size is odd, we need to add
            // one last point to the list.
            if (sampleSize % 2 != 0)
            {
                do
                {
                    u = (2.0 * r.NextDouble()) - 1.0;
                    v = (2.0 * r.NextDouble()) - 1.0;
                    s = (u * u) + (v * v);
                }
                while (s >= 1.0);

                normalDistribution.Add(u);
            }

            // Calculate the mean and standard deviation of the current
            // distribution (should be close to 0 and 1, respectively).
            // Use this information, along with the desired results, 
            // to reshape the distribution.
            double calculatedMean = normalDistribution.Average();
            double calculatedStdDev = normalDistribution.Sum(d => Math.Pow(d - calculatedMean, 2));
            calculatedStdDev = Math.Sqrt((calculatedStdDev) / (normalDistribution.Count));

            // Reshaping
            for (int i = 0; i < normalDistribution.Count; i++)
            {
                normalDistribution[i] = mean + (standardDeviation / calculatedStdDev) * (normalDistribution[i] - calculatedMean);
            }

            return normalDistribution;
        }

        /// <summary>
        /// Box-Mueller random number generator that yields a specified 
        /// mean and standard deviation. The Box-Mueller scheme implemented
        /// is the polar method.
        /// </summary>
        /// <param name="mean">
        /// Mean of the exponential distribution. It is also the same 
        /// value as the standard deviation. The decay rate parameter
        /// lambda is equal to 1/mean. 
        /// </param>
        /// <param name="distributionSize">
        /// The size of the distribution created.
        /// </param>
        /// <returns>
        /// Returns the desired normal distribution.
        /// </returns>
        public List<double> ExponentialDistribution(double mean)
        {
            // Seed for random number generation
            Random r = new Random();

            List<double> exponentialDistribution = new List<double>();

            // Rate parameter
            double lambda = 1 / mean;

            for (int i = 0; i < sampleSize; i++)
            {
                exponentialDistribution.Add(Math.Log(1-r.NextDouble())/(-1*lambda));
            }

            // Correct the final mean value to match the desired mean.
            double distMean = exponentialDistribution.Average();
            for (int i = 0; i < exponentialDistribution.Count(); i++)
            {
                exponentialDistribution[i] = exponentialDistribution[i] - distMean + mean;
            }

            return exponentialDistribution;
        }

        /*
        /// <summary>
        /// Incomplete lower gamma function, power series estimation
        /// </summary>
        /// <param name="s">
        /// gamma function input
        /// </param>
        /// <param name="x">
        /// interface value between lower and upper Gamma function
        /// </param>
        /// <returns>
        /// Returns incomplete_lower_gamma(s,x)
        /// </returns>
        public double IncompleteLowerGamma(double s, double x)
        {
            Chart myStatFormula = new Chart();

            double intermediate = Math.Pow(x, s);

            intermediate = intermediate * myStatFormula.DataManipulator.Statistics.GammaFunction(s);
            intermediate = intermediate * Math.Exp(-1 * x);

            double sum = 0;
            int loops = 200;

            for (int k = 0; k < loops; k++)
            {
                sum = sum + Math.Pow(x, k) / myStatFormula.DataManipulator.Statistics.GammaFunction(s + k + 1);   
            }

            return intermediate*sum;
        }

        /// <summary>
        /// Incomplete upper gamma function
        /// </summary>
        /// <param name="s">
        /// gamma function input
        /// </param>
        /// <param name="x">
        /// interface value between lower and upper Gamma function
        /// </param>
        /// <returns>
        /// Returns incomplete_upper_gamma(s,x)
        /// </returns>
        public double IncompleteUpperGamma(double s, double x)
        {
            Chart myStatFormula = new Chart();

            return (myStatFormula.DataManipulator.Statistics.GammaFunction(s) - IncompleteLowerGamma(s, x));
        }

        /// <summary>
        /// Inverse Gamma distribution
        /// </summary>
        /// <param name="alpha">
        /// Distribution shape factor
        /// </param>
        /// <param name="beta">
        /// Distribution scale parameter
        /// </param>
        /// <returns>
        /// Returns a distribution of points that fall along 
        /// an inverse-gamma distribution.
        /// </returns>
        public List<double> InverseGammaDistribution(double mean, double weight)
        {
            double beta = weight;

            double alpha = weight / mean + 1;

            List<double> igDistribution = new List<double>();

            // Calculate the CDF of the inverse Gamma function.
            CDF igCDF = InverseGammaCDF(alpha, beta);

            // Seed for random number generation
            Random r = new Random();

            double u;
            int j;

            for (int i = 0; i < sampleSize; i++)
            {
                // Random number generation
                u = r.NextDouble();

                // find the index where the random number fits between
                // the discrete CDF values;
                j = igCDF.Q.FindIndex(o => (double)o > u)-1;

                // Interpolate between the x values of the CDF, and add to the distribution;
                igDistribution.Add((igCDF.x[j + 1] - igCDF.x[j]) / (igCDF.Q[j + 1] - igCDF.Q[j]) * (u - igCDF.Q[j]) + igCDF.x[j]);
            }

            return igDistribution;
        }

        /// <summary>
        /// Inverse Gamma Function CDF
        /// </summary>
        /// <param name="alpha">
        /// Distribution shape factor
        /// </param>
        /// <param name="beta">
        /// Distribution scale parameter
        /// </param>
        /// <returns>
        /// Returns the CDF associated with the Gamma function
        /// defined by parameters alpha and beta.
        /// </returns>
        public CDF InverseGammaCDF(double alpha, double beta)
        {
            CDF igCDF = new CDF();
            igCDF.Q = new List<double>();
            igCDF.x = new List<double>();

            Chart myStatFormula = new Chart();

            double Q = 0;
            double x = 0.01;
            while (Q < .9999)
            {
                Q = IncompleteUpperGamma(alpha, beta / x) / myStatFormula.DataManipulator.Statistics.GammaFunction(alpha);
                igCDF.Q.Add(Q);
                igCDF.x.Add(x);

                x += 0.005;
            }

            return igCDF;
        }
        */


        /// <summary>
        /// Gamma Distribution function
        /// </summary>
        /// <param name="mean">
        /// Final mean value of the randomly generated points
        /// </param>
        /// <param name="shapeFactor">
        /// Shape factor. Must be greater than 1. If it is less than 1,
        /// it will be set to 1.
        /// </param>
        /// <returns>
        /// Returns a list of randomly distributed numbers that follow
        /// a Gamma distribution. 
        /// </returns>
        public List<double> GammaDistribution(double alpha, double beta)
        {
            // Code derived from John D. Cook's open source 
            // random number generation code.
            // Implementation based on "A Simple Method for Generating Gamma Variables"
            // by George Marsaglia and Wai Wan Tsang.  ACM Transactions on Mathematical Software
            // Vol 26, No 3, September 2000, pages 363-372.

            double mean = alpha/beta;

            // Random number seed
            Random r = new Random();

            List<double> normalDist = NormalDistribution(0,1);

            List<double> gammaDist = new List<double>();

            double d, c, x, xsquared, v, u, u1, u2, theta;

            if (alpha >= 1.0)
            {
                d = alpha - 1.0/3.0;
                c = 1.0/Math.Sqrt(9.0*d);
                while (gammaDist.Count < sampleSize)
                {
                    do
                    {
                        // Use Box-Muller polar for a normal distributed
                        // number x
                        u1 = r.NextDouble();
                        u2 = r.NextDouble();
                        theta = 2.0*Math.PI*u2;
                        x =  Math.Sqrt( -2.0*Math.Log(u1) )*Math.Sin(theta);
                        v = 1.0 + c*x;
                    }
                    while (v <= 0.0);
                    v = v*v*v;
                    u = r.NextDouble();
                    xsquared = x*x;
                    if (u < 1.0 -.0331*xsquared*xsquared || Math.Log(u) < 0.5*xsquared + d*(1.0 - v + Math.Log(v)))
                        gammaDist.Add(beta*d*v);
                }

                // Correct mean
                double gammaMean = gammaDist.Average();
                for (int i = 0; i < gammaDist.Count; i++)
                {
                    gammaDist[i] = gammaDist[i] - gammaMean + mean;
                }
                return gammaDist;
            }
            else
            {
                return GammaDistribution(mean, 1);
            }
        }

        public List<double> InverseGammaDistribution(double mean, double shapeFactor)
        {
            double alpha = shapeFactor;
            double beta = mean*(alpha-1);

            List<double> inverseGammaDist = new List<double>();

            inverseGammaDist = GammaDistribution(alpha, beta);

            for (int i = 0; i < inverseGammaDist.Count; i++)
            {
                inverseGammaDist[i] = 1/inverseGammaDist[i];
            }

            double igammaMean = inverseGammaDist.Average();

            for (int i = 0; i < inverseGammaDist.Count; i++)
            {
                inverseGammaDist[i] = inverseGammaDist[i] - igammaMean + mean;
            }

            return inverseGammaDist;
        }

        #endregion
    }
}
