using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoviePlex
{
    public class Movie
    {
        public string movieName { get; set; }
        public string movieRating { get; set; }
    }
    class Program
    {
        public int flag = 0;
        public uint NumberOfMovies ;
        public List<string> movieNameList = new List<string>();
        public List<string> ageList = new List<string>();
        public static int selection;
        public static int count = 4;
        public static string password = "a";
        public string[] ageRating = new string[] { "G", "PG", "PG-13", "R", "NC-17" };
        public string entered_password;
        public bool ageValid;

        public static void Start()
        { 
            Console.WriteLine("\t\t\t\t************************************");
            Console.WriteLine("\t\t\t\t*** Welcome to MoviePlex Theater ***");
            Console.WriteLine("\t\t\t\t************************************");
            Console.WriteLine("Please Select from the following options:");
            Console.WriteLine("1: Administrator");
            Console.WriteLine("2: Guests");
            Console.Write("Selecion: ");
            try { 
              selection = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("You have made an invalid selection. You will be redirected to start menu within few seconds!");
                int milliseconds = 4000;
                Thread.Sleep(milliseconds);
                Console.Clear();
                Start();
            }
        }

        public void MainMenu()
        {
           
            string enteredValue = Console.ReadLine();
            if (enteredValue == "B" || enteredValue == "b")
            {
                Console.Clear();
                Main(null);

            }
            else
            {
                Console.Write("\nInvalid Input!\nPlease enter 'B' for Start menu: ");
                MainMenu();
            }
        }
        public void MovieList()
        {            
            int NoOfMovies=10;
            Console.Write("\nHow Many Movies are playing today? : ");
            try
            {
                NumberOfMovies = Convert.ToUInt32(Console.ReadLine()); 
            
            if (NumberOfMovies > 0 && NumberOfMovies <= NoOfMovies)
            {
                
                    try
                {
                    string[] orderArray = new string[] { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Nineth", "Tenth" };  
                    
                    for (int i = 0; i < NumberOfMovies; i++)
                    {
                        found:
                            Console.Write("\nPlease enter "+orderArray[i]+" movie's name : ");
                              string movieName = Console.ReadLine();
                            if (movieName == "")
                            {
                                Console.WriteLine("Please enter valid movie name");
                                goto found;

                            }
                            else
                            {
                                movieNameList.Add(movieName);
                            }
                 
                              enterAgeRating:
                              Console.Write("Please enter the age limit or Rating For the " + orderArray[i] + " movie : ");
                              string age = Console.ReadLine();
                            for(int j = 0; j < ageRating.Length; j++)
                            {
                                if (ageRating[j] == age)
                                {
                                    ageList.Add(age);
                                    break;
                                }
                                else 
                                if(Int32.TryParse(age, out int ageValue))
                                {
                                    if(ageValue>0 && ageValue <= 120)
                                    {
                                        ageList.Add(age);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Age must be between 0 and 120.");
                                        goto enterAgeRating;
                                    }
                                }
                                else if(j==ageRating.Length-1)
                                {
                                    Console.WriteLine("You have entered invalid Age Rating.");
                                    goto enterAgeRating;
                                }
                            }
                                
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
                        for (int i = 0; i < ageList.Count; i++)
                        {
                            Console.WriteLine(i + 1 + ". " + movieNameList[i] + "  {" + ageList[i] + "}");
                            
                        }
                    
                    Satisfy();
            }
                else
                {
                    Console.WriteLine("Please enter value between 1 and 10.");
                    MovieList();

                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid input");
                MovieList();
            }
        }
        public void Satisfy()
        {
            Console.Write("\nYour Movies playing today are listed above. Are you satisfied? (Y/N)? ");
            string satisfy = Console.ReadLine();
            if (satisfy == "Y" || satisfy == "y")
            {
                Console.Clear();
                Start();
            }

            else if (satisfy == "N" || satisfy == "n")
            {
                Console.Clear();
                Console.WriteLine("Welcome MoviePlex Administrator");
                movieNameList.Clear();
                ageList.Clear();
                MovieList();
            }
            else
            {
                Console.WriteLine("Please enter a valid input.");
                Satisfy();
            }
        }
        public void MakeSelection()
        {
            lable:
            if (Program.selection == 1)
            {
                Console.Write("Please enter Admin password: ");
                entered_password = Console.ReadLine();

                while (count >= 0)
                {
                    
                    if (entered_password == password)
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome MoviePlex Administrator");
                        MovieList();
                        flag = 1;
                        break;
                    }
                   
                    else
                    {
                        Console.Clear();
                        if (count == 0)
                        {
                            Console.WriteLine("You have reached maximum attempts! Press B to Go back to Main Menu");
                            count = 4;
                            MainMenu();
                        }
                        else
                        {
                            
                            Console.WriteLine("You entered an Invalid password. \nYou have " + count + " more attempts to enter the password  or " +
                                "Press B to go back to the previous screen");

                            Console.Write("Please enter Admin password: ");
                            entered_password = Console.ReadLine();
                            if (entered_password == password)
                            {
                                Console.Clear();
                                Console.WriteLine("Welcome MoviePlex Administrator");
                                MovieList();
                                flag = 1;
                                break;
                            }
                            
                            else if (entered_password == "B" || entered_password == "b")
                            {
                                count = 4;
                                
                                Console.Clear();
                                Start();
                                goto lable;
                            }

                        }
                    }
                    count--;
                }

            }
            else if (Program.selection == 2 && flag == 1)
            {
                UserAccess();
            }
            else if(Program.selection == 2 && flag == 0)
            {
                Console.WriteLine("Sorry, no movies are available today.");
                Program.selection = 0;
                Console.Write("Press S to go to Start menu : ");
                string startSelection = Console.ReadLine();
                if (startSelection == "S" || startSelection=="s")
                {
                    Console.Clear();
                    Start();
                }
                else
                {
                    Console.Write("\nYou have entered a wrong input. You will be redirected to start menu shortly!");
                    int milliseconds = 4000;
                    Thread.Sleep(milliseconds);

                    Console.Clear();
                    Start();

                }   

            }
            else
            { 
                Console.WriteLine("Please enter a valid input.");
            }  
        }
        public void UserAccess()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t************************************");
            Console.WriteLine("\t\t\t\t*** Welcome to MoviePlex Theater ***");
            Console.WriteLine("\t\t\t\t************************************");
            Console.WriteLine("\nThere are " + movieNameList.Count() + " movies playing today. Please choose from the following movies : \n");
          
                for (int i = 0; i < ageList.Count; i++)
                {

                    Console.WriteLine("\t" + (i + 1) + ". " +movieNameList[i]+ "  {" + ageList[i] + "}");
                  
                }
            

            validateMovieNumber();          
        }
        public void validateMovieNumber()
        {
            try
            {
                Console.Write("\nWhich movie would you like to watch : ");
                int movieNumber = Convert.ToInt32(Console.ReadLine());
                if (movieNumber > 0 && movieNumber <= movieNameList.Count)
                {
                    EnterAge(movieNumber);
                }
                else
                {
                    Console.WriteLine("Please enter valid movie number.");
                    validateMovieNumber();
                }
               
            }
            catch
            {
                Console.WriteLine("Please enter valid movie number.");
                validateMovieNumber();
           
            }
        }
        public void EnterAge(int movieNumber)
        {
            int enteredAge = AgeValidation();
            if (enteredAge >= 1)
            {
                ageValid = AgeCategory(movieNumber, enteredAge);
                if (ageValid)
                    Console.WriteLine("\nEnjoy the Movie!\n");
                else
                {
                   
                    Console.WriteLine("\nYou are not allowed to watch this movie.\n");
                }
                Program.selection = 0;
                GuestSelection();
            }
            else
                EnterAge(movieNumber);
        }
        public int AgeValidation()
        {
            try
            {
                Console.Write("\nPlease enter age for verification : ");
                string enteredAge = Console.ReadLine();
                int age;
                bool success = Int32.TryParse(enteredAge, out age);
                if (success)
                {
                    if (age < 0 || age>120)
                    {
                        throw new Exception("Age Cannot be "+age+" Please enter age between 1 and 120.");

                    }
                    else if(age==0){
                        Console.WriteLine("Age can't be zero");
                    }
                    else
                    {
                        return age;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Age!");
                    return AgeValidation();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                return AgeValidation();
            }
            return 0;
        }
        public bool AgeCategory(int selectedNumber, int userAge)
        {
            string recommendedAge = ageList[selectedNumber-1];
            if (recommendedAge == "R")
            {
                if (userAge >= 15)
                {
                    if (userAge > 120)
                    {
                        Console.WriteLine("Age Must not exceed 120");
                        GuestSelection();
                    }
                    else
                        return true;
                }
            }
            else if (recommendedAge == "PG")
            {
                if (userAge >= 10)
                {
                    if (userAge > 120)
                    {
                        Console.WriteLine("Age Must not exceed 120");
                        GuestSelection();
                    }
                    else
                        return true;
                }

            }
            else if (recommendedAge == "PG-13")
            {
                if (userAge >= 13)
                {
                    if (userAge > 120)
                    {
                        Console.WriteLine("Age Must not exceed 120");
                        GuestSelection();
                    }
                    else
                        return true;
                }

            }
            else if (recommendedAge == "NC-17")
            {
                if (userAge >= 17)
                {
                    if (userAge > 120)
                    {
                        Console.WriteLine("Age Must not exceed 120");
                        GuestSelection();
                    }
                    else
                        return true;
                }

            }
            else if (recommendedAge == "G")
            {
                if (userAge > 120 || userAge<1)
                {
                    Console.WriteLine("Please enter a valid age");
                    GuestSelection();
                }
                else
                    return true;
            }

            else if ( userAge >= Convert.ToInt32(recommendedAge) && userAge <=120)
            {   

                return true;
             
            }
             
            return false;
           
        }
        public void GuestSelection()
        {
            Console.WriteLine("Press M for guest main menu.");
            Console.WriteLine("Press S to go back to Start Page.");
            string guestMenu = Console.ReadLine();
            if (guestMenu == "M" || guestMenu=="m")
            {   

                UserAccess();
            }
            else if (guestMenu == "S" || guestMenu=="s")
            {
                Console.Clear();
                Program.Start();
            }
            else
            {
                Console.WriteLine("Please enter valid input");
                GuestSelection();
            }
        }
       
        public static void Main(string[] args)
        {
            Program program = new Program();
            Program.Start();
            
            while(true)
            {
                try
                {
                    
                    if (Program.selection == 1 || Program.selection == 2)
                    {
                        program.MakeSelection();
                    }
                    else
                    {
                        Console.WriteLine("You have made an invalid selection. You will be redirected to start menu within few seconds!");
                        int milliseconds = 4000;
                        Thread.Sleep(milliseconds);
                        Console.Clear();
                        Start();
                    }
                        
                }
                catch
                {
                    Console.WriteLine("You have made an invalid selection. You will be redirected to start menu within few seconds!");
                    int milliseconds = 4000;
                    Thread.Sleep(milliseconds);
                    Console.Clear();
                    Start();
                }
                   
            }
           
        }
    }
}
