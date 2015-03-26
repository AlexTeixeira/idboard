using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace idboard_v1.ViewModel
{
    public class AbsenceLateViewModel : ViewModelBase
    {
        public InfoViewModel Info { get; set; }


        private String raison;

        public String Raison
        {
            get { return raison; }
            set
            {
                if (raison != value)
                {
                    raison = value;
                    RaisePropertyChanged(() => Raison);

                }

            }
        }

        private DateTimeOffset dateBegin;

        public DateTimeOffset DateBegin
        {
            get { return dateBegin; }
            set
            {
                if (dateBegin != value)
                {
                    dateBegin = value;
                    RaisePropertyChanged(() => DateBegin);

                }

            }
        }

        private DateTimeOffset dateEnd;

        public DateTimeOffset DateEnd
        {
            get { return dateEnd; }
            set
            {
                if (dateEnd != value)
                {
                    dateEnd = value;
                    RaisePropertyChanged(() => DateEnd);

                }

            }
        }

        private DateTime hour;

        public DateTime Hour
        {
            get { return hour; }
            set {
                if (hour != value)
                {
                    hour = value;
                    RaisePropertyChanged(() => Hour);
                }
            }
        }

        public ICommand SendLate { get; set; }

        public async void Late()
        {
            string text = "Bonjour," + Environment.NewLine + Environment.NewLine + UserInstance.Instance.LastName + " " + UserInstance.Instance.FirstName + " sera en retard et arrivera à " + Hour + Environment.NewLine + Environment.NewLine + " Raison : " + Raison;
            var mailto = new Uri("mailto:?to=teixeiraperso@gmail.com&subject=Absence&body=" + text);
            await Windows.System.Launcher.LaunchUriAsync(mailto);
        }
        public ICommand SendAbsence { get; set; }

        public async void Absence()
        {
            DateTime begin = DateBegin.DateTime;
            DateTime end = DateEnd.DateTime;
            var message = new StringBuilder();
            message.AppendLine("Bonjour,");
            message.AppendLine(UserInstance.Instance.LastName + " " + UserInstance.Instance.FirstName + " sera absence du " + begin.ToString("dd-MM-yyyy") + " au " + end.ToString("dd-MM-yyyy"));
            message.AppendLine(" Raison : " + Raison);

            var mailto = new Uri("mailto:?to=scolarite@ieid.eu&subject=Absence&body=" + message);
            await Windows.System.Launcher.LaunchUriAsync(mailto);
        }
        public AbsenceLateViewModel(INavigationService navigationService)
        {
            Info = new InfoViewModel(navigationService);
            SendAbsence = new RelayCommand(Absence);
            SendLate = new RelayCommand(Late);

            DateBegin = DateTime.Today;
            DateEnd = DateTime.Today;
        }
    }
}
