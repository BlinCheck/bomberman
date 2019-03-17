using Newtonsoft.Json;
using System;
using System.Threading;

namespace Bomberman
{
    class Program
    {
        static void Main(string[] args)
        {
            //Level level = new Level(3);
            string json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level3.json");
            Level level = JsonConvert.DeserializeObject<Level>(json);
            //string json = JsonConvert.SerializeObject(level);
            //System.IO.File.WriteAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level3.json", json);
            Session session = new Session(level);
            session.Start();
            
        }

        /*
         * generate
        string json = JsonConvert.SerializeObject(level);
        System.IO.File.WriteAllText(@"D:\level1.txt", json);
        */
             

    }


    
}
