using System;
using System.Collections.Generic;
using System.Text;

namespace Exercici_Videos
{
    class Video
    {
        public int VideoId { get; set; }
        public string Title { get; set; }

        public string url;
        


        public int VideoDuration { get; set; }

        private List<string> tags;

        //Constructor
        public Video(string title, string url, int videoId)
        {
            VideoId = videoId;
            this.Title = title;
            this.url = url;
            VideoDuration = new Random().Next(1, 600);
            tags = new List<string>();
        }

        // Add Tags to the video
        public void AddTags(string tag)
        {
            tags.Add(tag);
        }

        public string GetTags()
        {
            string valReturn ="";

            foreach (string tag in tags)
            {
                valReturn += $" "+tag+",";
            }
            return valReturn;
        }
    }
}
