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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser
{
    class Program
    {
        public static void Main(string[] args)
        {
            string signature = "	  ____.           .____             _____  _______ \n";
            signature += "	 |    |           |    |    ____   /  |  | \\   _  \\ \n";
            signature += "	 |    |   ______  |    |   /  _ \\ /   |  |_/  /_\\  \\ \n";
            signature += "     /\\__|    |  /_____/  |    |__(  <_> )    ^   /\\  \\_/   \\ \n";
            signature += "     \\________|           |_______ \\____/\\____   |  \\_____  / \n";
            signature += "                                  \\/          |__|        \\/ \n";
            signature += " *************************************************************** \n";
            Console.WriteLine(signature);

            //Create & run reports
            IReport regs = new RegsReport(args[0], args[1]);
            regs.RunReport();


            //Hold Console
            Console.Write("press any key to exit... ");
            Console.ReadKey(true);
            Environment.Exit(0);
        } // end method Main()
    } // end class Program
} // end namespace CSVParser
