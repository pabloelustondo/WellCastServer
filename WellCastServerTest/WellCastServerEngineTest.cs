using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WellCastServer;
using WellCastServer.Models;

namespace WellCastServerTest
{
    [TestClass]
    public class WellCastServerEngineTest
    {
        [TestMethod]
        public void addAlert2UserTest()
        {
           WellCastServerEngine mm = new WellCastServerEngine();

          User user = new User();

            user.ID = "542c53a302d6a4910db3fdd7";

            mm.addAlert2User(user);

        }
    }
}
