using System;
using System.Collections.Generic;

namespace Unit04.Game.Casting
{

    public class Mineral : Actor
    {
        
        //Temarary BULL
        private string message = "Fail";
        //Point to move 
        private Point direction = new Point(0,0);
        // Constructs a new instance of Mineral
        public Mineral()
       {
       }
        // Move the minerals 
        //public void MoveMinerals()
        //{
            //direction = new Point(0, -10);
        //}
        // Stuff for the Score
        public string GetMessage()
        {
            return message;
        }
        public void SetMessage(string input)
        {
            message = input;
        }

    }
}