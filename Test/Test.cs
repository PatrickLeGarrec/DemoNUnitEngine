using System.Diagnostics;

namespace Test
{
    public class Test
    {
        [Test]
        public void HelloCharlie()
        {
            var workSpaceDir = AppDomain.CurrentDomain.BaseDirectory.Replace(@"Test\bin\Debug\net8.0", @"WorkSpace");

            File.Copy("Test.dll", workSpaceDir + "Test.dll", true);

            Assert.Pass();
        }
    }
}