using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using idboard_v1.DataModel.InterShipsFolder;
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
    public class OfferViewModel : ViewModelBase
    {
        public List<InternShips> listInterships;

        public List<InternShips> ListInterships
        {
            get { return listInterships; }
            set
            {
                if (listInterships != value)
                {
                    listInterships = value;
                    RaisePropertyChanged(() => ListInterships);

                }

            }
        }
        public InfoViewModel Info { get; set; }

        public ICommand GetOffersCommand { get; set; }

        private async void GetOffers()
        {

            Internships msg = new Internships("http://idboard.net/idws/api/", "Internships");

            var customObj = new { IDBoard = Login.Instance.IDBoard, Password = Login.Instance.Password, Start = msg.Start, Count = msg.Count };

            String rep = await msg.RunAsync(customObj);
            var obj = JObject.Parse(rep);

            var result = obj;
            var resultObj = JsonConvert.DeserializeObject<InternshipsInfo>(rep);
            if (int.Parse(resultObj.Result.ExitCode) != 0)
            {
                MessageDialog erreur = new MessageDialog("Erreur lors de la récupération des offres");
                await erreur.ShowAsync();
            }
            else
            {
                ListInterships = resultObj.InternShips;
            }

        }

        public InternShips getOneInternship;

        public InternShips GetOneInternship
        {
            get { return getOneInternship; }
            set
            {
                if (getOneInternship != value)
                {
                    getOneInternship = value;
                    RaisePropertyChanged(() => GetOneInternship);

                    ConnexionGetInternship();
                }
            }
        }

        public async void ConnexionGetInternship()
        {
            Internship intern = new Internship("http://idboard.net/idws/api/", "Internship");

            var customObj = new { IDBoard = Login.Instance.IDBoard, Password = Login.Instance.Password, IDInternship = GetOneInternship.IDInternship };

            String rep = await intern.RunAsync(customObj);
            var obj = JObject.Parse(rep);

            var result = obj;
            var resultObj = JsonConvert.DeserializeObject<InternshipInfo>(rep);
            if (int.Parse(resultObj.Result.ExitCode) != 0)
            {
                MessageDialog erreur = new MessageDialog("Erreur lors de la récupération des offres");
                await erreur.ShowAsync();
            }
            else
            {
                MessageDialog info = new MessageDialog(resultObj.Internship.Title + " - " + resultObj.Internship.CompanyName +  Environment.NewLine  + resultObj.Internship.City + Environment.NewLine + Environment.NewLine + Environment.NewLine + resultObj.Internship.MissionSummary);
                await info.ShowAsync();

            }
        }
        public OfferViewModel(INavigationService navigationService)
        {
            Info = new InfoViewModel(navigationService);
            GetOffersCommand = new RelayCommand(GetOffers);

        }
    }
}
