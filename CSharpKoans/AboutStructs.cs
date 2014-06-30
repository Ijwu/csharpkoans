using CSharpKoans.Core;
using NUnit.Framework;

namespace CSharpKoans
{
    public class AboutStructs : KoanContainer
    {
        private struct Point
        {
            public double X;
            public double Y;
        }

        /// <summary>
        /// Structs are value types, and so they may never be intialized to null.
        /// </summary>
        /// <instructions>
        /// Fill in the values with the expected behavior.
        /// </instructions>
        [Koan]
        public void UnderstandDefaultValuesForStructcs()
        {
            var pt = new Point();

            Assert.AreEqual(0, pt.X);
            Assert.AreEqual(0, pt.Y);
        }
        
        /// <summary>
        /// Since structs are value types, when they are copied their whole
        /// value is copied over rather than a reference to the object.
        /// </summary>
        /// <instructions>
        /// Fill in the values with the expected behavior.
        /// </instructions>
        [Koan]
        public void UnderstandStructCopySemantics()
        {
            var pt1 = new Point
            {
                X = 3,
                Y = 4
            };

            var pt2 = pt1;

            pt2.X *= 2;
            pt2.Y *= 2;

            Assert.AreEqual(3, pt1.X);
            Assert.AreEqual(4, pt1.Y);
            Assert.AreEqual(6, pt2.X);
            Assert.AreEqual(8, pt2.Y);
        }

        /// <summary>
        /// Structs may be converted to objects and vice-versa. However, for an object
        /// to become a struct, it must have once been that struct.
        /// </summary>
        /// <instruction>
        /// Fill in the values with the expected behavior.
        /// </instruction>
        [Koan]
        public void UndestandStructConversionsToObject()
        {
            var pt1 = new Point
            {
                X = 3,
                Y = 4
            };

            object pt2 = pt1;

            pt1.X = 12;

            Assert.AreEqual(3, ((Point)pt2).X);
        }

        private const double FILL_ME_IN = -1;
    }
}