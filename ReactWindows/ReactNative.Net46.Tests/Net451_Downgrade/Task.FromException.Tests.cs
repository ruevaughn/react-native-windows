using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ReactNative.Net46.Tests.Net451_Downgrade
{
    [TestFixture]
    class Task
    {
        [Test]
        public void FromException()
        {
            var k = Net45.Task.FromException<string>(new ArgumentNullException("test 2"));

            Assert.AreEqual(k.Status, TaskStatus.Faulted);
        }

        [Test]
        public void CompletedTask()
        {
            var k = Net45.Task.CompletedTask;
            Assert.IsTrue(k.IsCompleted);
        }
    }
}
