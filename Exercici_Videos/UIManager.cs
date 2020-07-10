using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Exercici_Videos
{
    class UIManager
    {
        #region Variables Declaration

        private int id;

        private List<User> usersList; // Name - Surname - Password

        private string mainScreen = "Welcome to OfiTube \n\n Commands: \n\n 1 - Register \n 2 - Login \n\n 0 - Close Application ";

        private string profileScreen;

        private string currentContext;

        private string emptyError = "\n\n Input field cant be empty";

        string inputNotValid = "\n\n Input not valid";

        string input;

        #endregion

        public void StartUIManger()
        {
            id = 0;
            usersList = new List<User>();
            MainScreen();
        }

        private void MainScreen(string errorMsg = "")
        {
            AskInput(mainScreen, errorMsg);
            if (input == "1")      { RegisterScreen(); }
            else if (input == "2") { if (usersList.Count > 0) { LoginScreen(); } else { MainScreen("\n\n There is not registered users yet"); } }
            else if (input == "0") { Environment.Exit(0); }
            else { MainScreen(inputNotValid); }
        }

        private void RegisterScreen()
        {
            string _name, _surname, _pass;

            AskInput("Insert your name");
            _name = input;
            AskInput("Insert your surname");
            _surname = input;
            AskInput("Insert a password");
            _pass = input;
            Console.Clear();
            Program.RegisteredUsers++;
            id = Program.RegisteredUsers;
            usersList.Add(new User(id, _name, _surname, _pass));
            Console.WriteLine($"Creating User...\n\n Name: { usersList[id-1].UserName }\n " +
                $"Surname: { usersList[id-1].UserSurname }\n " +
                $"Passowrd: {  usersList[id-1].UserPassword } \n\n press any key to continue");
            Console.ReadKey();
            ProfileScreen();
        }

        private void LoginScreen(string errorMsg = "")
        {
            AskInput("Insert account name", errorMsg);
            foreach(User users in usersList)
            {
                if(input == users.UserName)
                {
                    AskInput("Insert password name");
                    if(input == users.UserPassword)
                    {
                        id = users.UserId;
                        ProfileScreen();
                    }
                    else
                    {
                        LoginScreen("\n\n Password is not valid");
                    }
                }
                else
                {
                    LoginScreen("\n\n Name is not valid");
                }
            }
        }

        private void ProfileScreen(string errorMsg = "")
        {
            profileScreen = $"Welcome {usersList[id-1].UserName} to the main menu. Account created on {usersList[id-1].RegisterDate} \n\n " +
                $"Commands: \n\n 1 - Create Video \n 2 - See a list of all my videos \n 3 - EditVideo \n 4 - Exit Account \n\n 0 - Close Application ";
            
            AskInput(profileScreen, errorMsg);

            if (input == "1") { CreateVideo(); }
            else if (input == "2") { if (usersList[id - 1].userVideos.Count > 0) { SelectVideoFromList(); } ProfileScreen("\n\n You dont have videos yet. First create a video");  }
            else if (input == "3") { if (usersList[id - 1].userVideos.Count > 0) { SelectVideoFromList("", true); } ProfileScreen("\n\n You dont have videos yet. First create a video");  }
            else if (input == "4") { MainScreen(); }
            else if (input == "0") { Environment.Exit(0); }
            else { ProfileScreen(inputNotValid); }
        }

        private void CreateVideo(string errorMsg = "")
        {
            string title;
            AskInput("Video Title", errorMsg);
            title = input;
            usersList[id - 1].CreateVideo(title, $"www.ofitube.com/{usersList[id -1].UserName.ToLower()}/{title.ToLower()}");
            Console.WriteLine($"Video named {title} created successfully, press any key");
            Console.ReadKey();
            ProfileScreen();
        }

        private void SelectVideoFromList(string errorMsg = "", bool editTags = false)
        {
            string output = "";
            foreach(Video vid in usersList[id - 1].userVideos)
            {
                output += $"\n {vid.VideoId} - {vid.Title}";
            }
            AskInput("Select the video you want to edit by writing the index number \n" + output + errorMsg);
            foreach(Video vid in usersList[id - 1].userVideos)
            {
                if(input == vid.VideoId.ToString())
                {
                    if (editTags) { AddTags(vid.VideoId); break; }
                    GoToVideo(vid.VideoId);
                    break;
                }
            }
            SelectVideoFromList(inputNotValid);
        }

        public void GoToVideo(int videoId, string st = "Buffering...")
        {
            string videoState = st;

            Video currentVideo = usersList[id - 1].userVideos[videoId];
            
            AskInput($" \n \n " +
                $"Url: https://{currentVideo.url}\n" +
                "****************************************************\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "*                                                  *\n" +
                "****************************************************\n" +
                $"Title: {currentVideo.Title} \n" +
                $"Tags: {currentVideo.GetTags()} \n" +
                $"Video State: {st}\n" +
                $"Duration: {currentVideo.VideoDuration} seconds" +
                "\n 1 - Play video \n 2 - Pause \n 3 - Stop \n 4 - Exit video player "
                );
            if(input == "1") { GoToVideo(videoId, "Playing");  }
            if(input == "2") { GoToVideo(videoId, "Paused"); }
            if(input == "3") { GoToVideo(videoId, "Stopped"); }
            if(input == "4") { ProfileScreen(); }
            else { GoToVideo(videoId, st); }
        }

       /* public enum VideoState { Playing, Paused, Stopped, Buffering, Ended }
        /// <summary>
        /// 0 = Playing, 1 = Paused, 2 = Stopped, 3 = Buffering, 4 = Ended
        /// </summary>
        public VideoState MyVideoState
        {
            get
            {
                return MyVideoState;
            }
            set
            {
                MyVideoState = value;
            }
        }
       */
        private void AddTags(int videoId, string errorMsg = "")
        {
            AskInput("Add as many tags as you want, write 1 when you end", errorMsg);
            if(input == "1") { ProfileScreen(); }
            usersList[id - 1].userVideos[videoId].AddTags(input);
            AddTags(videoId);
        }

        private void AskInput(string output, string errorMessage = "")
        {
            currentContext = output;
            Console.Clear();
            Console.WriteLine(currentContext + errorMessage);
            ReadLine();
        }

        private void ReadLine()
        {
            try
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) 
                {
                    AskInput(currentContext, emptyError);
                }
            }
            catch
            {
                AskInput(currentContext, emptyError);
            }
        }
    }
}
