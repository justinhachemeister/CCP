﻿using ConventionalCommandLineParser.UnitTests.MockExecutors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConventionalCommandLineParser.UnitTests
{
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public void When_NoArguments_BuildsCorrectExecutable()
        {
            string[] args = new string[] { };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly);

            Assert.AreEqual(0, executables.Length);
        }

        [TestMethod]
        public void When_CommandWithNoArgs_BuildsCorrectExecutable()
        {
            string[] args = new string[] { "CommandWithNoArgs" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithNoArgs);
        }
    }
}