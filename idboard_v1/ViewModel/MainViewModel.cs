using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace idboard_v1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 

        private String idNumber;

        public String IDNumber
        {
            get { return idNumber; }
            set
            {
                if (idNumber != null)
                {
                    idNumber = value;
                    RaisePropertyChanged(() => idNumber);
                }

            }
        }

        private String password;

        public String Password
        {
            get { return password; }
            set
            {
                if (password != null)
                {
                    password = value;
                    RaisePropertyChanged(() => password);

                }

            }
        }


        public ICommand ConnexionCommand { get; set; }


        private void Connexion(MainViewModel main)
        {
            Debug.WriteLine("Je suis " + main.idNumber);
        }
        public MainViewModel()
        {
            ConnexionCommand = new RelayCommand<MainViewModel>(Connexion);

            //ConnexionCommand = new (callWB);

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