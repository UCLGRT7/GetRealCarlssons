using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;
using System.Linq;
using System.Collections.ObjectModel;


namespace CarlssonsUnitTest
{
    [TestClass]
    public class CustomerViewModelTests
    {
        [TestMethod]
        public void AddCustomer_ValidInput_AddsCustomer()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            var viewModel = new CustomerViewModel(mockRepo.Object);

            viewModel.AddCustomer("Sandra", "sandra@example.com");

            mockRepo.Verify(r => r.Add(It.Is<Customer>(c =>
                c.Name == "Sandra" && c.Email == "sandra@example.com"
            )), Times.Once);
        }
    }
}
