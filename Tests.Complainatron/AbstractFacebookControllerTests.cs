using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Complainatron.Controllers;
using Complainatron.Core.Services;
using FluentAssertions;
using FluentAssertions.Assertions;
using FakeItEasy;

namespace Tests.Complainatron
{
    [TestClass]
    public class AbstractFacebookControllerTests
    {
        private class FakeController : AbstractFacebookController
        {
            public FakeController(IFacebookService _facebookService, ILogService _loggingService)
                : base(_facebookService, _loggingService)
            {

            }
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_facebook_service_is_null()
        {
            var loggingService = A.Fake<ILogService>();

            Action creation = () => new FakeController(null, loggingService);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_logging_service_is_null()
        {
            var facebookService = A.Fake<IFacebookService>();

            Action creation = () => new FakeController(facebookService, null);

            creation.ShouldThrow<ArgumentNullException>();
        }
    }
}
