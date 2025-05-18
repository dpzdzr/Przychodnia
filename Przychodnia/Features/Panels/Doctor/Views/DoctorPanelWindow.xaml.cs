using Przychodnia.Features.Panels.Doctor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Przychodnia.Features.Panels.Doctor.Views
{
    /// <summary>
    /// Interaction logic for DoctorPanelWindow.xaml
    /// </summary>
    public partial class DoctorPanelWindow : Window
    {
        public DoctorPanelWindow(DoctorPanelViewModel vm)
        {
            InitializeComponent();
            vm.CloseApplicationEvent += Close;
            DataContext = vm;
        }
    }
}
