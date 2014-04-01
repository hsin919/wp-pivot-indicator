using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;

namespace Japf.PivotIndicatorDemo.Control
{
    public partial class PivotIndicator : UserControl
    {
        public Pivot Pivot
        {
            get { return (Pivot)GetValue(PivotProperty); }
            set { SetValue(PivotProperty, value); }
        }

        public static readonly DependencyProperty PivotProperty = DependencyProperty.Register(
            "Pivot", 
            typeof(Pivot), 
            typeof(PivotIndicator), 
            new PropertyMetadata(null));

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            "HeaderTemplate", 
            typeof(DataTemplate), 
            typeof(PivotIndicator), 
            new PropertyMetadata(null));

        public PivotIndicator()
        {
            this.InitializeComponent();
        }

        private void OnTap(object sender, GestureEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement && this.Pivot != null)
            {
                // find the index in the list
                int index = this.itemscontrol.Items.IndexOf(((FrameworkElement) e.OriginalSource).DataContext);

                if (index >= 0)
                    this.Pivot.SelectedIndex = index;
            }
        }
    }
}
