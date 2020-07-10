using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace Exercici_Videos
{
    class User
    {
        public User(int id, string name, string surname, string pass)
        {
            UserId = id;
            UserName = name;
            UserSurname = surname;
            UserPassword = pass;
            RegisterDate = DateTime.Now;
            userVideos = new List<Video>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPassword { get; set; }
        public DateTime RegisterDate { get; }

        public List<Video> userVideos;
        public void CreateVideo(string title, string url)
        {
            userVideos.Add(new Video(title, url, userVideos.Count));
        }
    }
}
