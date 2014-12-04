using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FluentAssertions;

namespace LinqToArray
{
    [TestClass]
    public class LinqToArrayTests
    {
        [TestMethod]
        public void Reverse()
        {
            var array = Enumerable.Range(0, 8).Select(i => i.ToString()).ToArray();

            LinqArrayExtensions.Reverse(array).Should().Equal(Enumerable.Reverse(array));
        }

        [TestMethod]
        public void Skip()
        {
            var array = Enumerable.Range(0, 8).Select(i => i.ToString()).ToArray();

            LinqArrayExtensions.Skip(array, 2).Should().Equal(Enumerable.Skip(array, 2));
        }

        [TestMethod]
        public void ReverseSkip()
        {
            var array = Enumerable.Range(0, 8).Select(i => i.ToString()).ToArray();

            LinqArrayExtensions.Reverse(array)
                .Skip(2)
                .Should().Equal(Enumerable.Reverse(array).Skip(2));
        }

        [TestMethod]
        public void SkipReverse()
        {
            var array = Enumerable.Range(0, 8).Select(i => i.ToString()).ToArray();

            LinqArrayExtensions.Skip(array, 2)
                .Reverse()
                .Should().Equal(Enumerable.Skip(array, 2).Reverse());
        }

        [TestMethod]
        public void TestSkip()
        {
            var array = Enumerable.Range(0, 8).Select(i => i.ToString()).ToArray();

            Enumerable.Skip(array, 9).Any();
        }
    }
}
