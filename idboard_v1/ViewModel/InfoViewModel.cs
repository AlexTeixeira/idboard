using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace idboard_v1.ViewModel
{
    public class InfoViewModel : ViewModelBase
    {
        private List<String> menu = new List<String>();


        private String firstName;

        public String FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    RaisePropertyChanged(() => FirstName);

                }

            }
        }

        private String lastName;

        public String LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    RaisePropertyChanged(() => LastName);

                }

            }
        }

        private String roleName;

        public String RoleName
        {
            get { return roleName; }
            set
            {
                if (roleName != value)
                {
                    roleName = value;
                    RaisePropertyChanged(() => RoleName);

                }

            }
        }

        private String idNumber;

        public String IdNumber
        {
            get { return idNumber; }
            set
            {
                if (idNumber != value)
                {
                    idNumber = value;
                    RaisePropertyChanged(() => IdNumber);

                }

            }
        }


        /** Méthode d'une commance avec utilisation d'un delegate*/

        private RelayCommand getInfo;

        public ICommand GetInfo
        {
            get
            {
                return getInfo ??
                 (
                     getInfo = new RelayCommand
                     (
                         () =>
                         {
                             var newRoleName = Regex.Split(UserInstance.Instance.RoleName, "élèvede");
                             RoleName = newRoleName[1];
                             FirstName = UserInstance.Instance.FirstName;
                             LastName = UserInstance.Instance.LastName;
                             IdNumber = Login.Instance.IDBoard;
                         }
                     )
                 );
            }

        }


        private RelayCommand<String> changePage;

        public ICommand ChangePage
        {
            get
            {
                return changePage ??
                 (
                     changePage = new RelayCommand<String>
                     (
                         (view) =>
                         {
                             callNavigationService(view);
                         }
                     )
                 );
            }

        }


        private RelayCommand displayMenu;

        public ICommand DisplayMenu
        {
            get
            {
                return displayMenu ??
                 (
                     displayMenu = new RelayCommand
                     (
                         async () =>
                         {

                             var message = string.Join(Environment.NewLine, menu);
                             MessageDialog erreur = new MessageDialog(message);
                             await erreur.ShowAsync();
                         }
                     )
                 );
            }

        }


        /*Commande for navigation*/
        private INavigationService navigationService;


        public void callNavigationService(String view)
        {
            navigationService.NavigateTo(view);
        }
        public InfoViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            menu.Add("Calendrier");
            menu.Add("Offre");
            menu.Add("Message");
            menu.Add("Absence/Retard");

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        
    }
}
