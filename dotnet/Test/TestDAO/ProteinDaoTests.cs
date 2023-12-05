﻿using Capstone.DAO;
using Capstone.Models;

namespace Test.TestDAO
{
    [TestClass]
    public class ProteinDaoTests : BaseDaoTests
    {
        ProteinSqlDao dao;
        [TestInitialize]
        public virtual void Init()
        {
            dao = new ProteinSqlDao(connectionString);
        }

        [TestMethod]
        public void GetListofProteinsTest()
        {
            IList<Protein> proteins = dao.GetProteins();
            Assert.AreEqual(1, proteins.Count);
        }

        [TestMethod]
        public void GetProteinByIdHappyPath()
        {
            Protein protein = dao.GetProteinById(1);
            Assert.AreEqual(protein.SequenceName, "Insulin");
        }

        [TestMethod]
        public void GetProteinByIdInvalidId()
        {
            Protein protein = dao.GetProteinById(100);
            Assert.IsNull(protein);
        }

        [TestMethod]
        public void GetProteinsBySequenceNameHappyPath()
        {
            IList<Protein> proteins = dao.GetProteinsBySequenceName("Insulin");
            Assert.AreEqual(1, proteins.Count);
        }

        [TestMethod]
        public void GetProteinsBySequenceNameInvalidSequenceName()
        {
            IList<Protein> proteins = dao.GetProteinsBySequenceName("abc");
            Assert.AreEqual(0, proteins.Count);
        }

        [TestMethod]
        public void GetProteinsByUserIdHappyPath()
        {
            IList<Protein> proteins = dao.GetProteinsByUserId(1);
            Assert.AreEqual(1, proteins.Count);
        }

        [TestMethod]
        public void GetProteinsByUserIdInvalidId()
        {
            IList<Protein> proteins = dao.GetProteinsByUserId(100);
            Assert.AreEqual(0, proteins.Count);
        }


        [TestMethod]
        public void CreateProteinAddsProtein()
        {
            dao.CreateProtein("name", "sequence", "description", 1, 1);
            IList<Protein> proteins = dao.GetProteins();
            Assert.AreEqual(2, proteins.Count);
        }

        [TestMethod]
        public void CreateProteinCreatedProteinNotNull()
        {
            Protein newProtein = dao.CreateProtein("name", "sequence", "description", 1, 1);
            IList<Protein> protein = dao.GetProteinsBySequenceName("name");
            AssertProperties(newProtein, protein[0]);
        }

        [TestMethod]
        public void UpdateProteinChangesProtein()
        {
            Protein newProtein = dao.UpdateProtein(1, "name", "sequence", "description", 2, 2);
            Protein protein = dao.GetProteinById(1);
            AssertProperties(newProtein, protein);
        }


        [TestMethod]
        public void DeleteProteinDeletesProtein()
        {
            dao.DeleteProteinById(1);
            IList <Protein> proteins = dao.GetProteins();
            Assert.AreEqual(0, proteins.Count);
        }

        private void AssertProperties(Protein newProtein, Protein protein)
        {
            Assert.AreEqual(newProtein.ProteinId, protein.ProteinId);
            Assert.AreEqual(newProtein.SequenceName, protein.SequenceName);
            Assert.AreEqual(newProtein.ProteinSequence, protein.ProteinSequence);
            Assert.AreEqual(newProtein.Description, protein.Description);
            Assert.AreEqual(newProtein.FormatType, protein.FormatType);
            Assert.AreEqual(newProtein.UserId, protein.UserId);
        }
    }
}