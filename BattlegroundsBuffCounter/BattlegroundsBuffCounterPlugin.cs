﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using BattlegroundsBuffCounter.Counters;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace BattlegroundsBuffCounter
{
    public class ExecutusCounterPlugin : IPlugin
    {
        private ExecutusCounter _executusCounter;
        private CleefCounter _cleefCounter;
        private StrongarmCounter _strongarmCounter;

        private CounterOverlay _elementalsOverlay;
        private CounterOverlay _strongarmOverlay;
        private CounterOverlay _cleefOverlay;
        
        private Config _config;

        public void OnLoad()
        {
            CreateOverlays();

            _config = Config.Load();
            ApplyConfig();

            _executusCounter = new ExecutusCounter(_elementalsOverlay);
            _cleefCounter = new CleefCounter(_cleefOverlay);
            _strongarmCounter = new StrongarmCounter(_strongarmOverlay);

            GameEvents.OnGameStart.Add(_executusCounter.GameStart);
            GameEvents.OnGameEnd.Add(_executusCounter.GameEnd);
            GameEvents.OnPlayerPlay.Add(_executusCounter.PlayerPlayed);
            GameEvents.OnTurnStart.Add(_executusCounter.ResetCounter);
            GameEvents.OnInMenu.Add(_executusCounter.InMenu);

            GameEvents.OnGameStart.Add(_cleefCounter.GameStart);
            GameEvents.OnGameEnd.Add(_cleefCounter.GameEnd);
            GameEvents.OnInMenu.Add(_cleefCounter.InMenu);
            GameEvents.OnTurnStart.Add(_cleefCounter.ResetCounter);
            GameEvents.OnPlayerPlayToHand.Add(_cleefCounter.PlayToHand);
            
            GameEvents.OnGameStart.Add(_strongarmCounter.GameStart);
            GameEvents.OnGameEnd.Add(_strongarmCounter.GameEnd);
            GameEvents.OnPlayerPlayToHand.Add(_strongarmCounter.PlayToHand);
            GameEvents.OnTurnStart.Add(_strongarmCounter.ResetCounter);
            GameEvents.OnInMenu.Add(_strongarmCounter.InMenu);
        }

        private void CreateOverlays()
        {
            _elementalsOverlay = new CounterOverlay(new Uri(@"images\icon-executus.png", UriKind.Relative));
            _strongarmOverlay = new CounterOverlay(new Uri(@"images\icon-strongarm.png", UriKind.Relative));
            _cleefOverlay = new CounterOverlay(new Uri(@"images\icon-van-cleef.png", UriKind.Relative));
            
            ShowAndMakeClickable(_elementalsOverlay);
            ShowAndMakeClickable(_strongarmOverlay);
            ShowAndMakeClickable(_cleefOverlay);
        }

        private void ShowAndMakeClickable(CounterOverlay counterOverlay)
        {
            Core.OverlayCanvas.Children.Add(counterOverlay);
            
            // The overlay will not receive mouse events unless it is added to `_clickableElements`.
            // This is not currently supported via the api, so reflection is needed.
            // This was recommended by the developers on the discord.
            (Core.OverlayWindow.GetType()
                    .GetField("_clickableElements", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(Core.OverlayWindow) as List<FrameworkElement>)
                ?.Add(counterOverlay);   
        }
        
        private void ApplyConfig()
        {
            Canvas.SetTop(_elementalsOverlay, _config.ExecutusCounterTop);
            Canvas.SetLeft(_elementalsOverlay, _config.ExecutusCounterLeft);
            Canvas.SetTop(_cleefOverlay, _config.CleefCounterTop);
            Canvas.SetLeft(_cleefOverlay, _config.CleefCounterLeft);
            Canvas.SetTop(_strongarmOverlay, _config.StrongarmCounterTop);
            Canvas.SetLeft(_strongarmOverlay, _config.StrongarmCounterLeft);
        }

        public void OnUnload()
        {
            WriteConfig();
            Core.OverlayCanvas.Children.Remove(_elementalsOverlay);
        }

        private void WriteConfig()
        {
            _config.ExecutusCounterTop = Canvas.GetTop(_elementalsOverlay);
            _config.ExecutusCounterLeft = Canvas.GetLeft(_elementalsOverlay);
            _config.CleefCounterTop = Canvas.GetTop(_cleefOverlay);
            _config.CleefCounterLeft = Canvas.GetLeft(_cleefOverlay);
            _config.StrongarmCounterTop = Canvas.GetTop(_strongarmOverlay);
            _config.StrongarmCounterLeft = Canvas.GetLeft(_strongarmOverlay);
            _config.Save();
        }

        public void OnButtonPress()
        {
            
        }

        public void OnUpdate()
        {
            _executusCounter.ShowOverlayIfNeeded();
            _cleefCounter.ShowOverlayIfNeeded();
            _strongarmCounter.ShowOverlayIfNeeded();
        }

        public string Name => "Battlegrounds Buff Counter";
        public string Description =>
            "Adds a counter to show how many elementals you've played this turn when Majodormo Executus is in the tavern, your hand or on your board";

        public string ButtonText => "Settings";
        public string Author => "Kjeld Schmidt";
        public Version Version => new Version("1.0.0");
        public MenuItem MenuItem => null;
    }
}