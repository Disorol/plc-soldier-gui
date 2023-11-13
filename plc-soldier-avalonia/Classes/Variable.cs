﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plc_soldier_avalonia.Classes
{
    public interface IVariable
    {
        public string Name { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsList { get; set; }
    }
}
