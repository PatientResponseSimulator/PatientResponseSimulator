//------------------------------------------------------------------------------------------
// <copyright file=" EndPoint.cs" company="TESSELLA">
//     Copyright © SET COPYRIGHT IN THE SNIPPET FILE. All rights reserved.
// </copyright>
// <project>Tessella/NPD/PatientResposeSimulator</project>
// <summary></summary>
// <svn>
//  <lastChanged>$Date$</lastChanged>
//  <by>$Author$</by>
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
    public enum EndpointType
    {
        Continous,
        Dichotomous,
        TimeToEvent
    };

    class Endpoint
    {
        #region Properties 
        #endregion

        #region Constructor
        Endpoint()
        {
            Value = 0.0f;
        }
        #endregion


        #region Methods
        #endregion

        double Value;
        EndpointType Type;
        string Name;
    }
}
