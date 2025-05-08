using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    public class ProjektMainPageViewModel
    {
        
        public ObservableCollection<CombinedProjectData> ProjectGridItems { get; set; } = new ObservableCollection<CombinedProjectData>();

        public ProjektMainPageViewModel()
        {

            
        }

    }
}
