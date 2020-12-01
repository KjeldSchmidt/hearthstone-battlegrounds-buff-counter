using System.Diagnostics;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace ExecutusCounter
{
    public class ExecutusCounter
    {
        private int counter;
        private GameV2 _game;
        public ExecutusCounter() { }

        internal void GameStart()
        {
            counter = 0;
            _game = Hearthstone_Deck_Tracker.Core.Game;
            Log.Info("Game started");
        }

        internal void PlayerPlayed(Card card)
        {
            if (card.Race != "Elemental") return;

            counter++;
            Log.Info($"Played {counter} elementals this turn so far!");
        }

        public void ResetCounter(ActivePlayer obj)
        {
            counter = 0;
        }
    }
}