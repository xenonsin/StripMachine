using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turing_Machine
{
    class Program
    {
        #region Command Class
        public class Commands
        {


            public Commands() { }

            public Commands(string stateNumber, string input, string output, string nextState, string direction)
            {
                this.StateNumber = stateNumber;
                this.Input = input;
                this.Output = output;
                this.NextState = nextState;
                this.Direction = direction;
            }

            public string StateNumber { set; get; }

            public string Input { set; get; }

            public string Output { set; get; }

            public string NextState { set; get; }

            public string Direction{ set; get; }

        }
        #endregion

        //public class States : List<Commands> {}
        
        static void Main(string[] args)
        {
            Welcome();

           // States statesCollection = new States();
            List<List<Commands>> statesCollection = new List<List<Commands>>();
            AddStates(statesCollection);    

            var currentState = statesCollection[0];

            string input = GetInput(statesCollection);

            List<string> output = FillOutput(input.Length);

            int index = 0;

            while (index < input.Length)
            {

                foreach (Commands currentCommand in currentState)
                {
                    if (input[index].ToString() == currentCommand.Input)
                    {
                        output[index] = currentCommand.Output;

                        DisplayOutput(output, currentCommand.StateNumber);

                        IncrementIndex(ref index, currentCommand.Direction);

                        GetNextState(ref currentState, statesCollection, currentCommand.NextState);

                        break;
                        
                    }
                }           
            }

            Console.ReadLine();
        }

        #region Adding States
        static void AddStates(List<List<Commands>> stateCollection)
        {
            //Checking States

            List<Commands> states = new List<Commands>();
            
            states.Add(new Commands("0", "0", "0", "1", "R"));
            states.Add(new Commands("0", "1", "1", "c0", "R"));

            states.Add(new Commands("1", "1", "1", "2", "R"));
            states.Add(new Commands("1", "0", "1", "c0", "L"));

            states.Add(new Commands("2", "0", "0", "3", "R"));
            states.Add(new Commands("2", "1", "1", "c1", "L"));

            states.Add(new Commands("3", "1", "0", "4", "R"));
            states.Add(new Commands("3", "0", "1", "c2", "L"));

            states.Add(new Commands("4", "0", "1", "5", "R"));
            states.Add(new Commands("4", "1", "1", "c3", "L"));

            states.Add(new Commands("5", "1", "1", "6", "R"));
            states.Add(new Commands("5", "0", "1", "c4", "L"));

            states.Add(new Commands("6", "0", "0", "7", "R"));
            states.Add(new Commands("6", "1", "1", "c5", "L"));

            states.Add(new Commands("7", "1", "0", "", "R"));
            states.Add(new Commands("7", "0", "1", "c6", "L"));

            //Cleanup States
            states.Add(new Commands("c0", "0", "1", "c0", "R"));
            states.Add(new Commands("c0", "1", "1", "c0", "R"));

            states.Add(new Commands("c1", "1", "1", "c0", "L"));
            states.Add(new Commands("c1", "0", "1", "c0", "L"));

            states.Add(new Commands("c2", "0", "1", "c1", "L"));
            states.Add(new Commands("c2", "1", "1", "c1", "L"));

            states.Add(new Commands("c3", "1", "1", "c2", "L"));
            states.Add(new Commands("c3", "0", "1", "c2", "L"));

            states.Add(new Commands("c4", "0", "1", "c3", "L"));
            states.Add(new Commands("c4", "1", "1", "c3", "L"));

            states.Add(new Commands("c5", "1", "1", "c4", "L"));
            states.Add(new Commands("c5", "0", "1", "c4", "L"));

            states.Add(new Commands("c6", "0", "1", "c5", "L"));
            states.Add(new Commands("c6", "1", "1", "c5", "L"));

            List<string> StateNumbers = new List<string>();

            foreach (var entry in states)
            {
                StateNumbers.Add(entry.StateNumber);
            }

            StateNumbers.Distinct().ToList();

            foreach (string entry in StateNumbers)
            {
                stateCollection.Add(GetSameStateNumbers(states, entry));
                
            }

            //states.ForEach(Print);
        }
        
        static List<Commands> GetSameStateNumbers(List<Commands> input, string number)
        {
            var output = new List<Commands>();

            //Enumerate through all items
            foreach (var item in input)
            {
                //See if type is 1, then add it.
                if (item.StateNumber == number)
                {
                    output.Add(item);
                }
            }

            return output;
        }

        private static void Print(Commands ins)
        {
            Console.WriteLine("state number: {0}, input: {1}, output: {2}, next state: {3}, direction: {4}", ins.StateNumber, ins.Input, ins.Output, ins.NextState, ins.Direction);
        }
        #endregion

        #region Helper Functions

        static void Welcome()
        {
           // Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("| Turing Machine!");
            Console.WriteLine("| Project by: Andrew, Tara Bassir, Lung Luong, Keno San Pablo");
            Console.WriteLine("| Group Code: 01010101");
            Console.WriteLine("| ACII Output (L): 01001100");
           // Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void PrintCommands(List<List<Commands>> statesCollection)
        {
            foreach (var entry in statesCollection)
            {
                entry.ForEach(Print);
                Console.WriteLine();
            }
        }

        static bool IsBinaryOnly(string str)
        {
            foreach (char number in str)
            {
                if (number < '0' || number > '1')
                    return false;
            }
            return true;
        }

        static string GetInput(List<List<Commands>> statesCollection)
        {
            bool isReady = false;

            string input = "";

            while (!isReady)
            {
                Console.WriteLine("Enter bit input: ");
                input = Console.ReadLine();

                if (input == "print commands")
                    PrintCommands(statesCollection);

                if (input.Length == 8 && IsBinaryOnly(input))
                    isReady = true;
                else
                    Console.WriteLine("Input Error. Please input an 8 digit binary number.");
            }

            return input;
        }

        static List<string> FillOutput(int inputLength)
    {
            List <string> output = new List<string>();

            for (int i = 0; i < inputLength; i ++ )
            {
                output.Add("");
            }

                return output;
    }
        #endregion

        #region Turing Function
        static void DisplayOutput(List<string> output, string StateNumber)
        {
            Console.WriteLine("Current State: " + StateNumber);

            foreach (string entry in output)
                Console.Write(entry);

            Console.WriteLine();
        }

        static void IncrementIndex(ref int index, string direction) //How about if index < 0?
        {

                if (direction == "R")
                    index++;
                else
                    index--;
        }

        static void GetNextState(ref List<Commands> currentState, List<List<Commands>> statesCollection, string NextState)
        {
            foreach (var state in statesCollection)
            {
                foreach (Commands nextCommands in state)
                {
                    if (nextCommands.StateNumber == NextState)
                    {
                        currentState = state;

                    }

                }

            }
        }
        #endregion


    }
}
