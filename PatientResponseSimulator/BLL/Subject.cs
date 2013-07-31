//------------------------------------------------------------------------------------------
// <copyright file=" GroupedSubject.cs" company="TESSELLA">
//     Copyright © SET COPYRIGHT IN THE SNIPPET FILE. All rights reserved.
// </copyright>
// <project>Tessella/NPD/PatientResponseSimulator</project>
// <summary></summary>
// <svn>
//  <lastChanged>$Date$</lastChanged>
//  <by>Tiziano Diamanti</by>
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
    public class Subject
    {
        #region fields

        protected uint subjectID;
        protected uint doseID;

        #endregion


        #region Properties
        public uint SubjectID
        {
            get
            {
                return subjectID;
            }
        }

        public uint DoseID
        {
            get
            {
                return doseID;
            }
        }
        public List<VisitEndpoint> SubjectResponses;

        #endregion

        #region Constructors
        public Subject(uint subjectid, uint doseid)
        {
            subjectID = subjectid;
            doseID = doseid;
            SubjectResponses = new List<VisitEndpoint>();
        }

        protected Subject()
        {
        }

        #endregion
    }
}
