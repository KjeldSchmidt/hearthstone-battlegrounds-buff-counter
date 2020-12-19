using System.Windows;
using System.Windows.Controls;

namespace BattlegroundsBuffCounter
{
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
