using System;
using CashManager.Domain.ObjectValue;
using CashManager.Domain.ReportAgg;
using FluentAssertions;
using SweetBuilders;
using Xunit;

namespace CashManager.Domain.Test.ReportAgg
{
    public class ReportAggTest
    {
        const string EMAIL = "wodsonluiz@live.com";
        const string NAME = "Wodson";
        const string DOCUMENT = "123456789";

        [Fact]
        public void GivenReport_WhenWithoutDocument_ThenReturnThrow()
        {
            //arrange
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new ReportDaily(null, NAME, null, transaction);

            //assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenReport_WhenWithoutName_ThenReturnThrow()
        {
            //arrange
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new ReportDaily(null, null, DOCUMENT, transaction);

            //assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenReport_WhenWithoutTransaction_ThenReturnThrow()
        {
            //arrang + act
            Action action = () => new ReportDaily(null, NAME, DOCUMENT, null);

            //assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenReport_WhenPassingAllProperts_ThenReturnObject()
        {
            //arrange
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new ReportDaily(null, NAME, DOCUMENT, transaction);

            //assert
            action.Should().NotThrow();
        }
        
    }
}