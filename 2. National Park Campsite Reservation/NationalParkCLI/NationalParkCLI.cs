using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Capstone
{
    public class NationalParkCLI
    {
        private NationalPark _nationalPark = null;
        Dictionary<int, Park> _menuSel = new Dictionary<int, Park>();
        Dictionary<int, Campground> _campDic = new Dictionary<int, Campground>();
        int _userParkInput = 0;
        int _userCampChoice = 0;

        public NationalParkCLI(NationalPark park)
        {
            _nationalPark = park;
        }

        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1) View All Parks");
                Console.WriteLine("q) Quit");

                char key = Console.ReadKey().KeyChar;
                if (key == 'q' || key == 'Q')
                {
                    exit = true;
                }
                else if (key == '1')
                {
                    Console.Clear();
                    Console.WriteLine("View Parks Interface");
                    Console.WriteLine("Select a Park for Further Details" );

                    List<Park> listOfParks = _nationalPark.GetParks();
                    int counter = 1;
                    _menuSel.Clear();
                    foreach (var park in listOfParks)
                    {
                        _menuSel.Add(counter, park);
                        Console.WriteLine($"{counter}) {park.Name}");
                        counter++;
                    }

                    try
                    {
                        _userParkInput = int.Parse(Console.ReadLine());
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Please enter your selection in the valid format (e.g., 1, 4, 10).");
                    }

                    Console.Clear();
                    Console.WriteLine("Park Information Screen");
                    
                    try
                    {
                        Console.WriteLine($"{_menuSel[_userParkInput].Name.ToString()} National Park");
                        Console.WriteLine($"Location: {_menuSel[_userParkInput].Location.ToString()}");
                        Console.WriteLine($"Established: {_menuSel[_userParkInput].EstablishDate.ToString()}");
                        Console.WriteLine($"Area: {_menuSel[_userParkInput].Area.ToString()} sq km");
                        Console.WriteLine($"Annual Visitors: {_menuSel[_userParkInput].Visitors.ToString()}");
                        Console.WriteLine();
                        Console.WriteLine($"{_menuSel[_userParkInput].Description.ToString()}");
                        Console.WriteLine();
                        CampgroundMenu(_userParkInput);
                    }
                    catch(KeyNotFoundException)
                    {
                        Console.WriteLine("Invalid Selection. Please try again.");
                        Console.ReadKey();
                    }
                    
                   
                }
                else
                {
                    Console.WriteLine("\nInvalid Selection...\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        public void CampgroundMenu(int userParkInput)
        {
            bool exit = false;
            int _counter = 1;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Select a Command");
                Console.WriteLine("   1) View Campgrounds");
                Console.WriteLine("   2) Search for Reservation");
                Console.WriteLine("   3) Return to Previous Screen");

                char key = Console.ReadKey().KeyChar;
                if (key == '3')
                {
                    _menuSel.Clear();
                    _userParkInput = 0;
                    exit = true;
                }
                else if (key == '1')
                {
                    Console.Clear();
                    List<Campground> campList = _nationalPark.GetCampgroundsByPark(_menuSel[userParkInput]);
                        
                    Console.WriteLine("Park Campgrounds");
                    Console.WriteLine($"{_menuSel[userParkInput].Name} National Park Campgrounds");
                    Console.WriteLine();
                    Console.WriteLine("      Name".PadRight(30, ' ') + "  Open".PadRight(25, ' ') +
                                        " Close".PadRight(25, ' ') + " " +
                                        "Daily Fee".PadRight(23, ' '));
                    foreach (var camp in campList)
                    {
                        Console.WriteLine($"#{_counter}" +
                                            $"    {camp.Name}".PadRight(30, ' ') +
                                            $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(camp.OpenFrom)}".PadRight(25, ' ') +
                                            $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(camp.ClosedFrom)}".PadRight(25, ' ') +
                                            $"{camp.DailyFee.ToString("C")}");
                        _counter++;
                    }
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (key == '2')
                {
                    ReservationMenu(userParkInput);
                }
                else
                {
                    Console.WriteLine("\nInvalid Selection...\nPress any key to continue...");
                    Console.ReadKey();
                }
            }   
        }

        public void ReservationMenu(int userParkInput)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Select a Command");
                Console.WriteLine("   1) Search for Reservation");
                Console.WriteLine("   2) Return to Previous Screen");

                char key = Console.ReadKey().KeyChar;
                if (key == '2')
                {
                    exit = true;
                }
                else if (key == '1')
                {
                    Console.Clear();
                    List<Campground> campList = _nationalPark.GetCampgroundsByPark(_menuSel[userParkInput]);
                    int counter = 1;
                    Console.WriteLine("Search for Campground Reservation");
                    Console.WriteLine("      Name".PadRight(20, ' ') + "  Open".PadRight(21, ' ') + " Close".PadRight(20, ' ') + " " +
                        "Daily Fee".PadRight(23, ' '));
                    foreach (var camp in campList)
                    {
                        _campDic.Add(counter, camp);
                        Console.WriteLine($"#{counter}" +
                                        $"    {camp.Name}".PadRight(20, ' ') +
                                        $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(camp.OpenFrom)}".PadRight(20, ' ') +
                                        $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(camp.ClosedFrom)}".PadRight(20, ' ') +
                                        $"{camp.DailyFee.ToString("C")}");
                        counter++;
                    }

                    Console.Write("Which campground (enter 0 to cancel)? ");

                    try
                    {
                        _userCampChoice = int.Parse(Console.ReadLine().ToString()) - 1;
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Please enter your selection in the valid format (e.g., 1, 4, 10).");
                        Console.ReadKey();
                        campList.Clear();
                        _campDic.Clear();
                    }
                    if (_userCampChoice > counter)
                    {
                        Console.WriteLine("Check your selection number, and try again.");
                    }

                    Console.Write("What is the arrival date? (Enter as YYYY-MM-DD) ");
                    //try/catch
                    DateTime userArrDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("What is the departure date? (Enter as YYYY-MM-DD) ");
                    //try/catch
                    DateTime userDepDate = DateTime.Parse(Console.ReadLine());
                    List<Site> allSitesByCampground = _nationalPark.GetSitesByCampground(campList[_userCampChoice]);

                    Console.WriteLine("Results Matching Your Search Criteria");
                    Console.WriteLine();
                    Console.WriteLine("Site No.".PadRight(20, ' ') + "Max Occup.".PadRight(21, ' ') +
                                        "Accessible?".PadRight(20, ' ') + "RV Len".PadRight(23, ' ') + "Utility".PadRight(20, ' ') +
                                        "Total Cost");
                    List<CustomItem> tempCusItemList = _nationalPark.GetSitesForUser(campList[_userCampChoice].Id, userArrDate, userDepDate);
                    foreach (var i in tempCusItemList)
                    {
                        Console.WriteLine($"{i.Number} ".PadRight(20, ' ') +
                                            $"{i.MaxOccupancy} ".PadRight(21, ' ') +
                                            $"{i.Accessible} ".PadRight(20, ' ') +
                                            $"{i.MaxRvLength} ".PadRight(23, ' ') +
                                            $"{i.Utilities} ".PadRight(20, ' ') +
                                            $"{i.TotalCost.ToString("C")}");
                    }
                    Console.WriteLine();
                    Console.Write("Which site should be reserved (enter 0 to cancel)? ");

                    int userSiteChoice = int.Parse(Console.ReadLine());
                    Console.Write("What name should the reservation be made under? ");
                    string userResName = Console.ReadLine();
                    Console.WriteLine();
                    int reservationId = _nationalPark.MakeReservation(userSiteChoice, userResName, userArrDate, userDepDate);

                    Console.WriteLine($"The reservation has been made and the confirmation id is {reservationId}");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("\nInvalid Selection...\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

        }

    }
}
