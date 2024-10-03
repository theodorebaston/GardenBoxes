using Microsoft.Data.SqlClient;
using System.Numerics;

namespace GardenBoxesGitProject
{
    // Project Instructions
        // Write a program that will...
        // [x]take in a users garden box size 
        // [x]let them pick from a list of plants
        // [x]tell them how many they can plant in that space.
                // per a 16sqft box, able to plant: 9 beets, 16 carrots, 3 corn
                // per square foot, that is: .5625 beets, 1 carrot, .1875 corn
        //Create a database that will hold plants
        // [x]You don't need to add more than 2 or 3 plants into the database for testing.
        // [ ]Make sure the database or a description of the database is included in your repo.

    internal class Program
    {
        static void Main(string[] args)
        {
            //Welcome
            Console.WriteLine("Welcome to The Garden Aid!");
            Console.WriteLine("Pondering what plants to plant in your planting plot? I can propose plans to plant plants!");
            Console.WriteLine();

            //take in a user's garden box size
            Console.WriteLine("I propose we proceed by putting in place the perameters of your planting plot! \n (referred to henceforth as the 'planting area'. I can't keep the alliteration up all day)");
            Console.WriteLine();

            Console.WriteLine("How LONG is your planting area in feet?");
            decimal length = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("How WIDE is your planting area in feet?");
            decimal width = Convert.ToDecimal(Console.ReadLine());

            decimal area = length * width;

            Console.Clear();
            Console.WriteLine($"Awesome!\nYour planting area is {length} feet long and {width} feet wide.\n\nYou'll have {area} square feet in which to plant!\n\n\n Press [Enter] to continue");
            Console.ReadLine();
            Console.Clear();



            //let them pick from a list of plants
            Console.WriteLine("Now let's peruse some plants to plant!");

            //Sql Connection
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\twbas\\PersonalProjects\\GardenBoxes\\GardenBoxes\\Database1.mdf;Integrated Security=True");

            //Loop that allows users to select a plant, see how many will fit in their area, then either select a new plant or exit the program
            string tryotherplants = "maybe";
            while (tryotherplants != "n")
            {
                Console.WriteLine("Which plant would you like to plant?\n");

                //Print Plant Options
                string sqlCmd = "SELECT * from Plants";
                SqlCommand command = new SqlCommand(sqlCmd, connection);

                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"{dataReader["Id"]}) {dataReader["plantName"]}");
                    }
                }
                dataReader.Close();
                connection.Close();
                //Get USER Plant Choice
                int answer = Convert.ToInt32(Console.ReadLine());
                //Declare Variables
                string plantName = "";
                decimal persquarefoot = 0;

                //Reopen connection to collect Users [Plant] name and its [PerSqFt]
                connection.Open();
                //Set Plant Choice Name
                sqlCmd = $"SELECT plantName FROM Plants WHERE Id = {answer}";
                command = new SqlCommand(sqlCmd, connection);
                SqlDataReader plantNameReader = command.ExecuteReader();

                if (plantNameReader.HasRows)
                {
                    while (plantNameReader.Read())
                    {
                        plantName = Convert.ToString(plantNameReader["plantName"]);
                    }

                }
                //Console.WriteLine(plantName);
                plantNameReader.Close();

                //Set Plant Choice PerSqFt
                sqlCmd = $"SELECT plantsPerSqFt FROM Plants WHERE Id = {answer}";
                command = new SqlCommand(sqlCmd, connection);
                SqlDataReader PerSqFtReader = command.ExecuteReader();
                if (PerSqFtReader.HasRows)
                {
                    while (PerSqFtReader.Read())
                    {
                        persquarefoot = Convert.ToDecimal(PerSqFtReader["plantsPerSqFt"]);
                    }
                }
                //Console.WriteLine(persquarefoot);
                PerSqFtReader.Close();

                connection.Close();

                //Calculate Plants per given Area
                decimal perplantingarea = Math.Floor(area * persquarefoot);

                Console.WriteLine($"YUMMMM! If you fill your planting area with {plantName}, you'll be able to plant {perplantingarea} of them!\n\n");

                Console.WriteLine("Would you like to select another plant? y/n");
                tryotherplants = Console.ReadLine().ToLower();
            }
        }
    }
}
