//------------------------------------------------------------------------------------------
// <copyright file="VisitEndpoint.cs" company="TESSELLA">
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

namespace PatientResponseSimulator.BLL
{
    /// <summary>
    /// Continous is a floating point value, can have real values
    /// Dichotomous is only 0 or 1 (represents boolean)
    /// TimeToEvent is meant to measure weeks, decimal represents days divided by 7
    /// </summary>
    public enum EndpointType
    {
        Continous,
        Dichotomous,
        TimeToEvent
    };

    public class VisitEndpoint
    {
        public uint VisitID;
        public double Value;
        public EndpointType Type;
        public int EndpointID;

        public VisitEndpoint(uint visitID, double value, EndpointType type, int endpointID)
        {
            VisitID = visitID;
            Value = value;
            Type = type;
            EndpointID = endpointID;
        }
    }
}
