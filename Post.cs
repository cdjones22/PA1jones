using System;
using System.Collections.Generic;
using System.Text;

namespace BIG_AI
{
    class Post
    {
        // Auto-implemented properties for get and set
        public int PostID { get; set; }
        public string PostText { get; set; }
        public DateTime PostDate { get; set; }

        // Constructor
        public Post(int ID, string text, DateTime date)
        {
            PostID = ID;
            PostText = text;
            PostDate = date;
        }

        // Methods

        public void displayPost()
        {
            Console.WriteLine("Post ID: " + PostID);
            Console.WriteLine("Post Text: " + PostText);
            Console.WriteLine("Post Date: " + PostDate.ToShortDateString());
            Console.WriteLine("-----------------");
        }

    }
}
