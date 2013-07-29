using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatientResponseSimulator.BLL
{
    interface ISubject_Manager <T>
    {
        int GetNewID();

        T GetSubject(int ID);

        T AddSubject(int doseID);

        int GetNumSubjects();

        int AddEndpoint(string Name, EndpointType Type, List<int> VisitOccurances);

        void ClearPopulation();
    }
}
