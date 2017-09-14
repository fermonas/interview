using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TTCDictionary.UnitTests
{
    [TestFixture]
    public class LanguageDictionaryTest
    {
        private LanguageDictionary SUT;

        private Dictionary<string, List<string>> list;

        [SetUp]
        public void Setup()
        {
            list = new Dictionary<string, List<string>>();

            SUT = new LanguageDictionary(list);
        }

        [Test]
        public void When_adding_a_word_which_does_not_exist_should_return_true()
        {
            // Arrange.
            var word = "test";
            var lang = "English";

            // Act.
            var result = this.SUT.Add(lang, word);

            // Assert.
            Assert.IsTrue(result);

            var listCheck = this.list.FirstOrDefault(i => i.Key == lang && i.Value.IndexOf(word) >= 0);

            Assert.IsTrue(listCheck.Key == lang);
            Assert.IsTrue(listCheck.Value.IndexOf(word) >= 0);
        }

        [Test]
        public void When_adding_a_word_which_does_exist_should_return_false()
        {
            // Arrange.
            var word = "test";
            this.SUT.Add("English", word);

            // Act.
            var result = this.SUT.Add("English", word);

            // Assert.
            Assert.IsFalse(result);
        }

        [Test]
        public void When_adding_a_word_which_does_exist_but_in_a_different_language_should_return_true()
        {
            // Arrange.
            var word = "test";
            this.SUT.Add("English", word);

            // Act.
            var result = this.SUT.Add("German", word);

            // Assert.
            Assert.IsTrue(result);
        }

        [Test]
        public void When_checking_a_word_which_does_not_exist_should_return_false()
        {
            // Arrange.
            var word = "test";

            // Act.
            var result = this.SUT.Check("English", word);

            // Assert.
            Assert.IsFalse(result);
        }

        [Test]
        public void When_checking_a_word_which_does_exist_should_return_true()
        {
            // Arrange.
            var word = "test";
            this.SUT.Add("English", word);

            // Act.
            var result = this.SUT.Check("English", word);

            // Assert.
            Assert.IsTrue(result);
        }

        [Test]
        public void When_searching_all_word_that_starts_with_hi()
        {
            // Arrange.
            List<string> wordsListGood = new List<string>();
            List<string> wordsList     = new List<string>();
            wordsListGood.Add("hi" );
            wordsListGood.Add("hi1");
            wordsListGood.Add("hi2");
            wordsListGood.Add("hi3");
            wordsListGood.Add("hi4");
            wordsListGood.Add("hi5");
            wordsListGood.Add("hi6");

            wordsList.Add("hi" );
            wordsList.Add("hi1");
            wordsList.Add("hi2");
            wordsList.Add("hi3");
            wordsList.Add("hi4");
            wordsList.Add("hi5");
            wordsList.Add("hi6");

            wordsList.Add("nothi6");
            wordsList.Add("nothi7");

            foreach(string s in wordsList)
            {
                this.SUT.Add("English", s);
            }

            // Act.
            IEnumerable<string> result = this.SUT.Search("hi");

            wordsListGood.Sort();

            Assert.AreEqual(wordsListGood as IEnumerable<string>, result);
           
        }
    }
}
