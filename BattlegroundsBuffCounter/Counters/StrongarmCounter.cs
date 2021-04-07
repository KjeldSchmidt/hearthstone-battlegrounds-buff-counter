using System.Linq;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace BattlegroundsBuffCounter.Counters
{
    public class StrongarmCounter : Counter
    {
        public StrongarmCounter(CounterOverlay overlay) : base(overlay)
        {
        }
        
        internal void PlayToHand(Card card)
        {
            if (card.Race != "Pirate" && card.Race != "All" ) return;
            
            CurrentCount++;
            Overlay.Update(CurrentCount);
        }
        
        public void ResetCounter(ActivePlayer obj)
        {
            CurrentCount = 0;
            Overlay.Update(CurrentCount);
        }

        protected override bool ShouldOverlayBeShown()
        {
            if (!Game.IsBattlegroundsMatch) return false;
            
            var visibleCards = Game.Opponent.Board.Concat(Game.Player.Hand);
            bool hasStrongarm = visibleCards.Any(entity => entity.Card.Name == "Southsea Strongarm");

            return hasStrongarm;
        }
    }
}