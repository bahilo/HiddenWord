using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiddenWordCommon.Interfaces.Business;
using HiddenWord.Business;
using HiddenWordCommon.Interfaces.DAL;
using HiddenWordCommon.classes;
using System.Collections.Generic;
using HiddenWordBusiness.Core;
using HiddenWordBusiness.classes;
using HiddenWordCommon.Enums;
using HiddenWordTest.Classes;

namespace HiddenWordTest
{
    [TestClass]
    public class UnitTestBL
    {
        IDisplay blDisplay;
        BL Bl;
        Random rd;

        public UnitTestBL()
        {
            blDisplay = new BusinessDisplay(new HiddenWordConsole.Classes.Display(),
                                                        new BusinessStatistic(new FakeDAL()),
                                                        new BusinessWord(new FakeDAL()),
                                                        new BusinessSetup(new FakeDAL()),
                                                        new BusinessUser(new FakeDAL()));
            Bl = new BL(new BusinessStatistic(new FakeDAL()),
                            new BusinessWord(new FakeDAL()),
                            new BusinessSetup(new FakeDAL()),
                            new BusinessUser(new FakeDAL()),
                            new FakeDAL(),
                            blDisplay);
            rd = new Random();
        }

        //-------------------------[ Testing BLWord ]---------------------------------
        
        [TestMethod]
        public void TestGetRandomWord()
        {                
            Assert.AreEqual(Bl.BlWord.getNewRandomWord(rd).Name, "dance");
        }

        [TestMethod]
        public void TestUpdateWord()
        {
            Words word = Bl.BlWord.GetWordsById(1);// new Words { Id = 1, Name = "dance" };
            Assert.AreEqual(Bl.BlWord.UpdateWord(new Words { Id=word.Id, Name="swim"}).Name, "swim");
        }

        [TestMethod]
        public void TestInsertWord()
        {
            //Assert.AreEqual(Bl.BlWord.InsertWord("swim").Name, "swim");
        }


        //-------------------------[ Testing BLUser ]---------------------------------

        [TestMethod]
        public void TestGetUserByPseudo()
        {            
            Assert.AreEqual(Bl.BlUser.GetUserByPseudo("bahilo").Pseudo, "bahilo");
        }

        //-------------------------[ Testing BLSetup ]---------------------------------

        [TestMethod]
        public void TestGetSetupByStatus()
        {            
            Assert.AreEqual(Bl.BlSetup.GetSetupByStatus((int)ESetup.Active)[0].Status, (int)ESetup.Active);
        }


    }

    
}
