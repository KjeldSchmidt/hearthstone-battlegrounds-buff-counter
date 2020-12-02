using System;
using System.Windows;
using System.Windows.Controls;

namespace ExecutusCounter
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ElementalCounterOverlay : UserControl
    {
        public ElementalCounterOverlay()
        {
            InitializeComponent();
            Hide();
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
            ElementalCountLabel.Text = count.ToString();
        }
    }
}
