namespace PortTunnel_forWindowsXP_1_NUnit
{
    [Parallelizable( ParallelScope.Self )]
    [TestFixture]
    public class Tests //: PageTest
    {
        PortTunnel_forWindowsXP_1.ControllerClass controllerClass = new PortTunnel_forWindowsXP_1.ControllerClass();

        [Test]
        public void test1_TEST_METHOD_1()
        {
            int result = controllerClass.TEST_METHOD_1(1, 3);

            Assert.That( result, Is.EqualTo( 4 ) );
            return;
        }
        [Test]
        public void test2_TEST_METHOD_1()
        {
            var result = controllerClass.TEST_METHOD_1(2, 3);

            Assert.That( result, Is.EqualTo( 5 ) );
            return;
        }
    }
}
