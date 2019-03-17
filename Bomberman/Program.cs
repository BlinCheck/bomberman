using Newtonsoft.Json;
using System;
using System.Threading;

namespace Bomberman
{
    class Program
    {
        static void Main(string[] args)
        {
            //Level level = new Level(1);
            string json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level1.json");
            Level level = JsonConvert.DeserializeObject<Level>(json);
            Session session = new Session(level);
            session.Start();
            
        }


        /*public static void pickUpBonus(Elem elem)
        {
            if (elem.Name.Equals("addBomb"))
            {
                Session.bombAmount++;
                displayBombAmount();
            }

            if (elem.Name.Equals("addLife"))
            {
                Session.lives++;
                displayLives();
            }
        }*/

        /*
         * generate
        string json = JsonConvert.SerializeObject(level);
        System.IO.File.WriteAllText(@"D:\level1.txt", json);
        */
             

    }


    
}
