using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.Mobile
{
    public class MainPage : ContentPage
    {
        public MainPage(App app)
        {
            try
            {
                Title = "Select Sample";
                var list = new ListView();
                list.ItemsSource = new List<string> {
                "Notes",
            };
                list.ItemTapped += (object sender, ItemTappedEventArgs e) =>
                {
                    if ((string)e.Item == "Notes")
                        app.LoadNotes();
                };
                this.Content = list;
            }catch(Exception ex)
            {

            }
        }
         
    }
}
