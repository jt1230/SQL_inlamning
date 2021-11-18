using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;


    namespace SQLövning
    {           
        internal class Start
        {
            public void Menu()
            {
                var db = new Databas();
                db.DatabaseName = "People"; //namnet på databasen där MOCK_DATA finns
                var allParameters = new ParamData[0];

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("================= WELCOME TO OUR DATABASE CONTAINING 1000 USERS =================\n");
                    Console.WriteLine("Press any of the following numbers to check out some information we have gathered:");
                    Console.WriteLine("(1) How many countries are represented?");
                    Console.WriteLine("(2) Are all usernames and passwords unique?");
                    Console.WriteLine("(3) How many are from the Nordic Countries and from Scandinavian?");
                    Console.WriteLine("(4) Which country is the most common among our users?:");
                    Console.WriteLine("(5) Show the 10 first users with a lastname starting with the letter L");
                    Console.WriteLine("(6) Show all the users where first and last name starts with the same letter");
                    Console.WriteLine("(7) Exit database");
                    Console.WriteLine("\n=================================================================================");
                    int input = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    switch (input)
                    {
                        case 1:
                            //Hur många olika länder finns representerade?
                            Console.WriteLine("================= WELCOME TO THE DATABASE CONTAINING 1000 USERS =================\n");

                            var dtCountry = db.GetDataTable("Select Count(Distinct country) from MOCK_DATA", allParameters);
                            foreach (DataRow item in dtCountry.Rows)
                            {
                                Console.WriteLine($"There are a total of {item[0]} unique countries.");
                            }
                            Console.WriteLine("\nPress [ENTER] to go back to the menu...");
                            Console.WriteLine("\n=================================================================================");
                            Console.ReadLine();
                            break;

                        case 2:
                            //Är alla username och password unika?
                            Console.WriteLine("================= WELCOME TO THE DATABASE CONTAINING 1000 USERS =================\n");

                            var dtUsername = db.GetDataTable("Select Count(Distinct username) from MOCK_DATA", allParameters);
                            foreach (DataRow item in dtUsername.Rows)
                            {
                                var dtPassword = db.GetDataTable("Select Count(Distinct password) from MOCK_DATA", allParameters);
                                foreach (DataRow Ditem in dtPassword.Rows)
                                {
                                    var dtUsers = db.GetDataTable("Select Count(id) from MOCK_DATA", allParameters); //få ut antalet id
                                    foreach (DataRow UItem in dtUsers.Rows)
                                    {
                                        Console.WriteLine("UITEM = " + dtUsers);
                                        if (item[0] == UItem[0] && Ditem[0] == UItem[0]) //jämför ifall antalet unika username och password är lika med antalet id
                                        Console.WriteLine("True.");
                                        else
                                        Console.WriteLine("They are not unique. There are a total of " + item[0] + " unique usernames in this database and " + Ditem[0] + " unique passwords in this database!");
                                    }
                                }
                            }
                            Console.WriteLine("\nPress [ENTER] to go back to the menu...");
                            Console.WriteLine("\n=================================================================================");
                            Console.ReadLine();
                            break;

                        case 3:
                            //Hur många är från Norden respektive Skandinavien?
                            Console.WriteLine("================= WELCOME TO THE DATABASE CONTAINING 1000 USERS =================\n");

                            var dtNordic = db.GetDataTable("SELECT COUNT(country) FROM MOCK_DATA WHERE country IN ('Sweden', 'Denmark', 'Norway', 'Finland', 'Iceland', 'Greenland', 'The Faraoe Islands', 'Åland')", allParameters);
                            foreach (DataRow item in dtNordic.Rows)
                            {
                                var dtSkandi = db.GetDataTable("SELECT COUNT(country) FROM MOCK_DATA WHERE country IN ('Sweden', 'Denmark', 'Norway')", allParameters);
                                foreach (DataRow Ditem in dtSkandi.Rows)
                                {
                                    Console.WriteLine($"There are a total of {item[0]} people from the Nordic countries and \n{Ditem[0]} people from Scandinavia.");
                                }
                            }
                            Console.WriteLine("\nPress [ENTER] to go back to the menu...");
                            Console.WriteLine("\n=================================================================================");
                            Console.ReadLine();
                            break;

                        case 4:
                            //Vilket är det vanligaste landet?
                            Console.WriteLine("================= WELCOME TO THE DATABASE CONTAINING 1000 USERS =================\n");

                            var dtCommonCountry = db.GetDataTable("SELECT TOP 1 COUNT(id) AS new, country FROM MOCK_DATA group by country order by new desc", allParameters);
                            foreach (DataRow item in dtCommonCountry.Rows)
                            {
                                Console.WriteLine("The country where most of our users live in is " + item[1] + ", \nwith " + item[0] + " of our users.");
                            }
                            Console.WriteLine("\nPress [ENTER] to go back to the menu...");
                            Console.WriteLine("\n=================================================================================");
                            Console.ReadLine();
                            break;

                        case 5:
                            //Lista de 10 första användarna vars efternamn börjar på bokstaven L
                            Console.WriteLine("================= WELCOME TO THE DATABASE CONTAINING 1000 USERS =================\n");
                            Console.WriteLine("The first 10 people in this database with a lastname, which starts with L are: ");

                            var dtLastNameL = db.GetDataTable("SELECT top 10 last_name FROM MOCK_DATA WHERE last_name LIKE 'L%'", allParameters);
                            foreach (DataRow item in dtLastNameL.Rows)
                            {
                                Console.WriteLine(item[0]);
                            }
                            Console.WriteLine("\nPress [ENTER] to go back to the menu...");
                            Console.WriteLine("\n=================================================================================");
                            Console.ReadLine();
                            break;

                        case 6:
                            //Visa alla användare vars för - och efternamn har samma begynnelsebokstav(ex Peter Parker, Bruce Banner, Janis Joplin)
                            Console.WriteLine("================= WELCOME TO THE DATABASE CONTAINING 1000 USERS =================\n");
                            Console.WriteLine("Show all users with first and last name, which starts with the same letter:");

                            var dtfirstlastname = db.GetDataTable("SELECT first_name, last_name FROM MOCK_DATA WHERE UPPER(LEFT(first_name, 1)) = UPPER(LEFT(last_name, 1))", allParameters);
                            foreach (DataRow item in dtfirstlastname.Rows)
                            {
                                Console.WriteLine(item[0] + " " + item[1]);
                            }
                            Console.WriteLine("\nPress [ENTER] to go back to the menu...");
                            Console.WriteLine("\n=================================================================================");
                            Console.ReadLine();
                            break;

                        case 7:
                            Console.WriteLine("Thank you for taking a look at our database!");
                            Console.WriteLine("Press [ENTER] to exit ...");
                            Console.ReadLine();
                            Environment.Exit(0);
                            break;
                    }
                }

                Console.ReadLine();
            }
        }
    }
