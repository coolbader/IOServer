using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOServer.Logic.Models
{
    public enum BusinessState
    {
        Idle,
        Start,
        Work,
        Completed,
        Listening,
        Ended
    }
}
