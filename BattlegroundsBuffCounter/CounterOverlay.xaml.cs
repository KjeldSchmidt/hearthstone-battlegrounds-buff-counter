using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BattlegroundsBuffCounter
{
    public partial class CounterOverlay : UserControl
    {
        
        public CounterOverlay(Uri imageUri)
        {
            InitializeComponent();
            Hide();
            BuffImage.Source = new BitmapImage(imageUri);
        }

        public void Hide()
        {
            Visibility = Visibility.Hidden;
        }

        public void Show()
        {
            Visibility = Visibility.Visible;
        }

        public void Update(int count)
        {
            CountLabel.Text = count.ToString();
        }
    }
}
