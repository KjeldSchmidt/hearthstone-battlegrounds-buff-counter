using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace ExecutusCounter
{
    public class ExecutusCounterPlugin : IPlugin
    {
        private ExecutusCounter _counter;
        private ElementalCounterOverlay _counterOverlay;
        public void OnLoad()
        {
            _counterOverlay = new ElementalCounterOverlay();
            Core.OverlayCanvas.Children.Add(_counterOverlay);

            (Core.OverlayWindow.GetType()
                .GetField("_clickableElements", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(Core.OverlayWindow) as List<FrameworkElement>)
                ?.Add(_counterOverlay);

            _counter = new ExecutusCounter(_counterOverlay);
            GameEvents.OnGameStart.Add(_counter.GameStart);
            GameEvents.OnGameEnd.Add(_counter.GameEnd);
            GameEvents.OnPlayerPlay.Add(_counter.PlayerPlayed);
            GameEvents.OnTurnStart.Add(_counter.ResetCounter);
            GameEvents.OnInMenu.Add(_counter.InMenu);
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
            _counter.ShowOverlayIfNeeded();
        }

        public string Name => "Executus Elementals Counter";
        public string Description =>
            "Adds a counter to show how many elementals you've played this turn when Majodormo Executus is in the tavern, your hand or on your board";

        public string ButtonText => "Settings";
        public string Author => "Kjeld Schmidt";
        public Version Version => new Version("0.3.0");
        public MenuItem MenuItem => null;
    }
}