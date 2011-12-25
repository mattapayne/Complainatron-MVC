using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Complainatron.Controllers;
using FluentAssertions;
using FluentAssertions.Assertions;
using System.Web.Mvc;

namespace Tests.Complainatron
{
    [TestClass]
    public class MainControllerTests
    {
        private MainController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _controller = new MainController();
        }

        [TestMethod]
        [TestCategory("About Action")]
        public void It_should_return_a_partial_view()
        {
            //Arrange

            //Act
            var result = _controller.About();

            result.Should().BeOfType<PartialViewResult>();
        }

        [TestMethod]
        [TestCategory("About Action")]
        public void It_should_return_a_partial_view_named__about()
        {
            //Arrange

            //Act
            var result = _controller.About() as PartialViewResult;

            result.ViewName.Should().Be("_About");
        }
    }
}
