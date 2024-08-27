// See https://aka.ms/new-console-template for more information
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks.Dataflow;
using MyApp.Models;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        //main is always written and run on COnsole.App
        static void Main(string[] args)
        {

            Computer myComputer = new Computer();
            myComputer.SetMotherboard("new");
            myComputer.SetVideoCard("cardd");
            Console.WriteLine(myComputer.GetVideoCard());
            Console.WriteLine("----------------");
        }

    }
}