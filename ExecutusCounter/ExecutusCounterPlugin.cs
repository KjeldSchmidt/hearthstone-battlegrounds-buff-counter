using System;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace ExecutusCounter
{
    public class ExecutusCounterPlugin : IPlugin
    {
        private ElementalCounterOverlay _counterOverlay;
        public void OnLoad()
        {
            _counterOverlay = new ElementalCounterOverlay();
            Core.OverlayCanvas.Children.Add(_counterOverlay);
            var counter = new ExecutusCounter(_counterOverlay);
            GameEvents.OnGameStart.Add(counter.GameStart);
            GameEvents.OnPlayerPlay.Add(counter.PlayerPlayed);
            GameEvents.OnTurnStart.Add(counter.ResetCounter);
            GameEvents.OnInMenu.Add(counter.InMenu);
        }

        public void OnUnload()
        {
            Core.OverlayCanvas.Children.Remove(_counterOverlay);
        }

        public void OnButtonPress()
        {
            
        }

        public void OnUpdate()
        {
            
        }

        public string Name => "Executus Elementals Counter";
        public string Description =>
            "Adds a counter to show how many elementals you've played this turn if Majodormo Executus is in the tavern, your hand or on your board";

        public string ButtonText => "Settings";
        public string Author => "Kjeld Schmidt";
        public Version Version => new Version("0.1.1");
        public MenuItem MenuItem => null;
    }
}