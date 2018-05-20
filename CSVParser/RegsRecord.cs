// ******************************************************************************************************************
//  CSVParser - parses a directory of CSV files, then generates a report based on the file data.
//  Copyright(C) 2018  James LoForti
//  Contact Info: jamesloforti@gmail.com
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
//									     ____.           .____             _____  _______   
//									    |    |           |    |    ____   /  |  | \   _  \  
//									    |    |   ______  |    |   /  _ \ /   |  |_/  /_\  \ 
//									/\__|    |  /_____/  |    |__(  <_> )    ^   /\  \_/   \
//									\________|           |_______ \____/\____   |  \_____  /
//									                             \/          |__|        \/ 
//
// ******************************************************************************************************************
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser
{
    public class RegsRecord
    {
        public string TimeStamp { get; set; }
        public string Environment { get; set; }
        public string Module { get; set; }
        public string Passes { get; set; }
        public string Fails { get; set; }

        public RegsRecord()
        {
            TimeStamp = string.Empty;
            Environment = string.Empty;
            Module = string.Empty;
            Passes = string.Empty;
            Fails = string.Empty;
        } // end default constructor

        public RegsRecord(string time, string env, string mod, string pass, string fail)
        {
            TimeStamp = time;
            Environment = env;
            Module = mod;
            Passes = pass;
            Fails = fail;
        } // end constructor
    } // end class RegsRecord
} // end namespace CSVParser
