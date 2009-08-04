using _3dplayground.Maths;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace MathsPhysicsTests
{
    
    
    /// <summary>
    ///This is a test class for DVector3Test and is intended
    ///to contain all DVector3Test Unit Tests
    ///</summary>
    [TestClass()]
    public class DVector3Test
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Zero
        ///</summary>
        [TestMethod()]
        public void ZeroTest()
        {
            DVector3 actual;
            actual = DVector3.Zero;
            Assert.AreEqual<double>(0D, actual.X);
            Assert.AreEqual<double>(0D, actual.Y);
            Assert.AreEqual<double>(0D, actual.Z);
        }

        /// <summary>
        ///A test for Z
        ///</summary>
        [TestMethod()]
        public void ZTest()
        {
            DVector3 target = new DVector3(); 
            double expected = 10.345000001D; 
            double actual;
            target.Z = expected;
            actual = target.Z;
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for Y
        ///</summary>
        [TestMethod()]
        public void YTest()
        {
            DVector3 target = new DVector3();
            double expected = 0.100000200030D; 
            double actual;
            target.Y = expected;
            actual = target.Y;
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for X
        ///</summary>
        [TestMethod()]
        public void XTest()
        {
            DVector3 target = new DVector3();
            double expected = 0.2324523423D; 
            double actual;
            target.X = expected;
            actual = target.X;
            Assert.AreEqual(expected, actual);
         
        }

        /// <summary>
        ///A test for UnitZ
        ///</summary>
        [TestMethod()]
        public void UnitZTest()
        {
            DVector3 actual;
            actual = DVector3.UnitZ;
            Assert.AreEqual<double>(0D, actual.X);
            Assert.AreEqual<double>(0D, actual.Y);
            Assert.AreEqual<double>(1D, actual.Z);
        }

        /// <summary>
        ///A test for UnitY
        ///</summary>
        [TestMethod()]
        public void UnitYTest()
        {
            DVector3 actual;
            actual = DVector3.UnitY;
            Assert.AreEqual<double>(0D, actual.X);
            Assert.AreEqual<double>(1D, actual.Y);
            Assert.AreEqual<double>(0D, actual.Z);
        }

        /// <summary>
        ///A test for UnitX
        ///</summary>
        [TestMethod()]
        public void UnitXTest()
        {
            DVector3 actual;
            actual = DVector3.UnitX;
            Assert.AreEqual<double>(1D, actual.X);
            Assert.AreEqual<double>(0D, actual.Y);
            Assert.AreEqual<double>(0D, actual.Z);
        }

        /// <summary>
        ///A test for ToVector3
        ///</summary>
        [TestMethod()]
        public void ToVector3Test()
        {
            DVector3 target = new DVector3(1,2,3); 
            Vector3 expected = new Vector3(1,2,3); 
            Vector3 actual;
            actual = target.ToVector3();
            Assert.AreEqual(expected, actual);
           
        }



        /// <summary>
        ///A test for Subtract
        ///</summary>
        [TestMethod()]
        public void SubtractTest()
        {
            DVector3 SubtractFrom = new DVector3(1,1,1);
            DVector3 SubtractFromExpected = SubtractFrom;
            DVector3 ValueToSubtract = new DVector3(5,5,5);
            DVector3 ValueToSubtractExpected = ValueToSubtract;
            DVector3 expected = new DVector3(-4,-4,-4); 
            DVector3 actual;
            actual = DVector3.Subtract(ref SubtractFrom, ref ValueToSubtract);
            Assert.AreEqual(SubtractFromExpected, SubtractFrom);
            Assert.AreEqual(ValueToSubtractExpected, ValueToSubtract);
            Assert.AreEqual(expected, actual);
           
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest()
        {
            DVector3 SubtractFrom = new DVector3(5,3.4,2);
            DVector3 ValueToSubtract = new DVector3(1,2.4,1);
            DVector3 expected = new DVector3(4,1,1); 
            DVector3 actual;
            actual = (SubtractFrom - ValueToSubtract);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest1()
        {
            DVector3 v1 = new DVector3(2,5,9);
            double v2 = 2.5D;
            DVector3 expected = new DVector3(5,12.5,22.5); 
            DVector3 actual;
            actual = (v1 * v2);
            Assert.AreEqual(expected, actual);
          
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest()
        {
            double v2 = -3F;
            DVector3 v1 = new DVector3(3,3,3); 
            DVector3 expected = new DVector3(-9,-9,-9); 
            DVector3 actual;
            actual = (v2 * v1);
            Assert.AreEqual(expected, actual);
          
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest()
        {
            DVector3 v1 = new DVector3(0.000000001, 1, 2);
            DVector3 v2 = new DVector3(0.000000001, 1, 2);
            bool expected = false;
            bool actual;
            actual = (v1 != v2);
            Assert.AreEqual(expected, actual);

            v1 = new DVector3(1, 1, 1);
            actual = (v1 != v2);
            expected = true;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest()
        {
            DVector3 v1 = new DVector3(0.000000001, 1, 2);
            DVector3 v2 = new DVector3(0.000000001, 1, 2);
            bool expected = true ;
            bool actual;
            actual = (v1 == v2);
            Assert.AreEqual(expected, actual);

            v1 = new DVector3(1, 1, 1);
            actual = (v1 == v2);
            expected = false ;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest()
        {
            DVector3 v1 = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 v2 = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 expected = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 actual;
            actual = (v1 + v2);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Length
        ///</summary>
        [TestMethod()]
        public void LengthTest()
        {
            DVector3 target = new DVector3(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = target.Length();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            DVector3 target = new DVector3(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest1()
        {
            DVector3 target = new DVector3(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            DVector3 target = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 other = new DVector3(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            DVector3 V1 = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 V1Expected = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 V2 = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 V2Expected = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 expected = new DVector3(); // TODO: Initialize to an appropriate value
            DVector3 actual;
            actual = DVector3.Add(ref V1, ref V2);
            Assert.AreEqual(V1Expected, V1);
            Assert.AreEqual(V2Expected, V2);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DVector3 Constructor
        ///</summary>
        [TestMethod()]
        public void DVector3ConstructorTest()
        {
            double x = 0F; // TODO: Initialize to an appropriate value
            double y = 0F; // TODO: Initialize to an appropriate value
            double z = 0F; // TODO: Initialize to an appropriate value
            DVector3 target = new DVector3(x, y, z);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
