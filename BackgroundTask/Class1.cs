using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.Web.Syndication;

namespace BackgroundTask
{
 

    public sealed class Class1 : IBackgroundTask
    {
        public static string IDNumber { get; set; }
        public static string Password { get; set; }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // Get a deferral, to prevent the task from closing prematurely 
            // while asynchronous code is still running.
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            // Download the feed.
            var feed = await GetMessage();

            // Update the live tile with the feed items.
            SendMsgToast(feed);

            // Inform the system that the task is finished.
            deferral.Complete();
        }

        private static async Task<SyndicationFeed> GetMessage()
        {
            SyndicationFeed resultCalendar = null;

            //Get file with login/mdp

            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (local != null)
            {
                // Get the DataFolder folder.

                try
                {
                    var dataFolder = await local.GetFolderAsync("IDBoard");

                    // Get the file.
                    var file = await dataFolder.OpenStreamForReadAsync("Save.txt");
                    if (file != null)
                    {
                        String result;
                        // Read the data.
                        using (StreamReader streamReader = new StreamReader(file))
                        {
                            result = streamReader.ReadToEnd();
                        }

                        var obj = JObject.Parse(result);
                        IDNumber = (String)obj["IDBoard"];
                        Password = (String)obj["Password"];


                    }

                }
                catch
                {
                    Debug.WriteLine("Folder IDBoard not found");
                }

                try
                {
                    // Create a syndication client that downloads the feed.  
                    SyndicationClient client = new SyndicationClient();
                    client.BypassCacheOnRetrieve = true;
                    client.SetRequestHeader("IdBoard", IDNumber);
                    client.SetRequestHeader("Password", Password);

                    // Download the feed. 
                    resultCalendar = await client.RetrieveFeedAsync(new Uri("http://idboard.net/idws/api/Courses"));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }


            }
            return resultCalendar;
        }

        private static void SendMsgToast(SyndicationFeed feed)
        {
            foreach (var item in feed.Items)
            {
                
            }

        }
    }
}
