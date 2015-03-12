using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using idboard_v1.DataModel.CoursesFolder;
using idboard_v1.Helpers;
using Model;
using Model.classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
namespace idboard_v1.ViewModel
{
    public class CalendarViewModel : ViewModelBase
    {
        private DateTime begining;
        private DateTime end;
        private String week;
        private double height;
        private double width;

        public Object CoursesObj { get; set; }
        public String Week
        {
            get { return week; }
            set
            {
                if (value != null)
                {
                    week = value;
                    RaisePropertyChanged(() => Week);
                }
            }
        }

        private ObservableCollection<UIElement> elements;

        public ObservableCollection<UIElement> Elements
        {
            get { return elements; }
            set
            {
                if (elements != value)
                {
                    elements = value;
                    RaisePropertyChanged(() => Elements);
                }
            }
        }
        public InfoViewModel Info { get; set; }


         private RelayCommand<String> changeWeek;

         public ICommand ChangeWeek
         {
             get
             {
                 return changeWeek ??
                  (
                      changeWeek = new RelayCommand<String>
                      (
                          async (parameter) =>
                          {

                              if (parameter != "Now")
                              {
                                  Date.GetWeek(begining, new CultureInfo("fr-FR"), out begining, out end, parameter);
                                  Week = begining.Date.ToString("d") + " - " + end.Date.ToString("d");
                              }
                              else
                              {
                                  Date.GetWeek(DateTime.Today, new CultureInfo("fr-FR"), out begining, out end, parameter);
                                  Week = begining.Date.ToString("d") + " - " + end.Date.ToString("d");
                              }
                           

                              Courses courses = new Courses("http://idboard.net/idws/api/", "Courses", begining.ToString("o"), end.ToString("o"));
                              var customObj = new { IDBoard = Login.Instance.IDBoard, Password = Login.Instance.Password, DateStart = courses.DateStart, DateEnd = courses.DateEnd };

                              String rep = await courses.RunAsync(customObj);
                              var obj = JObject.Parse(rep);

                              var result = obj;
                              var resultObj = JsonConvert.DeserializeObject<CoursesInfo>(rep);
                              CoursesObj = resultObj;
                              if (int.Parse(resultObj.Result.ExitCode) != 0)
                              {
                                  MessageDialog erreur = new MessageDialog("Erreur lors de la récupération des offres");
                                  await erreur.ShowAsync();
                              }
                              else
                              {
                                  foreach (Course course in resultObj.Courses)
                                  {
                                      DateTime dateS = Convert.ToDateTime(course.DateStart);
                                      DateTime dateE = Convert.ToDateTime(course.DateEnd);

                                      double marginWidth = (double)(dateS.DayOfWeek);
                                      TimeSpan timeS = (dateS.TimeOfDay);
                                      TimeSpan timeE = (dateE.TimeOfDay);
                                      double buttonHeigh = (double)(timeE.Hours) - (double)(timeS.Hours);

                                      Button btn = new Button();
                                      btn.Height = height * (buttonHeigh+1.4);
                                      btn.Width = width;
                                      Thickness margin = btn.Margin;
                                      btn.BorderThickness = new Thickness(0.0);
                                      margin.Left = width * (marginWidth-1);
                                      margin.Top = height * (buttonHeigh-0.15);
                                      btn.Margin = margin;
                                      btn.Content = course.Name;
                                      btn.Background = new SolidColorBrush(Colors.Gray);
                                      Elements = new ObservableCollection<UIElement>();
                                      Elements.Add(btn);
                                  }


                              }
                          }
                      )
                  );
             }
         }
        
         private RelayCommand<DependencyObject> getRowDef;

         public ICommand GetRowDef
         {
             get
             {
                 return getRowDef ??
                  (
                      getRowDef = new RelayCommand<DependencyObject>
                      (
                      (parameter) =>
                      {
                          var border = parameter as Border;
                          height = border.ActualHeight;
                          width = border.ActualWidth;

                      }
                      )
                  );
             }
         }

         
        public CalendarViewModel(INavigationService navigationService)
        {
            Info = new InfoViewModel(navigationService);
            //Date.GetWeek(DateTime.Today, new CultureInfo("fr-FR"), out begining, out end);
            //Week = begining.Date.ToString("d") + " - " + end.Date.ToString("d");
        }
    }
}
