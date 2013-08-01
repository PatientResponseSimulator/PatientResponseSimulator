//------------------------------------------------------------------------------------------
// <copyright file="Window1.xaml.cs" company="Tessella">
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
using PatientResponseSimulator.BLL;
using PatientResponseSimulator.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PatientResponseSimulator
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// Initializes a new instance of the Window1 class
        /// </summary>
        public Window1()
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Statistics_Manager SM = Statistics_Manager.Instance;

            List<double> values = new List<double>();

            SM.SetSampleSize(10000);
            values = SM.InverseGammaDistribution(1, 1);

            try
            {

                using (StreamWriter sw = new StreamWriter("C:\\Tessella\\Values.txt"))
                {
                    foreach(double d in values)
                    {
                        sw.WriteLine(d.ToString());
                    }
                    sw.Close();
                }
            }
            catch(Exception excep)
            {
                MessageBox.Show("Error! \n" + excep.ToString());
            }

            /*
            DoseFindingContinuous dfc = new DoseFindingContinuous();

            dfc.AddDose(1500, 120, 90, 15);
            dfc.AddDose(2500, 120, 50, 1);
            dfc.AddDose(2000, 120, 150, 30);
            dfc.AddDose(3000, 120, 120, 15);

            dfc.SetOutputDirectory("c:\\Tessella\\");

            dfc.SetNumberVisits(10);

            dfc.RunSinglePopulation("TestPopulation2.txt");
            */
        }
    }
}
