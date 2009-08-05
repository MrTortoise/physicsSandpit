using _3dplayground;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3dplayground.Physics;
using _3dplayground.Maths;

using MathsPhysicsTests.mocks; 

namespace MathsPhysicsTests
{
    
    
    /// <summary>
    ///This is a test class for DisplacementStructureTest and is intended
    ///to contain all DisplacementStructureTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DisplacementStructureTest
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
        ///A test for Velocity
        ///</summary>
        [TestMethod()]
        public void VelocityTest()
        {
            IPhysicsObject obj = new IPhysicsObjectMock();
            DisplacementStructure target = new DisplacementStructure(obj, DVector3.Zero, DVector3.Zero);
            DVector3 expected = new DVector3(3.141233, 23423423, 934534);
            DVector3 actual;
            target.Velocity = expected;
            actual = target.Velocity;
            Assert.AreEqual(expected, actual);
           
        }

        /// <summary>
        ///A test for Position
        ///</summary>
        [TestMethod()]
        public void PositionTest()
        {
            DisplacementStructure target = new DisplacementStructure();
            DVector3 expected = new DVector3(2342.45645, 3453.3453, 1231243);
            DVector3 actual;
            target.Position = expected;
            actual = target.Position;
            Assert.AreEqual(expected, actual);
           
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            IPhysicsObject obj = new IPhysicsObjectMock();
            DisplacementStructure target = new DisplacementStructure(obj,DVector3.Zero,DVector3.Zero ); 
            string actual;
            actual = target.Name;
            Assert.AreEqual<string>("Mock IAmInSpace", actual);
        }

        /// <summary>
        ///A test for Mass
        ///</summary>
        [TestMethod()]
        public void MassTest()
        {
            IPhysicsObject obj = new IPhysicsObjectMock();
            DisplacementStructure target = new DisplacementStructure(obj, DVector3.Zero, DVector3.Zero); 
            double actual;
            actual = target.Mass;
            Assert.AreEqual(1, actual);
        }

        /// <summary>
        ///A test for IAmInSpace
        ///</summary>
        [TestMethod()]
        public void IPhysicsObjectTest()
        {
            IPhysicsObject obj = new IPhysicsObjectMock();
            DisplacementStructure target = new DisplacementStructure(obj, DVector3.Zero, DVector3.Zero); 
            IPhysicsObject actual;
            actual = target.IPhysicsObject ;
            Assert.AreEqual<IPhysicsObject>(obj, actual);
        }

        /// <summary>
        ///A test for DeltaVelocity
        ///</summary>
        [TestMethod()]
        public void DeltaVelocityTest()
        {
            DisplacementStructure target = new DisplacementStructure();
            DVector3 expected = new DVector3(123, 345, 234);
            DVector3 actual;
            target.DeltaVelocity = expected;
            actual = target.DeltaVelocity;
            Assert.AreEqual(expected, actual);
      
        }

        /// <summary>
        ///A test for DeltaPosition
        ///</summary>
        [TestMethod()]
        public void DeltaPositionTest()
        {
            DisplacementStructure target = new DisplacementStructure(); 
            DVector3 expected = new DVector3(123,456,789);
            DVector3 actual;
            target.DeltaPosition = expected;
            actual = target.DeltaPosition;
            Assert.AreEqual(expected, actual);
         
        }

