using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Japf.PivotIndicatorDemo.Control
{
    public partial class PivotIndicator : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public static readonly DependencyProperty IndicatorForegroundProperty = DependencyProperty.Register(
            "IndicatorForeground", typeof(Brush),
            typeof(PivotIndicator),
            null
        );
        public Brush IndicatorForeground
        {
            get
            {
                return base.GetValue(IndicatorForegroundProperty) as Brush;
            }
            set
            {
                base.SetValue(IndicatorForegroundProperty, value);
            }
        }

        public static readonly DependencyProperty IndicatorBackgroundFillProperty = DependencyProperty.Register(
            "IndicatorBackgroundFill", typeof(Brush),
            typeof(PivotIndicator),
            null
        );
        public Brush IndicatorBackgroundFill
        {
            get
            {
                return base.GetValue(IndicatorBackgroundFillProperty) as Brush;
            }
            set
            {
                base.SetValue(IndicatorBackgroundFillProperty, value);
            }
        }

        public static readonly DependencyProperty IndicatorBackgroundStrokeProperty = DependencyProperty.Register(
            "IndicatorBackgroundStroke", typeof(Brush),
            typeof(PivotIndicator),
            null
        );
        public Brush IndicatorBackgroundStroke
        {
            get
            {
                return base.GetValue(IndicatorBackgroundStrokeProperty) as Brush;
            }
            set
            {
                base.SetValue(IndicatorBackgroundStrokeProperty, value);
            }
        }
           
        private int _indicateWidth = 10;
        public int indicateWidth
        {
            get { return _indicateWidth; }
            set
            {
                if (_indicateWidth != value)
                {
                    _indicateWidth = value;
                    OnPropertyChanged("indicateWidth");
                }
            }
        }

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
            IndicatorForeground = new SolidColorBrush(Colors.Transparent);
            IndicatorBackgroundFill = new SolidColorBrush(Colors.Transparent);
            IndicatorBackgroundStroke = new SolidColorBrush(Colors.Transparent);
        }

        private void OnTap(object sender, System.Windows.Input.GestureEventArgs e)
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
