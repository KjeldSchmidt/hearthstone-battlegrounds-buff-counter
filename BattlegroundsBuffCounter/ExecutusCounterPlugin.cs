using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace BattlegroundsBuffCounter
{
    public class ExecutusCounterPlugin : IPlugin
    {
        private ExecutusCounter _counter;
        private ElementalCounterOverlay _counterOverlay;
        private Config _config;
        
        public void OnLoad()
        {
            _counterOverlay = new ElementalCounterOverlay();
            Core.OverlayCanvas.Children.Add(_counterOverlay);

            // The overlay will not receive mouse events unless it is added to `_clickableElements`.
            // This is not currently supported via the api, so reflection is needed.
            // This was recommended by the developers on the discord.
            (Core.OverlayWindow.GetType()
                    .GetField("_clickableElements", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(Core.OverlayWindow) as List<FrameworkElement>)
                ?.Add(_counterOverlay);
            
            
            _config = Config.Load();
            ApplyConfig();

            _counter = new ExecutusCounter(_counterOverlay);
            
            GameEvents.OnGameStart.Add(_counter.GameStart);
            GameEvents.OnGameEnd.Add(_counter.GameEnd);
            GameEvents.OnPlayerPlay.Add(_counter.PlayerPlayed);
            GameEvents.OnTurnStart.Add(_counter.ResetCounter);
            GameEvents.OnInMenu.Add(_counter.InMenu);
        }

        private void ApplyConfig()
        {
            Canvas.SetTop(_counterOverlay, _config.ExecutusCounterTop);
            Canvas.SetLeft(_counterOverlay, _config.ExecutusCounterLeft);
        }

        public void OnUnload()
        {
            WriteConfig();
            Core.OverlayCanvas.Children.Remove(_counterOverlay);
        }

        private void WriteConfig()
        {
            _config.ExecutusCounterTop = Canvas.GetTop(_counterOverlay);
            _config.ExecutusCounterLeft = Canvas.GetLeft(_counterOverlay);
            _config.Save();
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
        public Version Version => new Version("0.3.7");
        public MenuItem MenuItem => null;
    }
}