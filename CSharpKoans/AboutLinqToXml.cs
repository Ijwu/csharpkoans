using System.Collections.Generic;
using System.Linq;
using CSharpKoans.Core;
using System.Xml.Linq;
using System.IO;
using NUnit.Framework;

namespace CSharpKoans
{
    public class AboutLinqToXml : KoanContainer
    {
        private class Senator
        {
            public string LastName { get; set; }
            public string Party { get; set; }
            public string State { get; set; }
        }

        /// <summary>
        /// XML documents are a tree like structure.
        /// </summary>
        /// <instructions>
        /// Fill in the values with the expected behavior.
        /// </instructions>
        [Koan]
        public void LinqToXmlBuildsAnObjectTreeFromTheXmlDocument()
        {
            var contactInfo = SenatorsDoc.Root;

            Assert.AreEqual("contact_information", contactInfo.Name.LocalName);
            Assert.AreEqual(null, contactInfo.Parent);
            Assert.AreEqual(true, contactInfo.HasElements);
        }

        /// <summary>
        /// Each node in the tree is the root of its own tree.
        /// </summary>
        /// <instructions>
        /// Fill in the values with the expected behavior.
        /// </instructions>
        [Koan]
        public void EachNodeInTheTreeIsAlsoATree()
        {
            var contactInfo = SenatorsDoc.Root;

            var senators = from s in contactInfo.Elements()
                           select s;        

            var firstSenator = senators.First();

            Assert.AreEqual("member", firstSenator.Name.LocalName);
            Assert.AreEqual("contact_information", firstSenator.Parent.Name.LocalName);
            Assert.AreEqual(true, firstSenator.HasElements);

            Assert.AreEqual(1, firstSenator.Ancestors().Count());

            var lastNameNodes = firstSenator.Elements("last_name");

            var lastName = lastNameNodes.First();

            Assert.AreEqual("last_name", lastName.Name.LocalName);
            Assert.AreEqual("Akaka", lastName.Value);
        }

        /// <summary>
        /// You can use LINQ to query XML documents as well.
        /// </summary>
        /// <instructions>
        /// Fill in the values with the expected behavior.
        /// </instructions>
        [Koan]
        public void YouCanUseLinqToQueryElements()
        {
            var contactInfo = SenatorsDoc.Root;
            var senators = contactInfo.Elements();
            var firstSenator = senators.First();

            var state = from e in firstSenator.Elements()
                        where e.Name == "state"
                        select e.Value;

            Assert.AreEqual(1, state.Count());
            Assert.AreEqual("HI", state.First());
        }

        /// <summary>
        /// The XML documents are enumerable, and so they are searchable with LINQ.
        /// </summary>
        /// <instructions>
        /// Fill in the LINQ query to match the expected behavior.
        /// </instructions>
        [Koan]
        public void LinqToXmlProducesEnumerableData()
        {
            var contactInfo = SenatorsDoc.Root;
            var thirdSenator = contactInfo.Elements().Skip(2).First();
            var nameOfThirdSenator = (from s in thirdSenator.Elements()
                                     where s.Name == "last_name"
                                     select s).First().Value;

            Assert.AreEqual("Ayotte", nameOfThirdSenator);
        }

        /// <summary>
        /// LINQ may be combined with the XML documents to turn the XML data into objects.
        /// </summary>
        /// <instructions>
        /// Fill in the LINQ queries to match the expected behavior.
        /// </instructions>
        [Koan]
        public void LinqToXmlCreatesObjectsFromXmlElements()
        {
            var contactInfo = SenatorsDoc.Root;

            var senatorObjects = from s in contactInfo.Elements()
                                 select new Senator()
                                 {
                                     LastName = s.Element("last_name").Value,
                                     State = s.Element("state").Value,
                                     Party = s.Element("party").Value
                                 };

            Assert.AreEqual("Wyden", senatorObjects.Last().LastName);

            var democrats = from s in senatorObjects where s.Party == "D" select s;

            Assert.AreEqual(51, democrats.Count());

            var stateOfIndependentSenator = (from s in senatorObjects where s.Party == "ID" select s.State).First();

            Assert.AreEqual(stateOfIndependentSenator, "CT");
        }

        private const int FILL_ME_IN = -1;
        private IEnumerable<Senator> LINQ_FILL_ME_IN;
        private string[] ARRAY_FILL_ME_IN = new string[] { };
        XDocument SenatorsDoc = XDocument.Load(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "senators_cfm.xml"));
    }
}
