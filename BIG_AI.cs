using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace BIG_AI
{
    class BIG_AI
    {
        static void ReadDataFromFile(string path,ref List<Post> posts)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                posts = new List<Post>();
                int count = 0;

                foreach (string line in lines)
                {
                    string[] param = line.Split('#');
                    int PostID = Convert.ToInt32(param[0]);
                    string PostText = param[1];
                    DateTime PostDate = DateTime.ParseExact(param[2],"dd-MM-yyyy",null);
                    posts.Add(new Post(PostID, PostText, PostDate));
                    count++;
                }
                posts = SortList(posts); //  Sorts the list
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        static List<Post> AddPost(List<Post> posts)
        {
            Console.Write("Enter the post text: ");
            string text = Console.ReadLine();
            int new_Post_ID = posts.Max(post => post.PostID) + 1;
            posts.Add(new Post(new_Post_ID, text, DateTime.Now));
            //Write to the file
            posts = SortList(posts); //  Sorts the list
            WriteToFile(posts);
            return posts;
        }

        static List<Post> DeletePost(List<Post> posts)
        {
            Console.Write("Enter the ID: ");
            string Id_String = Console.ReadLine();
            int Id = Convert.ToInt32(Id_String);
            Post post = posts.Find((id) => id.PostID == Id);
            if(post != null)    
                posts.Remove(post);
            //Write to the file
            WriteToFile(posts);
            return posts;
        }


        static void WriteToFile(List<Post> posts)
        {
            if(posts.Count == 0)
                File.WriteAllText(Directory.GetCurrentDirectory().ToString() + "\\Posts.txt", null); // In case user empties all the record
            for (int i =0; i < posts.Count; i++)
            {
                if(i==0)
                    File.WriteAllText(Directory.GetCurrentDirectory().ToString() + "\\Posts.txt", posts[i].PostID+"#"+posts[i].PostText+"#"+posts[i].PostDate.ToString("dd-MM-yyyy")+'\n');
                else
                    File.AppendAllText(Directory.GetCurrentDirectory().ToString() + "\\Posts.txt", posts[i].PostID + "#" + posts[i].PostText + "#" + posts[i].PostDate.ToString("dd-MM-yyyy") + '\n');
            }
        }

        static List<Post> SortList(List<Post> posts)
        {
            posts.Sort((x, y) => y.PostDate.CompareTo(x.PostDate));
            return posts;
        }

        static void DisplayPosts(List<Post> posts)
        {
            for (int i = 0; i < posts.Count; i++)
            {
                posts[i].displayPost();
            }
        }

        static void Main(string[] args)
        {
            //Uncomment this line below to output the current working directory
            //Console.WriteLine(Directory.GetCurrentDirectory().ToString());
            string option;
            List<Post> posts = null; // List of posts
            ReadDataFromFile(Directory.GetCurrentDirectory().ToString() + "\\Posts.txt", ref posts); // Passing by reference
            while (true)
            {
                Console.WriteLine("Select your option\n1.Show all posts\n2.Add a post\n3.Delete a post by ID\n4.Exit");
                option = Console.ReadLine();
                if(option.Equals("1"))
                {
                    DisplayPosts(posts);
                }
                else if (option.Equals("2"))
                {
                    AddPost(posts);
                }
                else if (option.Equals("3"))
                {
                    DeletePost(posts);
                }
                else if (option.Equals("4"))
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Option");
                }
            }
        }
    }
}
