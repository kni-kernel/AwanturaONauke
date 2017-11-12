using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public class Team
    {
        public String Name { get; set; }
        public int Points { get; set; }
        public bool isPlaying { get; set; }
        public BlackBox BlackBox { get; set; }
        public int Hints { get; set; }
        public String ClassName { get; set; }
    }
}
