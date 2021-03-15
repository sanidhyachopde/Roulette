using System;
using System.Text;

namespace Roulette
{
    /// <summary>
    /// Class to simulate the Roulette game
    /// </summary>
    class Roulette
    {
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int bankroll = 0;
            int bet = 0;

            Console.WriteLine("Enter inital amount: ");
            bankroll = int.Parse(Console.ReadLine()); //Initial amount is taken as input from the users.

            while (bankroll != 0)
            {
                Console.WriteLine("\r\nEnter the bet amount");
                do
                {                    
                    bet = int.Parse(Console.ReadLine()); //Users are asked to input the amount they want to bet.
                    if (bet <= 0 || bet > bankroll)
                    {
                        Console.WriteLine("\r\nEnter a valid bet amount");
                    }
                } while (bet <= 0 || bet > bankroll);

                bankroll -= bet;

                StringBuilder choice = DisplayMenu(); //Betting menu is displayed and users bet is received.

                bankroll = PlaceBet(choice, bankroll, bet); //Bet is placed and bankroll is updated with winnings or loss.

                Console.WriteLine("\r\nUpdated bankroll is: $" + bankroll);
                if (bankroll != 0)
                {
                    Console.WriteLine("\r\nDo you want to continue playing? (Y/N)");
                    if (Console.ReadLine().Equals("N"))
                    {
                        Console.WriteLine("\r\n*******************");
                        Console.WriteLine("\r\n     Game Over"); //This message is displayed if the user decides to exit the game.
                        Console.WriteLine("\r\n*******************");
                        Environment.Exit(0);
                    }
                }
            }

            if (bankroll == 0)
            {
                Console.WriteLine("\r\n*******************");
                Console.WriteLine("\r\n You Are Bankrupt"); //The game is exited if the users bankroll reaches $0.
                Console.WriteLine("\r\n*******************");
            }
        }

        /// <summary>
        /// This method displays the bet menu
        /// </summary>
        /// <returns></returns>
        private static StringBuilder DisplayMenu()
        {
            Console.WriteLine("\r\nSelect a bet:"); //Users are asked to select a bet from the following menu.
            Console.WriteLine("1) Red/Black");
            Console.WriteLine("2) Odd/Even");
            Console.WriteLine("3) Single number");
            Console.WriteLine("4) 1st 12, 2nd 12, 3rd 12: (1-12)/(13-24)/(25-36)");
            Console.WriteLine("5) Low (1-18) / High (19-36)");
            Console.WriteLine("6) Exit Game");
            Console.Write("\r\nSelect an option: ");

            StringBuilder sb = new StringBuilder();
            sb.Append(Console.ReadLine());
            return sb;
        }

        /// <summary>
        /// This method places bet that is selected by the user.
        /// </summary>
        /// <param name="choice">Users bet choice</param>
        /// <param name="bankroll">Users total bankroll</param>
        /// <param name="bet">Bet amount placed by the user</param>
        /// <returns></returns>
        private static int PlaceBet(StringBuilder choice, int bankroll, int bet)
        {
            int enteredNumber = 0;

            switch (choice.ToString()) //Switch statements are used to switch between the selected menu options.
            {
                case "1":
                    Console.WriteLine("\r\nType Red or Black: ");
                    choice.Clear();
                    choice.Append(Console.ReadLine());
                    bankroll += SpinRoulette("1", choice, bet); //The bankroll is updated with the amount won/lost in the Red/Black bet.
                    break;
                case "2":
                    Console.WriteLine("\r\nType Odd or Even: ");
                    choice.Clear();
                    choice.Append(Console.ReadLine());
                    bankroll += SpinRoulette("2", choice, bet); //The bankroll is updated with the amount won/lost in the Odd/Even bet.
                    break;
                case "3":
                    Console.WriteLine("\r\nEnter a number: ");
                    do
                    {
                        enteredNumber = int.Parse(Console.ReadLine());
                        if (enteredNumber < 0 || enteredNumber > 36)
                        {
                            Console.WriteLine("\r\nEnter a number between 00 and 36: ");
                        }
                    } while (enteredNumber < 0 || enteredNumber > 36);

                    choice.Clear();
                    choice.Append(enteredNumber);
                    bankroll += SpinRoulette("3", choice, bet); //The bankroll is updated with the amount won/lost in the Single Number bet.
                    break;
                case "4":
                    Console.WriteLine("\r\nType First or Second or Third: ");
                    choice.Clear();
                    choice.Append(Console.ReadLine());
                    bankroll += SpinRoulette("4", choice, bet); //The bankroll is updated with the amount won/lost in the First 12/Second 12/Third 12 bet.
                    break;
                case "5":
                    Console.WriteLine("\r\nType Low or High: ");
                    choice.Clear();
                    choice.Append(Console.ReadLine());
                    bankroll += SpinRoulette("5", choice, bet); //The bankroll is updated with the amount won/lost in the Low/High bet.
                    break;
                case "6":
                    Console.WriteLine("\r\n*******************");
                    Console.WriteLine("\r\n     Game Over"); //This message is displayed if the user decides to exit the game.
                    Console.WriteLine("\r\n*******************");
                    Environment.Exit(0);
                    break;
            }
            return bankroll;
        }

