using System;
using System.Collections.Generic;
using System.Diagnostics;
using Alphirk.Simulation;
using Xunit;

namespace Alphirk.Tests
{
    public class RandomExtensionsTests
    {
        [Fact]
        public void TestGaussianDistribution()
        {
            var rnd = new Random();

            for (var i = 0; i < 100; i++)
            {
                var val = rnd.NextTriangularValue(10, 30, 12);
                Debug.WriteLine($"New value generated: {val}");
                Assert.True(val >= 10);
                Assert.True(val <= 30);
            }

        }
            
        [Fact]
        public void TestProbabilityBoolean()
        {
            var rnd = new Random();
            var values = new List<bool>();
            var probability = 15;
            for (var i = 0; i < 100; i++)
            {
                values.Add(rnd.GetBooleanWithProbability(probability));
            }

            var trueCount = 0;
            foreach (var b in values)
            {
                if (b) trueCount += 1;
            }
            Assert.True(trueCount < probability * 1.5);
        }
    }
}
