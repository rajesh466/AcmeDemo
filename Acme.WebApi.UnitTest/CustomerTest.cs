using Acme.WebApi.Docker.Demo.Controllers;
using Acme.WebApi.Docker.Demo.DatabaseContext;
using Acme.WebApi.Docker.Demo.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Acme.WebApi.UnitTest
{
    public class CustomerTest
    {
        [Fact]
        public async Task GetCustomers_WhenCalled_ReturnsCustomersListAsync()
        {
            //Arrange
            var customerContextMock = new Mock<ApplicationDbContext>();
            customerContextMock.Setup<DbSet<Customer>>(x => x.Customers).ReturnsDbSet(CustomerTest.GetFakeCustomersList());
        
            //Act
            CustomersController customersController = new(customerContextMock.Object);
            var customers = (await customersController.GetCustomers()).Value;
        
            //Assert
            Assert.NotNull(customers);
            Assert.Equal(2, customers.Count());
        }

        private static List<Customer> GetFakeCustomersList()
        {
            return new List<Customer>() { new Customer()
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = "Rajesh Test",
                Address = "Address Test",
                Description = "Desc text"
            },
            new Customer()
            {
                 CustomerId = Guid.NewGuid(),
                CustomerName = "Kiran Test",
                Address = "Jubilee Hills",
                Description = "Desc text"
            }
            };
        }
    }


}