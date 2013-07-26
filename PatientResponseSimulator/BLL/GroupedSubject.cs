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
    class GroupedSubject : Subject
    {
        protected uint groupID;

        public uint GroupID
        {
            get
            {
                return groupID;
            }
        }

        public GroupedSubject(uint subjectid, uint doseid, uint groupid) : base (subjectid, doseid)
        {
            groupID = groupid;
        }
    }
}
