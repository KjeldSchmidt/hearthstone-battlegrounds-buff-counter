using System;
using System.Linq;
using Hearthstone_Deck_Tracker.Controls;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace BattlegroundsBuffCounter.Counters
{
    public class CleefCounter : Counter
    {
        public CleefCounter(CounterOverlay overlay) : base(overlay)
        {
        }

        internal void PlayToHand(Card card)
        {
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
            return Game.Player.PlayerEntities.Any(entity => entity.CardId == "TB_BaconShop_HP_001");
        }
    }
}