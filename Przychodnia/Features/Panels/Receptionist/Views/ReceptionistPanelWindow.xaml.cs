using Przychodnia.Features.Panels.Receptionist.ViewModels;
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

namespace Przychodnia.Features.Panels.Receptionist.Views
{
    /// <summary>
    /// Interaction logic for ReceptionistPanelWindow.xaml
    /// </summary>
    public partial class ReceptionistPanelWindow : Window
    {
        public ReceptionistPanelWindow(ReceptionistPanelViewModel vm)
        {
            InitializeComponent();
            vm.CloseApplicationEvent += Close;
            DataContext = vm;
        }
    }
}
