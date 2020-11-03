using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectQCMApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                QCMApp.Controllers.QuestionnaireController ctrl = new QCMApp.Controllers.QuestionnaireController();
                var r = ctrl.ListeQuestionnaires("j");
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
    }
}