        /// <summary>
        ///A test for ZeroDeltas
        ///</summary>
        [TestMethod()]
        public void ZeroDeltasTest()
        {
            IPhysicsObject source = new IPhysicsObjectMock();
            DisplacementStructure expected = new DisplacementStructure(source,DVector3.UnitX,DVector3.UnitY ); 
            DisplacementStructure actual;
            actual = DisplacementStructure.ZeroDeltas(source);
            Assert.AreEqual(DVector3.Zero , actual.DeltaPosition );
            Assert.AreEqual(DVector3.Zero, actual.DeltaVelocity);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest()
        {
            IPhysicsObject source = new IPhysicsObjectMock();
            DisplacementStructure s1 = new DisplacementStructure(source,DVector3.Zero,DVector3.UnitX,DVector3.Zero,DVector3.UnitY );
            DisplacementStructure s2 = new DisplacementStructure(source, DVector3.Zero, DVector3.UnitX*-1, DVector3.Zero, -1*DVector3.UnitY);
            DisplacementStructure expected = new DisplacementStructure(source, DVector3.Zero, DVector3.Zero, DVector3.Zero, DVector3.Zero);
            DisplacementStructure actual;
            actual = (s1 + s2);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for HasNoDeltas
        ///</summary>
        [TestMethod()]
        public void HasNoDeltasTest()
        {
            DisplacementStructure target = new DisplacementStructure();
            bool expected = false; 
            bool actual;

            target.DeltaVelocity = DVector3.UnitX ;
            actual = target.HasNoDeltas();
            Assert.AreEqual(expected, actual);

            expected = true;
            target.DeltaVelocity = DVector3.Zero;
            actual = target.HasNoDeltas();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            IPhysicsObject obj = new IPhysicsObjectMock();
            DisplacementStructure target = new DisplacementStructure(obj,DVector3.UnitX,DVector3.UnitY,DVector3.UnitZ,DVector3.UnitY );
            DisplacementStructure other = target;



            bool expected = true ; 
            bool actual;
            actual = target.Equals(other);

            Assert.AreEqual(expected, actual);

            expected = false;
            target = new DisplacementStructure(obj, DVector3.Zero, DVector3.Zero, DVector3.Zero, DVector3.Zero);
            actual = target.Equals(other);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            IPhysicsObject obj = new IPhysicsObjectMock();
            DisplacementStructure s1 = new DisplacementStructure(obj,DVector3.Zero,DVector3.Zero,DVector3.Zero,DVector3.Zero); 
            DisplacementStructure s2 = new DisplacementStructure(obj,DVector3.Zero,DVector3.UnitX,DVector3.Zero,DVector3.UnitX );
            DisplacementStructure expected = s2;
            DisplacementStructure actual;
            actual = DisplacementStructure.Add(s1, s2);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for DisplacementStructure Constructor
        ///</summary>
        [TestMethod()]
        public void DisplacementStructureConstructorTest1()
        {
            IPhysicsObject theObject = new IPhysicsObjectMock();
            DVector3 TheOrigionalPosition = new DVector3(123, 432, 456);
            DVector3 TheDeltaPosition = new DVector3(12.563534, 213431.43565, 2134231.456);
            DVector3 TheOrigionalVelocity = new DVector3(123, 435, 345);
            DVector3 TheDeltaVelocity = new DVector3(645,2345,765); 
            DisplacementStructure target = new DisplacementStructure(theObject, TheOrigionalPosition, TheDeltaPosition, TheOrigionalVelocity, TheDeltaVelocity);
            Assert.AreEqual(TheOrigionalPosition,target.Position);
            Assert.AreEqual(TheDeltaPosition,target.DeltaPosition);
            Assert.AreEqual(TheOrigionalVelocity,target.Velocity);
            Assert.AreEqual(TheDeltaVelocity,target.DeltaVelocity);
           
        }

        /// <summary>
        ///A test for DisplacementStructure Constructor
        ///</summary>
        [TestMethod()]
        public void DisplacementStructureConstructorTest()
        {
            IPhysicsObject theObject = new IPhysicsObjectMock();
            DVector3 thePosition = new DVector3(123,234,324); 
            DVector3 theVelocity = new DVector3(123,34,56); 
            DisplacementStructure target = new DisplacementStructure(theObject, thePosition, theVelocity);
            Assert.AreEqual(thePosition, target.Position);
            Assert.AreEqual(theVelocity, target.Velocity);
            Assert.AreEqual(DVector3.Zero, target.DeltaPosition);
            Assert.AreEqual(DVector3.Zero, target.DeltaVelocity );
        }
    }
}
