using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using idboard_v1.DataModel;
using Model;
using Model.classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.IO;


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


         private bool saveLoginIsChecked;


         public bool SaveLoginIsChecked
         {
             get { return saveLoginIsChecked; }
             set
             {
                 if (saveLoginIsChecked != value)
                 {
                     saveLoginIsChecked = value;
                     RaisePropertyChanged(() => SaveLoginIsChecked);
                 }

             }
         }
         /** Méthode d'une commance avec utilisation d'un delegate*/

         private RelayCommand<DependencyObject> saveLogin;

         public ICommand SaveLogin
         {
             get
             {
                 return saveLogin ??
                  (
                      saveLogin = new RelayCommand<DependencyObject>
                      (
                          (parameter) =>
                          {
                              if (SaveLoginIsChecked)
                              {
                                  var passwordBox = parameter as PasswordBox;
                                  var password = passwordBox.Password;

                                  Login.Instance.IDBoard = IDNumber;
                                  Login.Instance.Password = password;

                                  string resultObj = JsonConvert.SerializeObject(Login.Instance);

                                  CreateSave(resultObj);

                              }
                              else
                              {

                              }
                          }
                      )
                  );
             }
             //set {
             //    if (saveLogin != value)
             //    {
             //        saveLogin = value;
             //        RaisePropertyChanged(() => SaveLogin);
             //    }
             //}
         }


         private async Task CreateSave(String result)
         {
             // Get the text data from the textbox. 
             byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(result.ToCharArray());

             // Get the local folder.
             StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

             // Create a new folder name DataFolder.
             var dataFolder = await local.CreateFolderAsync("IDBoard",
             CreationCollisionOption.OpenIfExists);

             // Create a new file named DataFile.txt.
             var file = await dataFolder.CreateFileAsync("Save.txt",
             CreationCollisionOption.ReplaceExisting);

             // Write the data from the textbox.
             using (var s = await file.OpenStreamForWriteAsync())
             {
                 s.Write(fileBytes, 0, fileBytes.Length);
             }
         }

         private async void ReadSave(DependencyObject parameter)
         {
             StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

             if (local != null)
             {
                 // Get the DataFolder folder.
                 var dataFolder = await local.GetFolderAsync("IDBoard");

                 // Get the file.
                 var file = await dataFolder.OpenStreamForReadAsync("Save.txt");
                 if (file != null)
                 {
                     SaveLoginIsChecked = true;
                     String result;
                     // Read the data.
                     using (StreamReader streamReader = new StreamReader(file))
                     {
                         result = streamReader.ReadToEnd();
                     }

                     var obj = JObject.Parse(result);
                     IDNumber = (String)obj["IDBoard"];
                     var passwordBox = parameter as PasswordBox;
                     passwordBox.Password = (String)obj["Password"];


                 }


             }
         }

         public ICommand getSave { get; set; }
        public MainViewModel()
        {
            ConnexionCommand = new RelayCommand<DependencyObject>(Connexion);
            getSave = new RelayCommand<DependencyObject>(ReadSave);

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