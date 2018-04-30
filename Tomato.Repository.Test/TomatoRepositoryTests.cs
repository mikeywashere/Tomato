using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tomato.Core;

namespace Tomato.Repository.Test
{
    [TestClass]
    public class TomatoRepositoryTests
    {
        [TestMethod]
        public void CanPutAndGet()
        {
            InMemoryRepository<Guid, string> memory = new InMemoryRepository<Guid, string>();
            Guid key = Guid.NewGuid();
            memory.Put(key, "item");
            Assert.IsNotNull(memory.Get(key));
        }
    }
}
