using System;

namespace Alphirk.Simulation
{
    /// <summary>
    /// The SimulatedClock class can be used to simulate time series, based on a configured simulation multiplier and a start time
    /// This way the time will moving faster, which is perfect for simulation scenarios
    /// </summary>
    public static class SimulatedClock
    {
        private static DateTime _physicalStartTime;
        private static DateTime _initialTime;
        private static int _multiplier;

        /// <summary>
        /// Initializes the clock, using the simulation multiplier and the current datetime as start time
        /// </summary>
        /// <param name="simulationMultiplier">Multiplier for the speed of time</param>
        public static void Init(int simulationMultiplier = 1)
        {
            Init(simulationMultiplier, DateTime.Now);
        }

        /// <summary>
        /// Initializes the clock, using the simulation multiplier and the passed datetime as start time
        /// </summary>
        /// <param name="simulationMultiplier">Multiplier for the speed of time</param>
        /// <param name="initialTime">The start time for the clock to start running</param>
        public static void Init(int simulationMultiplier, DateTime initialTime)
        {
            _initialTime = initialTime;
            _physicalStartTime = DateTime.Now;
            _multiplier = simulationMultiplier;
        }

        /// <summary>
        /// Returns the simulated time
        /// </summary>
        public static DateTime Time
        {
            get
            {
                // Calculate timespan since start in milliseconds
                var runningTime = DateTime.Now.Subtract(_physicalStartTime).TotalMilliseconds;
                // Apply simulation multiplier if available
                if (_multiplier > 1)
                {
                    runningTime = runningTime * _multiplier;
                }
                // Return simulated time
                return _initialTime.AddMilliseconds(runningTime);
            }
        }
    }
}
