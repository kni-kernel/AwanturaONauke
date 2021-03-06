﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    [Serializable]
    public class Question
    {
        public Question() { }

        public Category Category { get; set; }
        public String Content { get; set; }
        public String FileName { get; set; }
        public String Tip1 { get; set; }
        public String Tip2 { get; set; }
        public String Tip3 { get; set; }
        public String Tip4 { get; set; }
        public String Answear { get; set; }
        public bool Used { get; set; }
    }
}
