using NUnit.Framework;
using Ottoman.CrossCutting.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ottoman.CrossCutting.Additional.Tests
{
    using PostSharp.Aspects.Advices;

    [TestFixture()]
    public class OttomanHandlerTests
    {
        [Test()]
        public void SendAsyncTest()
        {
            //object content = new int[] { 1, 3, 5 };

            //var genericType = typeof(OttomanResponse<>);
            //var specificType = genericType.MakeGenericType(content.GetType());
            //var ottomanResponse = Activator.CreateInstance(specificType,);

            //Assert.Fail();
        }
    }
}