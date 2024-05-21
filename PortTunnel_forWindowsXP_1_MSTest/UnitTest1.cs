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
            var result = controllerClass.TEST_METHOD_1(1, 3) == 4;

            Assert.IsTrue(result, "MSTest MUST BE 4");
        }
        [TestMethod]
        public void test2_TEST_METHOD_1()
        {
            var result = controllerClass.TEST_METHOD_1(1, 3) == 4;
            
            Assert.IsTrue(result, "MSTest MUST BE 5");
        }
    }
}
