using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Study.Fit.PageGenerator.Test
{
    [TestFixture]
    public class PageGeneratorTests
    {
        private const string PropertiesFilename = "properties.xml";
        private const string ContentFilename = "content.txt";
        private const string AssemblyFitName = "StudyFitPageGeneratorTest";
        private static readonly string _fitnessRootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "FitnesseRoot");
        private static readonly string _expectedFitnessRootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "FitnesseRoot.expected");
        private readonly string[] TestFixtureClasses = new string[] { "ThisIsATestFixture", "AnotherTestFixture" };

        [SetUp]
        public void Setup()
        {
            if (Directory.Exists(_fitnessRootPath))
                Directory.Delete(_fitnessRootPath, true);
        }

        [Test]
        public void Generator_create_assembly_subfolder()
        {
            GeneratePages();
            Assert.IsTrue(Directory.Exists(Path.Combine(_fitnessRootPath, AssemblyFitName)), Path.Combine(_fitnessRootPath, AssemblyFitName) + " has not been created");
        }

        [Test]
        public void Generator_build_properties_file_in_assembly_subfolder()
        {
            GeneratePages();
            Assert.IsTrue(File.Exists(Path.Combine(Path.Combine(_fitnessRootPath, AssemblyFitName), PropertiesFilename)), Path.Combine(Path.Combine(_fitnessRootPath, AssemblyFitName), PropertiesFilename) + " does not exist");
        }

        [Test]
        public void Generator_build_content_file_in_assembly_subfolder()
        {
            GeneratePages();
            Assert.IsTrue(File.Exists(Path.Combine(Path.Combine(_fitnessRootPath, AssemblyFitName), ContentFilename)), Path.Combine(Path.Combine(_fitnessRootPath, AssemblyFitName), ContentFilename) + " does not exist");
        }

        [Test]
        public void Generator_build_properties_file_in_type_subfolder()
        {
            GeneratePages();
            foreach (string classname in TestFixtureClasses)
            {
                AssertPropertiesFileExists(classname);                
            }
        }

        [Test]
        public void Generator_build_content_file_in_type_subfolder()
        {
            GeneratePages();
            foreach (string classname in TestFixtureClasses)
            {
                AssertContentFileExists(classname);
            }
        }

        [Test]
        public void Generator_build_expected_content_file_in_type_subfolder()
        {
            GeneratePages();
            foreach (string classname in TestFixtureClasses)
            {
                AssertIsExpectedFile(ContentFilename, classname);
            }
        }

        [Test]
        public void Generator_build_expected_content_file_in_assembly_folder()
        {
            GeneratePages();
            FileAssert.AreEqual(Path.Combine(Path.Combine(_expectedFitnessRootPath, AssemblyFitName), ContentFilename), Path.Combine(Path.Combine(_fitnessRootPath, AssemblyFitName), ContentFilename), Path.Combine(Path.Combine(_expectedFitnessRootPath, AssemblyFitName), ContentFilename) + " does not match expected");
        }

        private static void AssertContentFileExists(string testFixtureClass)
        {
            Assert.IsTrue(File.Exists(GetPathFor(ContentFilename, testFixtureClass)), GetPathFor(ContentFilename, testFixtureClass) + " does not exist");
        }

        private static void AssertPropertiesFileExists(string testFixtureClass)
        {
            Assert.IsTrue(File.Exists(GetPathFor(PropertiesFilename, testFixtureClass)), GetPathFor(PropertiesFilename, testFixtureClass) + " does not exists");
        }

        private static void AssertIsExpectedFile(string filename, string testFixtureClass)
        {
            FileAssert.AreEqual(GetPathForExpected(filename, testFixtureClass), GetPathFor(filename, testFixtureClass), filename + " does not match expected");
        }

        private static string GetPathFor(string filename, string testFixtureClass)
        {
            return Path.Combine(Path.Combine(Path.Combine(_fitnessRootPath, AssemblyFitName), testFixtureClass), filename);
        }

        private static string GetPathForExpected(string filename, string testFixtureClass)
        {
            return Path.Combine(Path.Combine(Path.Combine(_expectedFitnessRootPath, AssemblyFitName), testFixtureClass), filename);
        }

        private void GeneratePages()
        {
            var generator = new PageGenerator(GetType().Assembly, _fitnessRootPath);
            generator.GeneratePages(t => TestFixtureClasses.FirstOrDefault(classname => classname == t.Name) != null);
        }
    }
}