using System.Linq;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace BattlegroundsBuffCounter.Counters
{
    public class ExecutusCounter : Counter
    {
        internal void PlayerPlayed(Card card)
        {
            if (card.Race != "Elemental" && card.Race != "All" ) return;
            
            CurrentCount++;
            Overlay.Update(CurrentCount);
            ShowOverlayIfNeeded();
        }

        protected override bool ShouldOverlayBeShown()
        {
            if (!Game.IsBattlegroundsMatch) return false;

            var visibleCards = Game.Opponent.Board.Concat(Game.Player.Board).Concat(Game.Player.Hand);
            bool hasExecutus = visibleCards.Any(entity => entity.Card.Name == "Majordomo Executus"); // Card_ID should be "BGS_105", in case name doesn't work out

            return hasExecutus;
        }

        public void ResetCounter(ActivePlayer obj)
        {
            CurrentCount = 1;
            Overlay.Update(CurrentCount);
        }

        public ExecutusCounter(CounterOverlay overlay) : base(overlay)
        {
            
        }
    }
}