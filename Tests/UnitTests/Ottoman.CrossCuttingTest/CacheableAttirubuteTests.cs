namespace Ottoman.CrossCuttingTest
{
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using MemoryCache;

    [TestFixture(Category = "MemoryCache")]
    public class CacheableAttirubuteTests
    {
        private Dictionary<byte, string> _nameList;


        [SetUp]
        public void Init()
        {
            this._nameList = new Dictionary<byte, string>()
                                 {
                                     { 1, "John" },
                                     { 2, "James" },
                                     { 3, "Jane" },
                                     { 4, "Jack" },
                                     { 5, "Jason" },
                                     { 6, "Jamie" },
                                     { 7, "Jennifer" }
                                 };
        }

        [Cacheable]
        private string GetNameFromDic(byte key)
        {
            return this._nameList.FirstOrDefault(x => x.Key == key).Value;
        }

        [TestCase(1)]
        public void GetNameFromDicTest(byte key)
        {
            this.GetNameFromDic(key);

            this._nameList.Clear();

            string result = this.GetNameFromDic(key);

            Assert.AreEqual("John", result);
        }
    }
}