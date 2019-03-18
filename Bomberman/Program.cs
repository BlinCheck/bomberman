using Newtonsoft.Json;
using System;
using System.Threading;

namespace Bomberman
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.DisplayMenu();
        }

        /*
         Level level = new Level(3);
            string json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level3.json");
            Level level = JsonConvert.DeserializeObject<Level>(json);
            string json = JsonConvert.SerializeObject(level);
            System.IO.File.WriteAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level3.json", json);
        */


    }



}
