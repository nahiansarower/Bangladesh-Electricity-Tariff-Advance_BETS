using System;
using System.Linq;
namespace ETS
{
    public class Breakdown
    {
        private double[] tarrifRate = { 4.63, 5.26, 7.20, 7.59, 8.02, 12.67, 14.61 };

        private double[] Consumer = { 50.0, 75.0, 200.0, 300.0, 400.0, 500.0, 600.0 };

        public Breakdown()
        {
            bool exit = true;
            do
            {
                TarrifRate();
                UserInput();

                Console.WriteLine("To Continue Press any key Except || Exit Press E");
                if (Console.ReadLine().ToUpper() == "E")
                {
                    exit = false;
                }
            }
            while (exit);

        }

        public void TarrifRate()
        {
            Console.WriteLine("Bangladesh Electricity Tariff ");
            Console.WriteLine("================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("             Bangladesh Electricity Tariff Rate");
            Console.WriteLine("           ------------------------------------------");
            Console.WriteLine("Consumer Section              || Energy rate/charge(Taka/kWh)  ");
            Console.WriteLine("=================================||=================================");
            Console.WriteLine("Life Line : 0-50 Unit          ||  4.63");
            Console.WriteLine("---------------------------------||---------------------------------");
            Console.WriteLine("First Step : 51-75 Unit        ||  5.26");
            Console.WriteLine("---------------------------------||---------------------------------");
            Console.WriteLine("Second Step : 76-200 Unit      ||  7.20");
            Console.WriteLine("---------------------------------||---------------------------------");
            Console.WriteLine("Third Step : 201-300 Unit      ||  7.59");
            Console.WriteLine("---------------------------------||---------------------------------");
            Console.WriteLine("Fourth Step : 301-400 Unit     ||  8.02");
            Console.WriteLine("---------------------------------||---------------------------------");
            Console.WriteLine("Fifth Step : 401-600 Unit      ||  12.67");
            Console.WriteLine("---------------------------------||---------------------------------");
            Console.WriteLine("Sixth Step : 601 to Upper Unit ||  14.61");
            Console.WriteLine("=================================||=================================");
            Console.ResetColor();
        }

        public void UserInput()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please Enter Your Unit : ");

            if (!double.TryParse(Console.ReadLine(), out double tarrifInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Please enter a number.");
                return;
            }

            if (tarrifInput < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input. Please enter a positive number.");
                return;
            }

            double[] newArray;
            double nextSlab = Consumer.FirstOrDefault(x => x > tarrifInput);
            double[] AreaConsume = Consumer.TakeWhile(x => x <= tarrifInput).ToArray();

            if (nextSlab > 0)
            {
                newArray = AreaConsume.Concat(new[] { nextSlab }).ToArray();
            }
            else
            {
                newArray = AreaConsume;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nGenerated Tariff Slabs:");

            for (int i = 0; i < newArray.Length - 1; i++)
            {
                Console.WriteLine($"Step {i + 1}: {newArray[i]} TO {newArray[i + 1]}");
                Console.WriteLine("-----------------");
            }

            CalculateBill(tarrifInput, newArray);
        }

        private void CalculateBill(double totalUnits, double[] newArray)
        {
            double totalCost = 0;
            double remainingUnits = totalUnits;
            double previousSlabUnit = 0;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nStep-by-Step Calculation:");
            Console.WriteLine("----------------------------------------------------");

            for (int i = 0; i < Consumer.Length; i++)
            {
                double unitsInSlab = Consumer[i] - previousSlabUnit;
                double unitsToBill = 0;

                if (remainingUnits > unitsInSlab)
                {
                    unitsToBill = unitsInSlab;
                    double slabCost = unitsToBill * tarrifRate[i];
                    totalCost += slabCost;
                    remainingUnits -= unitsInSlab;
                    Console.WriteLine($"STEP {i + 1} : ({previousSlabUnit} - {Consumer[i]}): {unitsToBill} units * {tarrifRate[i]:F2} Taka/unit = {slabCost:F2} Taka");
                }
                else
                {
                    unitsToBill = remainingUnits;
                    double slabCost = unitsToBill * tarrifRate[i];
                    totalCost += slabCost;
                    remainingUnits = 0;
                    Console.WriteLine($"STEP {i + 1} : ({previousSlabUnit} - {Consumer[i]}): {unitsToBill} units * {tarrifRate[i]:F2} Taka/unit = {slabCost:F2} Taka");
                    break; 
                }

                previousSlabUnit = Consumer[i];
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----------------------------------------------------");
            Console.WriteLine($"For {totalUnits} Unit || Total Amount is : {totalCost:F2} Taka");
            Console.WriteLine("----------------------------------------------------");
            Console.ResetColor();
        }
    }


}
    





