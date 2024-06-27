using System;
using CashManager.Domain.CustomerTransactionAgg;
using CashManager.Domain.ObjectValue;
using FluentAssertions;
using SweetBuilders;
using Xunit;

namespace CashManager.Domain.Test.CustomerTransactionAgg
{
    public class CustomerTransactionTest
    {
        const string EMAIL = "wodsonluiz@live.com";
        const string NAME = "Wodson";
        const string PROFILE = "Administrator";
        const string DOCUMENT = "123456789";


        [Fact]
        public void GivenCreateTransaction_WhenWithoutDocument_ThenReturnThrow()
        {
            //arrange
            var company = Builder<Company>.New.Create();
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new CustomerTransaction(null, null, NAME, EMAIL, PROFILE, company, transaction);

            //assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenCreateTransaction_WhenWithoutName_ThenReturnThrow()
        {
            //arrange
            var company = Builder<Company>.New.Create();
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new CustomerTransaction(null, DOCUMENT, null, EMAIL, PROFILE, company, transaction);

            //assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenCreateTransaction_WhenWithoutEmail_ThenReturnThrow()
        {
            //arrange
            var company = Builder<Company>.New.Create();
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new CustomerTransaction(null, DOCUMENT, NAME, null, PROFILE, company, transaction);

            //assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenCreateTransaction_ThenWithoutProfile_ThenReturnThrow()
        {
            //arrange
            var company = Builder<Company>.New.Create();
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new CustomerTransaction(null, DOCUMENT, NAME, EMAIL, null, company, transaction);

            //assert
            action.Should().Throw<ArgumentException>();
        }

        
        [Fact]
        public void GivenCreateTransaction_WhenWithoutCompany_ThenReturnThrow()
        {
            //arrange
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new CustomerTransaction(null, DOCUMENT, NAME, EMAIL, PROFILE, null, transaction);

            //assert
            action.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void GivenCreateTransaction_WhenWithoutTransaction_ThenReturnThrow()
        {
            //arrange
            var company = Builder<Company>.New.Create();
            
            //act
            Action action = () => new CustomerTransaction(null, DOCUMENT, NAME, EMAIL, PROFILE, company, null);

            //assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenCreateTransaction_WhenPassingAllProperts_ThenReturnObject()
        {
            //arrange
            var company = Builder<Company>.New.Create();
            var transaction = Builder<Transaction>.New.Create();
            
            //act
            Action action = () => new CustomerTransaction(null, DOCUMENT, NAME, EMAIL, PROFILE, company, transaction);

            //assert
            action.Should().NotThrow();
        }


    }
}