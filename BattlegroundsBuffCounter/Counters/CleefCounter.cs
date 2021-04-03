using System;
using Hearthstone_Deck_Tracker.Controls;
using Hearthstone_Deck_Tracker.Enums;
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
            if (Game == null) return false;
            if (!Game.IsBattlegroundsMatch) return false;
            
            // Todo: Figure out a way to see if the current hero is VanCleef, or better yet, whether the current heropower is relevant (For Finley)
            
            return true;
        }
    }
}