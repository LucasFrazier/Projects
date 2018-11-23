using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CapStone;

namespace CapStone
{
    //vendingmachine.csv
    public class VendingMachine
    {
        //constants for Qtr, Nick, Dime
        
        public List<MachineItem> _purchaseList = new List<MachineItem>();
        //Access machine items with slot names ex "A1, B2, C3"
        public Dictionary<string, MachineItem> Inventory = new Dictionary<string, MachineItem>();
        public decimal CurrentBalance { get; private set; } = 0;
        public Report Report { get; }
        /// <summary>
        /// 
        /// </summary>
        public VendingMachine()
        {

            string filePath = Environment.CurrentDirectory;
            string fullPath = Path.Combine(filePath, "vendingmachine.csv");
            

            using (StreamReader sr = new StreamReader(fullPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lineArray = line.Split('|');
                    if (lineArray[3] == "Chip")
                    {
                        Inventory[lineArray[0]] = new Chip(lineArray[1], decimal.Parse(lineArray[2]));
                    }
                    if (lineArray[3] == "Candy")
                    {
                        Inventory[lineArray[0]] = new Candy(lineArray[1], decimal.Parse(lineArray[2]));
                    }
                    if (lineArray[3] == "Drink")
                    {
                        Inventory[lineArray[0]] = new Drink(lineArray[1], decimal.Parse(lineArray[2]));
                    }
                    if (lineArray[3] == "Gum")
                    {
                        Inventory[lineArray[0]] = new Gum(lineArray[1], decimal.Parse(lineArray[2]));
                    }
                }
            }

            Report = new Report(Inventory);

        }
        //dictionary that is type <string: machineItem>
        //when manipulating vending machine dictionary: dictvm[slot].qty-=1;
        //<{{A1: chip(name = "PotatoeCrisps")},
        //Have constructor
        //Constructor takes in vendingmachin.csv
        //Creates items as it reads down the file
        //string[] line = sr.Readline.Split('|');
        //4 x if (line[3] == category);
        //vmdict[slot] = new Chip(name, price);
        //var temp = new Chip();
        public void FeedMoney(decimal money)
        {
            CurrentBalance += money;
        }
        /// <summary>
        /// Sells item assigned to <paramref name="key"/> and subtracts it from CurrentBalance.
        /// </summary>
        /// <param name="key">Key in Inventory represented by slot code</param>
        public void ProductSelect(string key)
        {
            MachineItem mi = Inventory[key];
            if (!mi.SoldOut)
            {
                if (mi.Price <= CurrentBalance)
                {
                    mi.Qty -= 1;
                    CurrentBalance -= mi.Price;
                    _purchaseList.Add(mi);
                    Report.ReportDict[mi.Name]++;
                    Report.AddToTotal(mi.Price);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                throw new Exception();
            }
        }



        public Change ReturnChange()
        {
            Change change = new Change(CurrentBalance);
            CurrentBalance = 0;
            return change;
        }
    }
}
