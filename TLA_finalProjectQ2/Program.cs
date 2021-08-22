using System;
using System.Collections.Generic;

namespace TLA_finalProjectQ2
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            List<string> rules = new List<string>();
            string temp = "";
            int counter = 0;
            string[] parts, steps;
            List<Transition> transitions = new List<Transition>();
            int number;
            number = int.Parse(Console.ReadLine());
            string current = "1";
            List<string> questions = new List<string>();
            bool flag = false;
            string result;
            int correct = 0;
            int maxLength = 0;
            List<List<string>> matching = new List<List<string>>();
            List<string> holder = new List<string>();
            int start_index1, start_index2;
            for(int i = 0; i < input.Length - 1; i++)
            {
                if(input[i] == '0' && input[i + 1] == '0')
                {
                    for(int j = counter; j < i; j++)
                    {
                        temp += input[j];
                    }
                    rules.Add(temp);
                    counter = i + 2;
                    temp = "";
                }
            }

            for (int j = counter; j < input.Length; j++)
            {
                temp += input[j];
            }
            rules.Add(temp);

            for (int i = 0; i < rules.Count; i++)
            {
                //Console.WriteLine(rules[i]);
                parts = rules[i].Split('0');
                Transition transition = new Transition();
                transition.start = parts[0];
                transition.variable = parts[1];
                transition.end = parts[2];
                transition.write = parts[3];
                transition.way = parts[4];
                if(transition.end.Length > maxLength)
                {
                    maxLength = transition.end.Length;
                }
                if(transition.variable == "1")
                {
                    holder.Add(transition.start);
                    holder.Add(transition.end);
                }
                transitions.Add(transition);
            }

            /*for(int i = 0; i < transitions.Count; i++)
            {
                Console.WriteLine("transition " + i);
                Console.WriteLine("start: " + transitions[i].start);
                Console.WriteLine("variable: " + transitions[i].variable);
                Console.WriteLine("end: " + transitions[i].end);
                Console.WriteLine();
            }*/

            for(int i = 0; i < number; i++)
            {
                questions.Add(Console.ReadLine());
            }

            for(int i = 0; i < questions.Count; i++)
            {
                correct = 0;
                current = "1";
                start_index1 = "10101010101010101010101010".Split('0').Length;
                start_index2 = "01010101010101010101010101".Split('0').Length;
                int length = questions[i].Split('0').Length;
                questions[i] = "10101010101010101010101010" + questions[i] + "01010101010101010101010101";
                parts = questions[i].Split('0');
                for(int j = start_index1 - 1; j < length + start_index1 + start_index2;)
                {
                    flag = false;
                    for(int z = 0; z < transitions.Count; z++)
                    {
                        if(transitions[z].start == current && (transitions[z].variable == parts[j] || (transitions[z].variable == "1" && parts[j] == ""))) 
                        {
                            current = transitions[z].end;
                            parts[j] = transitions[z].write;
                            if(transitions[z].way == "1")
                            {
                                j--;
                            }
                            else
                            {
                                j++;
                            }
                            flag = true;
                            break;
                        }
                    }

                    if (current.Length == maxLength)
                    {
                        Console.WriteLine("Accepted");
                        break;
                    }

                    if (!flag)
                    {
                        Console.WriteLine("Rejected");
                        break;
                    }
                }

                
            }
        }
    }

    class Transition
    {
        public string start { set; get; }
        public string end { set; get; }
        public string variable { set; get; }
        public string write { set; get; }
        public string way { get; set; }
    }
}
