//using 

namespace PortTunnel_forWindowsXP_1_MSTest
{
    [TestClass]
    public class UnitTest1 //: PageTest
    {
        PortTunnel_forWindowsXP_1.ControllerClass controllerClass = new PortTunnel_forWindowsXP_1.ControllerClass();

        [TestMethod]
        public void test1_TEST_METHOD_1()
        {
            var result = controllerClass.TEST_METHOD_1(1, 3);

            Assert.AreEqual(result, 4);
            return;
        }
        [TestMethod]
        public void test2_TEST_METHOD_1()
        {
            var result = controllerClass.TEST_METHOD_1(2, 3);
            
            Assert.AreEqual(result, 5);
            return;
        }
    }
}
