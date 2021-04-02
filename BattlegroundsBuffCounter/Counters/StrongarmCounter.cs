namespace BattlegroundsBuffCounter.Counters
{
    public class StrongarmCounter : Counter
    {
        public StrongarmCounter(CounterOverlay overlay) : base(overlay)
        {
        }

        protected override bool ShouldOverlayBeShown()
        {
            return true;
        }
    }
}