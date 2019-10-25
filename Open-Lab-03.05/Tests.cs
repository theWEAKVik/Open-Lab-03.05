using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace Open_Lab_03._05
{
    [TestFixture]
    public class Tests
    {

        private Comparator comparator;

        private const int RandSeed = 305305305;
        private const int RandWordMinSize = 5;
        private const int RandWordMaxSize = 10;
        private const int RandMatchChance = 2;
        private const int RandTestCasesCount = 96;

        [OneTimeSetUp]
        public void Init() => comparator = new Comparator();

        [TestCase("hello", "hELLo", true)]
        [TestCase("motive", "emotive", false)]
        [TestCase("venom", "VENOM", true)]
        [TestCase("mask", "mAskinG", false)]
        [TestCaseSource(nameof(GetRandom))]
        public void MatchCaseInsensitiveTest(string str1, string str2, bool expectedOutput) =>
            Assert.That(comparator.MatchCaseInsensitive(str1, str2), Is.EqualTo(expectedOutput));

        private static IEnumerable GetRandom()
        {
            var random = new Random(RandSeed);

            for (var i = 0; i < RandTestCasesCount; i++)
            {
                var size = random.Next(RandWordMinSize, RandWordMaxSize + 1);
                var arrs = new []{ new char[size], new char[size] };
                var matches = random.Next(RandMatchChance) == 0;

                for (var j = 0; j < size; j++)
                    arrs[0][j] = (char) random.Next('A', 'Z' + 1);

                if (matches)
                    for (var j = 0; j < size; j++)
                        arrs[1][j] = random.Next(2) == 0 ? char.ToLower(arrs[0][j]) : arrs[0][j];
                else
                    do
                        for (var j = 0; j < size; j++)
                            arrs[1][j] = (char) random.Next('A', 'Z' + 1);
                    while (arrs[0].SequenceEqual(arrs[1]));

                yield return new TestCaseData(new string(arrs[0]), new string(arrs[1]), matches);
            }
        }

    }
}
