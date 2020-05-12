using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace REM
{
    /// <summary>
    /// Interaktionslogik für InputTextBox.xaml
    /// </summary>
    public partial class InputTextBox : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(Page), new UIPropertyMetadata(TitlePropertyChanged));
        private static void TitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var input = ((InputTextBox)d);

            input.TitleTextBlock.Text = (string)e.NewValue;
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(Page), new UIPropertyMetadata(TextPropertyChanged));
        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        public InputTextBox()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TitleTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            BottomBorder.Background = new SolidColorBrush(Colors.Black);

            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.1)),
                From = 12,
                To = 12.5,
                DecelerationRatio = 0.4f
            };

            TitleTextBlock.BeginAnimation(TextBlock.FontSizeProperty, animation);
        }

        private void TextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TitleTextBlock.Foreground = new SolidColorBrush(Colors.DarkGray);
            BottomBorder.Background = new SolidColorBrush(Colors.Gray);
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.1)),
                From = 12.5,
                To = 12,
                DecelerationRatio = 0.4f
            };

            TitleTextBlock.BeginAnimation(TextBlock.FontSizeProperty, animation);
        }

        public void Unfocus()
        {
            TextBox_LostFocus(null, null);
        }
    }
}

