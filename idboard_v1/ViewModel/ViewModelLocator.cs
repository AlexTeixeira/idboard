/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:idboard_v1"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace idboard_v1.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            if (!SimpleIoc.Default.IsRegistered<IDialogService>())
                SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<InfoViewModel>();
            SimpleIoc.Default.Register<CalendarViewModel>();
            SimpleIoc.Default.Register<OfferViewModel>();
            SimpleIoc.Default.Register<MessageViewModel>();
            SimpleIoc.Default.Register<AbsenceLateViewModel>();


        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public InfoViewModel Info
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InfoViewModel>();
            }
        }

        public CalendarViewModel Calendar
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CalendarViewModel>();
            }
        }

        public OfferViewModel Offer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OfferViewModel>();
            }
        }

        public MessageViewModel Message
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MessageViewModel>();
            }
        }

        public AbsenceLateViewModel AbsenceLate
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AbsenceLateViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("Calendar", typeof(views.Calendar));
            navigationService.Configure("Offers", typeof(views.Offer));
            navigationService.Configure("Messages", typeof(views.Message));
            navigationService.Configure("AbsenceLate", typeof(views.AbsenceLate));


            return navigationService;
        }
    }
}