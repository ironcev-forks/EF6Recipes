using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe16
{
    [Flags]
    public enum SponsorTypes
    {
        None = 0,
        ContributesMoney = 1,
        Volunteers = 2,
        IsABoardMember = 4
    }
}
