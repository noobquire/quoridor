using NUnit.Framework;
using QuoridorGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoridorGame.Model.Tests
{
    [TestFixture]
    public class UtilityExtensionsTests
    {
        [Test]
        public void DeepClone_MakesDeepCloneWithoutReferencesToInitialObject()
        {
            var cellField = new CellField();

            var deepClone = cellField.DeepClone();

            Assert.That(deepClone[0,0], Is.Not.SameAs(cellField[0,0]));
        }
    }
}
