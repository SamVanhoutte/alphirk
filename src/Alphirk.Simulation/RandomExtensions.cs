using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Alphirk.Simulation
{
    /// <summary>
    /// Inspired by https://github.com/WallyCZ/PoGo-UWP/blob/master/PokemonGo-UWP/Utils/Extensions/RandomExtensions.cs
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Generates a random string, using the provided format
        /// </summary>
        /// <param name="random">existing random</param>
        /// <param name="format">The method will replace every 0 with a random number and every # with a random character</param>
        /// <param name="casing">Specifies the letter case of the generated characters</param>
        /// <returns>A randomly generated string, using the format</returns>
        public static string GetString(this Random random, string format, LetterCase casing = LetterCase.Uppercase)
        {
            // Based on http://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-centerValue
            // Added logic to specify the format of the random string (# will be random string, 0 will be random numeric, other characters remain)
            var result = new StringBuilder();
            for (var formatIndex = 0; formatIndex < format.Length; formatIndex++)
            {
                switch (format.ToUpper()[formatIndex])
                {
                    case '0': result.Append(GetRandomNumeric(random)); break;
                    case '#': result.Append(GetRandomCharacter(random, casing)); break;
                    default: result.Append(format[formatIndex]); break;
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Generates a boolean, with a given probability (percentage).  
        /// </summary>
        /// <param name="random">existing random</param>
        /// <param name="probabilityPercentage">The probability to get back the configured value.  A probability of 50 would result in an equal distribution of true/falses</param>
        /// <param name="value">The boolean value that is linked with the probability</param>
        /// <returns>A boolean, based on the random probability and the passed value</returns>
        public static bool GetBooleanWithProbability(this Random random, int probabilityPercentage, bool value = true)
        {
            return random.Next(1, 101) <= probabilityPercentage ? value : !value;
        }

        /// <summary>
        /// Generates normally distributed numbers, using the Gaussian distribution algorithm
        /// </summary>
        /// <param name="random">existing random</param>
        /// <param name="mean">Mean of the distribution</param>
        /// <param name="stddev">Standard deviation</param>
        /// <returns>A random double value</returns>
        public static double NextGaussian(this Random random, double mean = 0, double stddev = 1)
        {
            var u1 = random.NextDouble();
            var u2 = random.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);

            var randNormal = mean + stddev * randStdNormal;

            return randNormal;
        }

        /// <summary>
        /// Generates normally distributed numbers, spread between the minimum and maximum value, using Gaussian distribution
        /// </summary>
        /// <param name="random"></param>
        /// <param name="maximum"></param>
        /// <param name="mean">Mean of the distribution</param>
        /// <param name="stddev">Standard deviation</param>
        /// <param name="minimum"></param>
        /// <returns>Generated value</returns>
        public static int NextGaussianValue(this Random random, int minimum, int maximum, double mean = 0, double stddev = 1)
        {
            var u1 = random.NextDouble();
            var u2 = random.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);
            var randNormal = mean + stddev * randStdNormal;

            return Math.Abs(Convert.ToInt32(minimum + randNormal * (maximum - minimum)));
        }

        /// <summary>
        /// Generates values from minimum triangular distribution, with the most common/central value specified
        /// </summary>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Triangular_distribution for minimum description of the triangular probability distribution and the algorithm for generating one.
        /// </remarks>
        /// <param name="random">given random</param>
        /// <param name="minimum">Minimum value</param>
        /// <param name="maximum">Maximum maximum value</param>
        /// <param name="centerValue">Mode (most frequent value)</param>
        /// <returns>Generated random value</returns>
        public static int NextTriangularValue(this Random random, int minimum, int maximum, int centerValue)
        {
            var rndValue = random.NextDouble();

            var resultingDouble = rndValue < (centerValue - minimum) / (maximum - minimum)
                ? minimum + Math.Sqrt(rndValue * (maximum - minimum) * (centerValue - minimum))
                : maximum - Math.Sqrt((1 - rndValue) * (maximum - minimum) * (maximum - centerValue));

            return Convert.ToInt32(resultingDouble);
        }

        /// <summary>
        /// Equally likely to return true or false. Uses <see cref="Random.Next()"/>.
        /// </summary>
        /// <returns>Random boolean value</returns>
        public static bool NextBoolean(this Random random)
        {
            return random.Next(2) > 0;
        }

        private static char GetRandomCharacter(Random random, LetterCase casing = LetterCase.Uppercase)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var generatedChar = chars[random.Next(chars.Length)];
            switch (casing)
            {
                case LetterCase.LowerCase:
                    return char.ToLower(generatedChar);
                case LetterCase.Mixed:
                    return random.NextBoolean() ? char.ToLower(generatedChar) : generatedChar;
                default:
                    return generatedChar; // was upper case in the chars list
            }
        }

        private static char GetRandomNumeric(System.Random random)
        {
            const string nums = "0123456789";
            return nums[random.Next(nums.Length)];
        }


    }

    public enum LetterCase
    {
        Uppercase,
        LowerCase,
        Mixed
    }
}