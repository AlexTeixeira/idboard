using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using idboard_v1.DataModel.MessageFolder;
using Model;
using Model.classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace idboard_v1.ViewModel
{
    public class MessageViewModel : ViewModelBase
    {
        private MessagesInfo myMessages;
        public InfoViewModel Info { get; set; }

        public List<MyMessages> listMessage;

        public List<MyMessages> ListMessage
        {
            get { return listMessage; }
            set
            {
                if (listMessage != value)
                {
                    listMessage = value;
                    RaisePropertyChanged(() => ListMessage);

                }

            }
        }
        public ICommand GetMessageCommand { get; set; }

        private async void GetMessage()
        {

            Messages msg = new Messages("http://idboard.net/idws/api/", "Messages");

            var customObj = new { IDBoard = Login.Instance.IDBoard, Password = Login.Instance.Password, Start = msg.Start, Count = msg.Count };

            String rep = await msg.RunAsync(customObj);
            var obj = JObject.Parse(rep);

            var result = obj;
            var resultObj = JsonConvert.DeserializeObject<MessagesInfo>(rep);
            myMessages = JsonConvert.DeserializeObject<MessagesInfo>(rep);
            if (int.Parse(resultObj.Result.ExitCode) != 0)
            {
                MessageDialog erreur = new MessageDialog("Erreur lors de la récupération des Messages");
                await erreur.ShowAsync();
            }
            else
            {
                ListMessage = resultObj.Messages;
            }

        }

        public MyMessages displayMessage;

        public MyMessages DisplayMessage
        {
            get { return displayMessage; }
            set
            {
                if (displayMessage != value)
                {
                    displayMessage = value;
                    RaisePropertyChanged(() => DisplayMessage);

                    GetOneMessage();
                }

            }
        }


        public async void GetOneMessage()
        {
            MessageDialog info = new MessageDialog(DisplayMessage.BBCode);
            await info.ShowAsync();
        }
        public MessageViewModel(INavigationService navigationService)
        {
            Info = new InfoViewModel(navigationService);
            GetMessageCommand = new RelayCommand(GetMessage);

        }
    }
}
