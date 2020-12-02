using System.Diagnostics;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace ExecutusCounter
{
    public class ExecutusCounter
    {
        private int _currentCount;
        private GameV2 _game;
        private ElementalCounterOverlay _overlay;

        public ExecutusCounter(ElementalCounterOverlay overlay)
        {
            _overlay = overlay;
        }

        internal void GameStart()
        {
            _currentCount = 0;
            _game = Hearthstone_Deck_Tracker.Core.Game;
            Log.Info("Game started");
            _overlay.Show();
        }

        internal void PlayerPlayed(Card card)
        {
            if (card.Race != "Elemental") return;

            _currentCount++;
            _overlay.Update(_currentCount);
        }

        public void ResetCounter(ActivePlayer obj)
        {
            _currentCount = 0;
        }

        public void InMenu()
        {
            _overlay.Hide();
        }
    }
}