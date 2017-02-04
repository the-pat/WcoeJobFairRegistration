using System.Windows;
using System.Windows.Controls;

namespace WcoeJobFairRegistration.Controls
{
    /// <summary>
    /// Custom TextBox that can gain focus through a binding
    /// </summary>
    public class FocusTextBox : TextBox
    {
        public static readonly DependencyProperty ShouldFocusProperty =
            DependencyProperty.Register(nameof(ShouldFocus), typeof(bool), typeof(FocusTextBox),
                new PropertyMetadata(false, OnIsFocusedPropertyChanged));

        public bool ShouldFocus
        {
            get { return (bool)GetValue(ShouldFocusProperty); }
            set { SetValue(ShouldFocusProperty, value); }
        }

        private static void OnIsFocusedPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if((bool)e.NewValue)
            {
                var textBox = obj as FocusTextBox;
                textBox?.Focus();
            }
        }
    }
}