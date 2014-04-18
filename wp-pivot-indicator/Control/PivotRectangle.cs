using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Japf.PivotIndicatorDemo.Control
{
    /// <summary>
    /// A control which renders a rectangle which can be animated based on a SelectedIndex and an ItemsCount property
    /// </summary>
    public class PivotRectangle : ContentControl
    {
        private readonly Canvas canvas;
        private readonly DoubleAnimation animation;
        private readonly Storyboard storyboard;
        private StackPanel border;
        /*
        public static readonly DependencyProperty IndicatorForegroundProperty = DependencyProperty.Register(
            "IndicatorForegroundColor",
            typeof(Color),
            typeof(PivotRectangle),
            new PropertyMetadata(1, new PropertyChangedCallback(OnPivotPropertyChanged)));
        public Color IndicatorForegroundColor { get; set; }*/

        public static readonly DependencyProperty IndicatorForegroundProperty = DependencyProperty.Register(
            "IndicatorForeground", typeof(Brush),
            typeof(PivotRectangle),
            //, null
            new PropertyMetadata(new PropertyChangedCallback(OnPivotPropertyChanged)) //"Black",new PropertyChangedCallback(
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
            typeof(PivotRectangle),
            //, null
            new PropertyMetadata(new PropertyChangedCallback(OnPivotPropertyChanged)) //"Black",new PropertyChangedCallback(
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
            typeof(PivotRectangle),
            //, null
            new PropertyMetadata(new PropertyChangedCallback(OnPivotPropertyChanged)) //"Black",new PropertyChangedCallback(
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

        public static readonly DependencyProperty indicateWidthProperty = DependencyProperty.Register(
            "indicateWidth",
            typeof(int),
            typeof(PivotRectangle),
            new PropertyMetadata(1, new PropertyChangedCallback(OnPivotPropertyChanged)));
        public int indicateWidth
        {
            get { return (int)this.GetValue(indicateWidthProperty); }
            set { this.SetValue(indicateWidthProperty, value); }
        }

        public int SelectedIndex
        {
            get { return (int)this.GetValue(SelectedIndexProperty); }
            set { this.SetValue(SelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex", 
            typeof(int), 
            typeof(PivotRectangle), 
            new PropertyMetadata(0, new PropertyChangedCallback(OnSelectedIndexChanged)));

        public int ItemsCount
        {
            get { return (int)this.GetValue(ItemsCountProperty); }
            set { this.SetValue(ItemsCountProperty, value); }
        }

        public static readonly DependencyProperty ItemsCountProperty = DependencyProperty.Register(
            "ItemsCount",
            typeof(int),
            typeof(PivotRectangle),
            new PropertyMetadata(1, new PropertyChangedCallback(OnItemsCountChanged)));

        public PivotRectangle()
        {
            this.canvas = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            this.Content = this.canvas;

            this.animation = new DoubleAnimation
                {
                    Duration = new Duration(TimeSpan.FromSeconds(0.5)), 
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

            this.storyboard = new Storyboard();
            this.storyboard.Children.Add(this.animation);

            this.Loaded += new RoutedEventHandler(this.OnCurrentPivotIndicatorLoaded);

            IndicatorForeground = new SolidColorBrush(Colors.Transparent);
            IndicatorBackgroundFill = new SolidColorBrush(Colors.Transparent);
            IndicatorBackgroundStroke = new SolidColorBrush(Colors.Transparent);
        }

        private void OnCurrentPivotIndicatorLoaded(object sender, RoutedEventArgs e)
        {
            this.RebuildIndicator();
        }

        private void RebuildIndicator()
        {
            int itemNum = (int)this.ItemsCount;
            if (itemNum == 0)
            {
                return;
            }

            double actualWidth = base.ActualWidth;
            double actualHeight = base.ActualHeight;
            
            this.canvas.Width = actualWidth;
            this.canvas.Height = actualHeight;
            this.canvas.Children.Clear();

            var translateTransform = new TranslateTransform();

            for(int i = 0 ; i < itemNum ; i++)
            {
                Ellipse grayEllipse = new Ellipse
                {
                    Width = indicateWidth - 1,//(int)(actualWidth / ((double)this.ItemsCount)),
                    Height = indicateWidth - 1,
                    StrokeThickness = 2,
                    Stroke = IndicatorBackgroundStroke,
                    Fill = IndicatorBackgroundFill,
                };
                int unitWidth = (int)(actualWidth / ((double)this.ItemsCount));
                int marginEllipse = (int)indicateWidth - 1;
                if(marginEllipse < unitWidth)
                {
                    marginEllipse = (unitWidth - marginEllipse)/2;
                }
                double left = i * unitWidth + marginEllipse;
                double top = 0;

                grayEllipse.Margin = new Thickness(left, top, 0, 0);
                canvas.Children.Add(grayEllipse);
            }

            this.border = new StackPanel
                {
                    Width = (int)(actualWidth / ((double)this.ItemsCount)),
                    Height = actualHeight,
                    //BorderBrush = base.Foreground,
                    //BorderThickness = new Thickness(0.0, actualHeight, 0.0, 0.0),
                    RenderTransform = translateTransform,
                    CacheMode = new BitmapCache()
                };

            //SolidColorBrush IndicatorSolidColorBrush = new SolidColorBrush();
            //IndicatorSolidColorBrush.Color = IndicatorForegroundColor;
            Ellipse myEllipse = new Ellipse
            {
                Width = indicateWidth,//(int)(actualWidth / ((double)this.ItemsCount)),
                Height = indicateWidth,
                Fill = IndicatorForeground,
                StrokeThickness = 2,
                Stroke = IndicatorForeground,
            };
            border.Children.Add(myEllipse);
            this.storyboard.Stop();
            
            // animate TranlateTransform.X property
            // not that using TranslateTransform matters, a Canvas.Left animation for example will
            // have less good performance because the compositor thread will not handle it
            Storyboard.SetTarget(this.animation, translateTransform);
            Storyboard.SetTargetProperty(this.animation, new PropertyPath(TranslateTransform.XProperty));

            this.canvas.Children.Add(this.border);
            
            this.ChangeSelectedIndex(0, this.SelectedIndex);
        }

        private void ChangeSelectedIndex(int oldIndex, int newIndex)
        {
            if (this.border != null)
            {
                double width = this.border.Width;
                this.animation.From = width * oldIndex;
                this.animation.To = width * newIndex;
                this.storyboard.Begin();
            }
        }

        private void OnSelectedIndexChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ChangeSelectedIndex((int)e.OldValue, (int)e.NewValue);
        }

        private void OnItemsCountChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RebuildIndicator();
        }

        private void OnPivotPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RebuildIndicator();
        }

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PivotRectangle)d).OnSelectedIndexChanged(e);
        }

        private static void OnItemsCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PivotRectangle)d).OnItemsCountChanged(e);
        }

        private static void OnPivotPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PivotRectangle)d).OnPivotPropertyChanged(e);
        }
    }
}
