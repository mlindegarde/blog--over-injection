using System;
using System.Diagnostics;

namespace FullBoar.Examples.OverInjection.FinalDemo.Messaging.Broker
{
    internal sealed class Subscription
    {
        #region Member Variables
        private const long TicksMultiplier = 1000 * TimeSpan.TicksPerMillisecond;
        private readonly long _throttleByTicks;
        private double? _lastHandleTimestamp;
        #endregion

        #region Properties
        internal Guid Token { get; }
        internal Type Type { get; }
        private object Handler { get; set; }
        #endregion

        #region Constructor
        internal Subscription(Type type, Guid token, TimeSpan throttleBy, object handler)
        {
            Type = type;
            Token = token;
            Handler = handler;
            _throttleByTicks = throttleBy.Ticks;
        }
        #endregion

        #region Utility Methods
        internal void Handle<T>(T message)
        {
            if (!CanHandle()) { return; }

            var handler = Handler as Action<T>;
            // ReSharper disable once PossibleNullReferenceException
            handler(message);
        }

        internal bool CanHandle()
        {
            if (_throttleByTicks == 0)
                return true;

            if (_lastHandleTimestamp == null)
            {
                _lastHandleTimestamp = Stopwatch.GetTimestamp();
                return true;
            }

            var now = Stopwatch.GetTimestamp();
            var durationInTicks = (now - _lastHandleTimestamp) / Stopwatch.Frequency * TicksMultiplier;

            if (!(durationInTicks >= _throttleByTicks))
                return false;

            _lastHandleTimestamp = now;
            return true;
        }

        internal void ClearHandler()
        {
            Handler = null;
        }
        #endregion
    }
}
