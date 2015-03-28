using idboard_v1.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=391641

namespace idboard_v1
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int startApp = 0;

        public MainPage()
        {
            this.InitializeComponent();

            SaveCheckbox.IsEnabled = false;
            this.NavigationCacheMode = NavigationCacheMode.Required;

        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d’événement décrivant la manière dont l’utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RegisterBackgroundTask();
            // TODO: préparer la page pour affichage ici.

            // TODO: si votre application comporte plusieurs pages, assurez-vous que vous
            // gérez le bouton Retour physique en vous inscrivant à l’événement
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si vous utilisez le NavigationHelper fourni par certains modèles,
            // cet événement est géré automatiquement.
        }


        private async void RegisterBackgroundTask()
        {
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == taskName)
                    {
                        task.Value.Unregister(true);
                    }
                }

                BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = taskName;
                taskBuilder.TaskEntryPoint = taskEntryPoint;
                taskBuilder.SetTrigger(new TimeTrigger(1440, false));
                var registration= taskBuilder.Register();
            }
        }

        private const string taskName = "BackgroundMessage";
        private const string taskEntryPoint = "BackgroundTask.BackgroundMessage";


        private void box_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(box_password.Password) && !String.IsNullOrEmpty(box_password.Password) && this.startApp >= 2)
            {
                SaveCheckbox.IsChecked = false;
                SaveCheckbox.IsEnabled = true;

            }
            this.startApp++;
        }

        private void box_idboard_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(box_password.Password) &&  !String.IsNullOrEmpty(box_password.Password) && this.startApp >= 2)
            {
                SaveCheckbox.IsChecked = false;
                SaveCheckbox.IsEnabled = true;

            }
            this.startApp++;
        }


 
    }
}
