using Japf.PivotIndicatorDemo.ViewModel;
using Microsoft.Phone.Controls;

namespace Japf.PivotIndicatorDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = new MainPageViewModel();
        }
    }
}