        /// <summary>
        /// This method simulates the spinning of the roulette to give a result to users on the bet they placed.
        /// </summary>
        /// <param name="cases">Case number selected from the menu</param>
        /// <param name="input">Input entered by the user</param>
        /// <param name="bet">Bet amount entered by the user</param>
        /// <returns></returns>
        private static int SpinRoulette(String cases, StringBuilder input, int bet)
        {
            int winningAmount = 0;

            int selectedNumber = WinningNumber(); // A winning number between 00 and 36 is generated randomly. 

            if (selectedNumber == -1)
                Console.WriteLine("\r\nWinning number is: 00");
            else
                Console.WriteLine("\r\nWinning number is: " + selectedNumber);

            if (input.Equals("00"))
            {
                input.Replace("00", "-1"); //00 is represented as -1.
            }

            switch (cases)
            {
                case "1":
                    if (selectedNumber == 0 || selectedNumber == -1)
                    {
                        winningAmount = 0; //Because 0 and 00 are labeled green.
                    }
                    else if ((input.Equals("red") && selectedNumber % 2 == 0) || (input.Equals("black") && selectedNumber % 2 != 0))
                    {
                        winningAmount = bet + bet; //Payoff 1 to 1
                    }
                    break;
                case "2":
                    if (selectedNumber == 0 || selectedNumber == -1)
                    {
                        winningAmount = 0; //Because 0 and 00 are neither even nor odd.
                    }
                    else if ((input.Equals("even") && selectedNumber % 2 == 0) || (input.Equals("odd") && selectedNumber % 2 != 0))
                    {
                        winningAmount = bet + bet; //Payoff 1 to 1
                    }
                    break;
                case "3":
                    if (selectedNumber == int.Parse(input.ToString()))
                    {
                        winningAmount = bet + 35 * bet; //Payoff 35 to 1
                    }
                    break;
                case "4":
                    if (input.Equals("first") && selectedNumber > 0 && selectedNumber < 13)
                    {
                        winningAmount = bet + 2 * bet; //Payoff 2 to 1
                    }
                    else if (input.Equals("second") && selectedNumber > 12 && selectedNumber < 25)
                    {
                        winningAmount = bet + 2 * bet; //Payoff 2 to 1
                    }
                    else if (input.Equals("third") && selectedNumber > 24 && selectedNumber < 37)
                    {
                        winningAmount = bet + 2 * bet; //Payoff 2 to 1
                    }
                    break;
                case "5":
                    if (input.Equals("low") && selectedNumber > 0 && selectedNumber < 19)
                    {
                        winningAmount = bet + bet; //Payoff 1 to 1
                    }
                    else if (input.Equals("high") && selectedNumber > 18 && selectedNumber < 37)
                    {
                        winningAmount = bet + bet; //Payoff 1 to 1
                    }
                    break;
            }
            if (winningAmount > 0)
                Console.WriteLine("\r\nCongratulations! You Won: $" + winningAmount);
            return winningAmount;
        }

        /// <summary>
        /// This method is used to generate a winning number randomly between the given range of 00 and 36.
        /// </summary>
        /// <returns></returns>
        private static int WinningNumber()
        {
            Random r = new Random();
            return r.Next(-1, 36);
        }
    }
}
