using BankingApp.Api.Controllers;
using BankingApp.Api.Models;
using BankingApp.Api.Services;
using BankingApp.Domain;
using BankingApp.Domain.Database;
using Castle.Core.Resource;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BankingApp.UnitTest
{
    public class CustomerPaymentControllerTests // change class name add api folder and customercontrollertests
    {       

        [Fact]
        public async Task TopUp_WithCertainAmount_ShouldTopUpCustomerBalance()
        {
            //Arrange
            var customer = new Customer()
            {
                Id = 1,
                Name = "Rashid",
                Surname = "Aliyev",
                PhoneNumber = "51 980 08 69",
                Balance = 50,
                BirthDate = new DateTime(1996, 8, 31)
            };

            var mockDbSet = new Mock<DbSet<Customer>>();

            mockDbSet.Setup(d => d.FindAsync(1))
                 .ReturnsAsync(customer);

            var mockDbContext = new Mock<AppDbContext>();

            mockDbContext.Setup(c => c.Customers).Returns(mockDbSet.Object);

            var customerService = new CustomerService(mockDbContext.Object);

                        
            //Act
            await customerService.TopUp(1, 20);

            //Assert            
            Assert.Equal(70.0m, customer.Balance);            
        }

        [Fact]
        public async Task Purchase_WithCertainAmount_ShouldPurchaseCustomerBalance()
        {
            //Arrange
            var customer = new Customer()
            {
                Id = 1,
                Name = "Rashid",
                Surname = "Aliyev",
                PhoneNumber = "51 980 08 69",
                Balance = 50,
                BirthDate = new DateTime(1996, 8, 31)
            };

            var mockDbSet = new Mock<DbSet<Customer>>();

            mockDbSet.Setup(d => d.FindAsync(1))
                 .ReturnsAsync(customer);

            var mockDbContext = new Mock<AppDbContext>();

            mockDbContext.Setup(c => c.Customers).Returns(mockDbSet.Object);

            var customerService = new CustomerService(mockDbContext.Object);


            //Act
            await customerService.Purchase(1, 10);

            //Assert
            Assert.Equal(40.0m, customer.Balance);

        }

        [Fact]
        public async Task Refund_WithCertainAmount_ShouldRefundCustomerBalance()
        {
            //Arrange
            var customer = new Customer()
            {
                Id = 1,
                Name = "Rashid",
                Surname = "Aliyev",
                PhoneNumber = "51 980 08 69",
                Balance = 50,
                BirthDate = new DateTime(1996, 8, 31)
            };

            var mockDbSet = new Mock<DbSet<Customer>>();

            mockDbSet.Setup(d => d.FindAsync(1))
                 .ReturnsAsync(customer);

            var mockDbContext = new Mock<AppDbContext>();

            mockDbContext.Setup(c => c.Customers).Returns(mockDbSet.Object);

            var customerService = new CustomerService(mockDbContext.Object);


            //Act
            await customerService.Refund(1, 5);

            //Assert

            Assert.Equal(55.0m, customer.Balance);
        }
    }
}