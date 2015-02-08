using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
                if (idNumber != value)
                {
                    idNumber = value;
                    RaisePropertyChanged(() => IDNumber);
                }

            }
        }


        private String password;

         public ICommand ConnexionCommand { get; set; }


         private void Connexion(DependencyObject parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            UserBoard userB = new UserBoard(IDNumber, password);
            userB.Connect("http://idboard.net/idws/api/","UserBoard");
         }
        public MainViewModel()
        {
            ConnexionCommand = new RelayCommand<DependencyObject>(Connexion);




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