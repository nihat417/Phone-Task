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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Phone_Task;



public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Configure();
    }

    public void Configure()
    {
        string num1 = "ABC";
        a.Content += num1;
        a.FontSize = 4;

    }


}
