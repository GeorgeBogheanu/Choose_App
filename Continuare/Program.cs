using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;

public class Choose_App
{
    public static bool isRunning(string name) {                                 // method to see if the process is working
        foreach (var item in Process.GetProcesses())                            // item is the var who get every process
        {

            if (item.ProcessName.Contains(name)) { return true; }               // testing the process to see if is the one that i need
        }
        return false;
    }

    public static void closeApp(string name) {                                  // method for closing the process
        foreach (var item in Process.GetProcesses())
        {
            if (item.ProcessName.Contains(name))                                // testing the processes
            {
                item.Kill();               
            }          
        }
        Console.WriteLine("App has been closed!");
    }

    public static void keybind() {                                              // method for second thread wihch is waiting for key to close
        Console.WriteLine("Press q to close");
        char key;
        key = Console.ReadKey().KeyChar;
        if (key == 'q' || key == 'Q') System.Environment.Exit(0);
    }

    public static void Main(string[] args)
    {
        Thread mainthread = Thread.CurrentThread;
        Thread newThread = new Thread(keybind);

        int lifetime;                                                           
        int freq;

        Console.WriteLine("Choose the app: ");
        string answear=  Console.ReadLine();
        if(answear == "" ) throw new Exception("Null imput");                   //checking the ans 

        Console.WriteLine("Maximum lifetime(min) :");
        var isInt =int.TryParse(Console.ReadLine(), out lifetime);
        if(!isInt) throw new Exception("Not an integer!");                      // checking the lifetime to be an int

        Console.WriteLine("Frequency(min) : ");
        var isInt2= int.TryParse(Console.ReadLine(), out freq);
        if (!isInt) throw new Exception("Not an integer!");                     // checking the freq to be an int

        newThread.Start();
        int time = -freq;                                                       // the int who will keep the time
        while (true)
             {
            time = time + freq;
            Thread.Sleep(freq*60000);                                           // suspend the thread for freq * 1 min
            Console.WriteLine(time);
            if (!isRunning(answear))                                            //cecking if the app is already closed to reset the time= -freq to actually start from 0
               { time = -freq; }
            else if (time >= lifetime)                                          // testing if it passed input mins (if frq is not a divisor of lifetime)
                {                                                               // ex: if lifetime is 7 and frq is 3, at the third test will close the app, that mean min 9
                 closeApp(answear);
                 time = -freq;
                }
            }
        }  
    }

