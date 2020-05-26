using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cycloid.Services;
using Cycloid.Models;
using System.Linq;

namespace Cycloid.API.Tests
{
    /// <summary>
    /// Summary description for ProgramServiceTest
    /// </summary>
    [TestClass]
    public class ProgramServiceTest
    {
        private IProgramsService _programsService; 

        [TestInitialize]
        public void Initialize()
        {
            _programsService = new ProgramsRestService();
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetAllPrograms_shouldRetrievePrograms()
        {
            List<Program> programs = _programsService.GetAllProgramsAsync().Result;

            Assert.IsTrue(programs.Any());
        }
    }
}
