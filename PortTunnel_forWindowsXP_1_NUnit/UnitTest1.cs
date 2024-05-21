namespace PortTunnel_forWindowsXP_1_Test
{
    [Parallelizable( ParallelScope.Self )]
    [TestFixture]
    public class Tests //: PageTest
    {
        PortTunnel_forWindowsXP_1.ControllerClass controllerClass = new PortTunnel_forWindowsXP_1.ControllerClass();

        [Test]
        public void test1_TEST_METHOD_1()
        {
            var result = controllerClass.TEST_METHOD_1(1, 3) == 4;

            Assert.That(result, Is.False, "Должно быть 4");
        }
        [Test]
        public void test2_TEST_METHOD_1()
        {
            var result = controllerClass.TEST_METHOD_1(1, 3) == 5;
            
            Assert.That(result, Is.False, "Должно быть 5");
        }
    }
}
