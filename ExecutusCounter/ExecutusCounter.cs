using System.Diagnostics;
using System.Linq;
using Hearthstone_Deck_Tracker.API;
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
            _currentCount = 1;
            _game = Hearthstone_Deck_Tracker.Core.Game;
            Log.Info("Game started");
        }

        internal void PlayerPlayed(Card card)
        {
            if (card.Race != "Elemental" && card.Race != "All" ) return;

            _currentCount++;
            _overlay.Update(_currentCount);
            ShowOverlayIfNeeded();
        }

        internal void ShowOverlayIfNeeded()
        {
            if (_game == null) return;
            
            if (ShouldOverlayBeShown())
            {
                _overlay.Show();
            }
            else
            {
                _overlay.Hide();
            }
        }

        private bool ShouldOverlayBeShown()
        {
            if (!_game.IsBattlegroundsMatch) return false;

            var visibleCards = _game.Opponent.Board.Concat(_game.Player.Board).Concat(_game.Player.Hand);
            bool hasExecutus = visibleCards.Any(entity => entity.Card.Name == "Majordomo Executus"); // Card_ID should be "BGS_105", in case name doesn't work out

            return hasExecutus;
        }

        public void ResetCounter(ActivePlayer obj)
        {
            _currentCount = 1;
            _overlay.Update(_currentCount);
        }

        public void InMenu()
        {
            _overlay.Hide();
        }

        public void GameEnd()
        {
            _overlay.Hide();
        }
    }
}