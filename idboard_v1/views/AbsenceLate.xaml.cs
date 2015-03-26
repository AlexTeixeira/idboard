using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace idboard_v1.views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AbsenceLate : Page
    {
        public AbsenceLate()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                e.Handled = true;
                MessageDialog dlg = new MessageDialog("Êtes-vous sur de vouloir vous déconnectez?", "Warning");
                dlg.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandHandler1)));
                dlg.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandHandler1)));




                await dlg.ShowAsync();
            }
        }

        private void CommandHandler1(IUICommand command)
        {
            var label = command.Label;
            switch (label)
            {
                case "Yes":
                    {
                        this.Frame.Navigate(typeof(MainPage));
                        break;
                    }
                case "No":
                    {
                        break;
                    }

            }

        }
    }
}
