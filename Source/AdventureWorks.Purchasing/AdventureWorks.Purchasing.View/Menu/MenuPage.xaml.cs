﻿using System;
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
using AdventureWorks.Purchasing.ViewModel;
using AdventureWorks.Purchasing.ViewModel.Menu;

namespace AdventureWorks.Purchasing.View.Page
{
    /// <summary>
    /// MenuPage.xaml の相互作用ロジック
    /// </summary>
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
        }
    }

    public class MenuDesignViewModel : MenuViewModel
    {
        public MenuDesignViewModel(IPresentationService presentationService) : base(default!)
        {
        }
    }
}