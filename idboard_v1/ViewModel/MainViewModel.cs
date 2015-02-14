using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using idboard_v1.DataModel;
using Model;
using Model.classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Popups;
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


        private bool ringIsActive;


        public bool RingIsActive
        {
            get { return ringIsActive; }
            set 
            {
                if (ringIsActive != value)
                {
                    ringIsActive = value;
                    RaisePropertyChanged(() => RingIsActive);

                }
                
            }
        }
        
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


         private async void Connexion(DependencyObject parameter)
        {
            RingIsActive = true;
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            UserBoard userB = new UserBoard("http://idboard.net/idws/api/", "UserBoard");
            Login.Instance.IDBoard = IDNumber;
            Login.Instance.Password = password;

            String rep = await userB.RunAsync(Login.Instance);
            var obj = JObject.Parse(rep);

            var result = obj;
            var resultObj = JsonConvert.DeserializeObject<UserInfo>(rep);
            if (int.Parse(resultObj.Result.ExitCode )!= 0)
            {
                MessageDialog erreur = new MessageDialog("Erreur lors de la connexion");
                await erreur.ShowAsync();
            }
            RingIsActive = false;
         }
        public MainViewModel()
        {
            ConnexionCommand = new RelayCommand<DependencyObject>(Connexion);

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