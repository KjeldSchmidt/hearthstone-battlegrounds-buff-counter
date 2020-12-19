using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core = Hearthstone_Deck_Tracker.API.Core;

namespace ExecutusCounter
{
    public partial class ElementalCounterOverlay
    {
        private bool _dragInProgress;
        private Point _offset;
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _dragInProgress = true;
            _offset = e.GetPosition(this);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragInProgress) return;
            Point absoluteMouse = e.GetPosition(Core.OverlayCanvas);
            Canvas.SetTop(this, absoluteMouse.Y - _offset.Y);
            Canvas.SetLeft(this, absoluteMouse.X - _offset.X);
        }


        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _dragInProgress = false;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            _dragInProgress = false;
        }
    }
}