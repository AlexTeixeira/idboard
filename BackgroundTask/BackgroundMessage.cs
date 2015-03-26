using Newtonsoft.Json;
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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using BackgroundTask.Model.MessageFolder;
using Windows.UI.Notifications;
using BackgroundTask.Model.CoursesFolder;

namespace BackgroundTask
{
    public sealed class BackgroundMessage : IBackgroundTask
    {
        public static string IDNumber { get; set; }
        public static string Password { get; set; }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            var customObj = new { IDBoard = IDNumber, Password = Password, Start = 0, Count = 20 };
            String path = "Messages";

            var msg = await GetMessage(path,customObj);

            path = "Courses";
            var customObj2 = new { IDBoard = IDNumber, Password = Password, DateStart = DateTime.Today.AddDays(7).ToString("o"), DateEnd = DateTime.Today.AddDays(14).ToString("o") };

            var calendar = await GetMessage(path, customObj2);

            SendMsgToast(msg);
            SendCalendarToast(calendar);

            deferral.Complete();
        }

        private static async Task<String> GetMessage(String path, Object customObj)
        {
            String resultMsg = null;

            //Get file with login/mdp

            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (local != null)
            {
                // Get the DataFolder folder.

                try
                {
                    if (String.IsNullOrEmpty(IDNumber) && String.IsNullOrEmpty(Password))
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
                    

                }
                catch
                {
                    Debug.WriteLine("Folder IDBoard not found");
                }

                String url = "http://idboard.net/idws/api/";
                string json = JsonConvert.SerializeObject(customObj);


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsJsonAsync(path, customObj);

                    if (response.IsSuccessStatusCode)
                    {
                        resultMsg = await response.Content.ReadAsStringAsync();
                        

                    }


                }

            }
            return resultMsg;
        }

        private static void SendMsgToast(String Msg)
        {
            IEnumerable<MyMessages> listMessage;
            var resultObj = JsonConvert.DeserializeObject<MessagesInfo>(Msg);

            if (int.Parse(resultObj.Result.ExitCode) == 0)
            {
                listMessage = resultObj.Messages;

                foreach(MyMessages myMsg in listMessage){
                    if (myMsg.DateStart.CompareTo(DateTime.Today.ToString("s")) == 0)
                    {
                        var notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

                        var toeastElement = notificationXml.GetElementsByTagName("text");

                        toeastElement[0].AppendChild(notificationXml.CreateTextNode("CampusID"));
                        toeastElement[1].AppendChild(notificationXml.CreateTextNode(myMsg.BBCode));
                        var toastNotification = new ToastNotification(notificationXml);

                        ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
                    }
                    
                }

                
            }
                              
        }

        private static void SendCalendarToast(String calendar)
        {
            IEnumerable<Course> listCourse;
            var resultObj = JsonConvert.DeserializeObject<CoursesInfo>(calendar);

            if (int.Parse(resultObj.Result.ExitCode) == 0)
            {
                listCourse = resultObj.Courses;

                if (listCourse.Count() > 0)
                {
                    var notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

                    var toeastElement = notificationXml.GetElementsByTagName("text");

                    toeastElement[0].AppendChild(notificationXml.CreateTextNode("CampusID"));
                    toeastElement[1].AppendChild(notificationXml.CreateTextNode("Le calendrier de la semaine prochaine est disponible"));
                    var toastNotification = new ToastNotification(notificationXml);

                    ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
                }

            }
        }

    }
}
