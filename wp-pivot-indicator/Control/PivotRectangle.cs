using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
        private Border border;

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
        }

        private void OnCurrentPivotIndicatorLoaded(object sender, RoutedEventArgs e)
        {
            this.RebuildIndicator();
        }

        private void RebuildIndicator()
        {
            double actualWidth = base.ActualWidth;
            double actualHeight = base.ActualHeight;
            
            this.canvas.Width = actualWidth;
            this.canvas.Height = actualHeight;
            this.canvas.Children.Clear();

            var translateTransform = new TranslateTransform();
            this.border = new Border
                {
                    Width = (int)(actualWidth / ((double)this.ItemsCount)),
                    Height = actualHeight,
                    BorderBrush = base.Foreground,
                    BorderThickness = new Thickness(0.0, actualHeight, 0.0, 0.0),
                    RenderTransform = translateTransform,
                    CacheMode = new BitmapCache()
                };

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

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PivotRectangle)d).OnSelectedIndexChanged(e);
        }

        private static void OnItemsCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PivotRectangle)d).OnItemsCountChanged(e);
        }
    }
}
