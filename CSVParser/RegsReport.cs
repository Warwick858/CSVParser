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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace CSVParser
{
    public class RegsReport : IReport
    {
        public string PWD { get; set; }
        public string Destination { get; set; }
        public string[] Files { get; set; }
        public string[] Directories { get; set; }
        public RegsRecord CurrentRecord { get; set; }
        public List<RegsRecord> Records { get; set; }

        public RegsReport(string source, string dst)
        {
            PWD = source;
            Destination = dst;
            CurrentRecord = null;
            Records = new List<RegsRecord>();
        } // end constructor

        public void RunReport()
        {
            //Validate Directory
            if (!DirectoryIsValid(PWD))
            {
                //Print error
                Console.WriteLine("The given directory does not exist.");
                return;
            }

            //Find file paths
            SearchDirectory(PWD);

            //Read & write data
            foreach (var file in Files)
            {
                ReadData(file);
                ParseFileName(file);
                Records.Add(CurrentRecord);
            }

            WriteData();
        } // end method RunReport()

        public bool DirectoryIsValid(string folder)
        {
            //If the directory does not exist
            if (folder.Equals(string.Empty) || !Directory.Exists(folder))
            {
                return false;
            }

            return true;
        } // end method DirectoryIsValid()

        public void SearchDirectory(string folder)
        {
            //Get files in the directory
            Files = Directory.GetFiles(folder);

            //Get subdirectories in the directory
            Directories = Directory.GetDirectories(folder);

            //Find file extensions in the directory
            var extensions = (from file in Files
                              select Path.GetExtension(file)).Distinct();

            //Recursively search subdirectories
            foreach (var sub in Directories)
            {
                SearchDirectory(sub);
            }
        } // end method SearchDirectory()

        public void ReadData(string file)
        {
            //Create Stream & CsvReader
            TextReader reader = new StreamReader(file);
            var csv = new CsvReader(reader);

            CurrentRecord = new RegsRecord();

            //Save passes
            csv.Read(); // read row 0
            CurrentRecord.Passes = csv[1]; // get col 1

            //Save fails
            csv.Read(); // read row 1
            CurrentRecord.Fails = csv[1]; // get col 1
        } // end method ReadData()

        public void ParseFileName(string file)
        {
            //Get file name and split it into tokens
            string fileName = Path.GetFileNameWithoutExtension(file);
            string[] tokens = fileName.Split();

            //Save the environment, timestamp, and module
            CurrentRecord.Environment = tokens[0];
            CurrentRecord.TimeStamp = GetTimeStamp(tokens);
            CurrentRecord.Module = String.Join("", tokens.Where((s, i) => i > 0 && i < tokens.Length - 3).Select(s => s += " ").ToArray());
        } // end method ParseFileName()

        public string GetTimeStamp(string[] tokens)
        {
            //Save various parts of timestamp from filename tokens
            string meridiem = tokens[tokens.Length - 1];
            string time = tokens[tokens.Length - 2];
            string date = tokens[tokens.Length - 3];
            string[] df = date.Split('-'); // date fragments

            //Seperate hours and minutes from time
            StringBuilder seconds = new StringBuilder();
            seconds.Append(time[time.Length - 2]);
            seconds.Append(time[time.Length - 1]);
            time = time.Remove(time.Length - 2);

            return $"{Int32.Parse(df[0])}/{Int32.Parse(df[1])}/{Int32.Parse("20" + df[2])} {Int32.Parse(time)}:" +
                $"{Int32.Parse(seconds.ToString())} {meridiem}";
        } // end method GetTimeStamp()

        public void WriteData()
        {
            //Create Stream & CsvWriter
            TextWriter writer = new StreamWriter(Destination);
            var csv = new CsvWriter(writer);

            //Write to the csv file
            csv.WriteRecords(Records);
            writer.Close();
        } // end method WriteData()
    } // end class RegsReport
} // end namespace CSVParser
