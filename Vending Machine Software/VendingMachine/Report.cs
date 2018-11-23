using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CapStone
{
    /// <summary>
    /// 
    /// </summary>
    public class Report
    {
        #region Properties
        /// <summary>
        /// Tracks items sold in current session.
        /// </summary>
        public Dictionary<string, int> ReportDict { get; private set; } = new Dictionary<string, int>();

        /// <summary>
        /// Running total of sales since the beginning of time.
        /// </summary>
        private decimal SalesTotal { get; set; }
        #endregion

        #region Constructor
        public Report(Dictionary<string, MachineItem> vmInventory)
        {
            foreach(var item in vmInventory)
            {
                ReportDict.Add(item.Value.Name, 0);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds decimal <paramref name="newSale"/> to Report SalesTotal.
        /// </summary>
        /// <param name="newSale">Decimal representing cost of the item sold.</param>
        public void AddToTotal(decimal newSale)
        {
            SalesTotal += newSale;
        }
        
        /// <summary>
        /// Reads in Sales Report historical file, and combines current session data.
        /// </summary>
        public void CombineReport()
        {
            #region ReadingFileIn]

            if (File.Exists(@"..\..\..\etc\SalesReport.txt"))
            {
                using (StreamReader sr = new StreamReader(@"..\..\..\etc\SalesReport.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (line.Contains("**TOTAL SALES**"))
                        {
                            string[] lineArray = line.Split('$');
                            string money = lineArray[1];
                            money = money.Replace(",", "");
                            SalesTotal += decimal.Parse(money);
                        }
                        else if (line != "")
                        {
                            string[] lineArray = line.Split('|');
                            if (ReportDict.ContainsKey(lineArray[0]))
                            {
                                ReportDict[lineArray[0]] += int.Parse(lineArray[1]);
                            }
                            else
                            {
                                ReportDict.Add(lineArray[0], int.Parse(lineArray[1]));
                            }

                        }
                    }
                }
            }
            
            
            #endregion
            #region OverwriteFile
            using (FileStream fs = File.Open(@"..\..\..\etc\SalesReport.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var item in ReportDict)
                    {
                        sw.WriteLine($"{item.Key}|{item.Value}");
                    }
                    sw.WriteLine();
                    sw.Write($"**TOTAL SALES** {SalesTotal.ToString("C")}");
                }
            }
            
            #endregion
        }
        #endregion


    }
}
