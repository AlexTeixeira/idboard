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
using Windows.Data.Xml.Dom;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
                                  Elements = new ObservableCollection<UIElement>();

                                  int i =0;
                                  foreach (Course course in resultObj.Courses)
                                  {
                                      /*get date start and date end of a course*/
                                      DateTime dateS = Convert.ToDateTime(course.DateStart);
                                      DateTime dateE = Convert.ToDateTime(course.DateEnd);
                                      
                                      /*calculate position of the course*/
                                      double marginWidth = (double)(dateS.DayOfWeek-1);
                                      TimeSpan timeS = (dateS.TimeOfDay);
                                      TimeSpan timeE = (dateE.TimeOfDay);
                                      double trueHeigh = (double)(timeE.Hours) - (double)(timeS.Hours);
                                      double marginTop = (double)(timeS.Hours) - 9.0;

                                      /*add textblock for the title */
                                      TextBlock title = new TextBlock();
                                      title.Text = course.Name + "\n\n" + course.Teacher;
                                      title.Foreground = new SolidColorBrush(Colors.Black);
                                      title.VerticalAlignment = VerticalAlignment.Center;
                                      title.HorizontalAlignment = HorizontalAlignment.Center;
                                      title.TextWrapping = TextWrapping.Wrap;

                                      /*create border for each border*/
                                      Border myBorder = new Border();
                                      myBorder.BorderBrush = new SolidColorBrush(Colors.Black);
                                      myBorder.BorderThickness = new Thickness(2);
                                      myBorder.Background = ColorToBrush(course.BackColor);
                                      myBorder.Opacity = 20;
                                      myBorder.Height = (height * trueHeigh);
                                      myBorder.Width = width;
                                      Thickness margin = myBorder.Margin;
                                      margin.Top = height * marginTop;
                                      margin.Left = width * (marginWidth);
                                      myBorder.Margin = margin;
                                      
                                      myBorder.Child = title;
                                      myBorder.PointerPressed += myBorder_PointerPressed;
                                      Elements.Add(myBorder);
                                      i++;
                                  }


                              }
                          }
                      )
                  );
             }
         }

         private async void myBorder_PointerPressed(object sender,PointerRoutedEventArgs e)
         {
             Border border = (Border)sender;
             TextBlock textB = (TextBlock)border.Child;

             MessageDialog erreur = new MessageDialog(textB.Text);
             await erreur.ShowAsync();
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

         public static Brush ColorToBrush(string color) // color = "#E7E44D"
         {
             color = color.Replace("#", "");
             if (color.Length == 6)
             {
                 return new SolidColorBrush(ColorHelper.FromArgb(255,
                     byte.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                     byte.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                     byte.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)));
             }  
             else
             {
                 return null;
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

         private INavigationService navigationService;


         public void callNavigationService(String view)
         {
             Elements.Clear();
             navigationService.NavigateTo(view);
         }
        public CalendarViewModel(INavigationService navigationService)
        {
            Info = new InfoViewModel(navigationService);
            this.navigationService = navigationService;
            //Date.GetWeek(DateTime.Today, new CultureInfo("fr-FR"), out begining, out end);
            //Week = begining.Date.ToString("d") + " - " + end.Date.ToString("d");
        }
    }
}
