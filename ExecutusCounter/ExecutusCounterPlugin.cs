using System;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace ExecutusCounter
{
    public class ExecutusCounterPlugin : IPlugin 
    {
        public void OnLoad()
        {
            var counter = new ExecutusCounter();
            GameEvents.OnGameStart.Add(counter.GameStart);
            GameEvents.OnPlayerPlay.Add(counter.PlayerPlayed);
            GameEvents.OnTurnStart.Add(counter.ResetCounter);
        }

        public void OnUnload()
        {
            
        }

        public void OnButtonPress()
        {
            
        }

        public void OnUpdate()
        {
            
        }

        public string Name => "Executus Elementals Counter";
        public string Description =>
            "Adds a counter to show how many elementals you've played this turn, if Majodormo Executus is in the tavern, your hand or your board";

        public string ButtonText => "Settings";
        public string Author => "Kjeld Schmidt";
        public Version Version => new Version("0.0.0.3");
        public MenuItem MenuItem => null;
    }
}