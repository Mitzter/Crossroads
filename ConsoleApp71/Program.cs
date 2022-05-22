using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp71
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int greenLightDuration = int.Parse(Console.ReadLine()); 
            int freeWindow = int.Parse(Console.ReadLine());

            Queue<string> lightQueue = new Queue<string>(); // Original Queue of cars for the green Light.
            string command; 
            int carsPassed = 0;
            while (true)
            {
                command = Console.ReadLine();
                
                if (command != "green")
                {
                    lightQueue.Enqueue(command);
                }
                else
                {
                    Queue<char> currentChar = new Queue<char>(); // Separate secondary queue for individual characters of the cars.
                                                                 // Used for removing individual characters of each car, as well as finding the character a car might have been hit at.
                   
                    foreach (string car in lightQueue)
                    {
                      
                        foreach (char c in car)
                        {
                            currentChar.Enqueue(c);
                        }
                    }
                    int counter = 0; // Combined with the Primary and Secondary Queues, this counter is used for keeping track of the amount of characters that have passed the crossroads,
                                     // as well as matching the characters with the Peek in the Primary Queue.
                                     
                    for (int i = 0; i < greenLightDuration; i++)
                    {
                        counter++;
                        if (currentChar.Count == 0)  
                        {

                            counter = 0;
                            continue;
                        }
                        else if (counter == lightQueue.Peek().Length)
                        {
                           
                            lightQueue.Dequeue();
                            counter = 0;
                            carsPassed++;
                        }
                        currentChar.Dequeue();
                        
                    }
                    int passedCar = 0; // Counter used for keeping track of the car inside the crossroads during the "Free Window" timer after the Green Light Duration has ended.
                    if (counter != 0) 
                    {
                        for (int i = 0; i < freeWindow; i++)
                        {
                            counter++;
                            if (counter == lightQueue.Peek().Length)
                            {
                               
                                lightQueue.Dequeue();
                                counter = 0;
                                carsPassed++;
                                passedCar++;
                            }
                            
                            else if (passedCar == 0)
                            {
                                currentChar.Dequeue();
                            }

                            if (passedCar == 1)
                            {
                                counter = 0;
                            }
                            


                        }
                    }

                    int hitCar = counter; // A car is hit if the counter != 0
                    if (hitCar != 0)
                    {
                        
                        Console.WriteLine("A crash happened!");
                        Console.WriteLine($"{lightQueue.Peek()} was hit at {currentChar.Peek()}.");
                        break;
                    }
                    

                }

                if (command == "END")
                {
                    Console.WriteLine("Everyone is safe.");
                    Console.WriteLine($"{carsPassed} total cars passed the crossroads.");
                    break;
                }

            }
            
        }
    }
}
