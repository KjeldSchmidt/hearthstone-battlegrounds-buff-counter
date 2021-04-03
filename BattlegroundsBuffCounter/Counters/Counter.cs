using Hearthstone_Deck_Tracker.Hearthstone;

namespace BattlegroundsBuffCounter.Counters
{
    public abstract class Counter
    {
        protected int CurrentCount;
        protected GameV2 Game;
        protected CounterOverlay Overlay;

        protected Counter(CounterOverlay overlay)
        {
            Overlay = overlay;
        }

        internal void GameStart()
        {
            Game = Hearthstone_Deck_Tracker.Core.Game;
        }

        internal void ShowOverlayIfNeeded()
        {
            if (Game == null) return;
            
            if (ShouldOverlayBeShown())
            {
                Overlay.Show();
            }
            else
            {
                Overlay.Hide();
            }
        }

        protected abstract bool ShouldOverlayBeShown();

        public void InMenu()
        {
            Overlay.Hide();
        }

        public void GameEnd()
        {
            Game = null;
        }

    }
}