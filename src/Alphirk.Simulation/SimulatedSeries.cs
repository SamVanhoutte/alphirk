using System;

namespace Alphirk.Simulation
{
    /// <summary>
    /// A class to generate a simulated series of values, that evolve in a triangular distribution, with maximum step size.
    /// </summary>
    public class SimulatedSeries
    {
        private readonly double minValue;
        private readonly double maxValue;
        private readonly double maxStepSize;
        public double CurrentValue;
        private Random random;

        /// <summary>
        /// Initiatizes a new random series generator with the specified parameters.
        /// </summary>
        /// <param name="minValue">The minimum value for values in this series</param>
        /// <param name="maxValue">The maximum value for values in this series</param>
        /// <param name="maxStepSize">The maximum step size between each subsequent value</param>
        /// <param name="initialValue">An optional value to initialize the first value in the series</param>
        public SimulatedSeries(double minValue, double maxValue, double maxStepSize, double? initialValue = null)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.maxStepSize = maxStepSize;
            random = new Random();
            CurrentValue = initialValue ?? random.NextTriangularValue(minValue, maxValue, (maxValue - minValue) / 2);
        }

        /// <summary>
        /// Generates a new value in the series, based on the current value, using a triangular distribution.
        /// </summary>
        /// <returns>The generated and current value</returns>
        public double NextValue()
        {
            
            CurrentValue = random.NextTriangularValue(
                Math.Max(minValue, CurrentValue-maxStepSize), 
                Math.Min(maxValue, CurrentValue+maxStepSize), 
                CurrentValue);
            return CurrentValue;
        }

        /// <summary>
        /// Generates a series of values, of length <paramref name="count"/>, starting on the current value and the triangular distribution.
        /// </summary>
        /// <param name="count">The number of values to generate</param>
        /// <returns>The resulting values in an array</returns>
        public double[] GenerateSeries(int count)
        {
            var result = new double[count];
            for (var loop = 0; loop < count; loop++)
            {
                result[loop] = NextValue();
            }

            return result;
        }

    }
}