using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DAL.Utilities
{
    public static class Services
    {
        public static void ThrowChildrenPresentException()
        {
            throw new Exception("Object cannot be deleted because child objects are present");
        }
    }
}
