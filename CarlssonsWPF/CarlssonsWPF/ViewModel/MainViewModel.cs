using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Data.UnitOfWork;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    public class MainViewModel
    {

        public MainViewModel()
        {
 

        }
        private readonly IUnitOfWork _unitOfWork;
        public MainViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            var customers = _unitOfWork.Customers;
            var contracts = _unitOfWork.Contracts;
            var projects = _unitOfWork.Projects;
            var services = _unitOfWork.Services;

        }
    }
}
