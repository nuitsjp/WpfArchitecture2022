﻿using AdventureWorks.Business.Purchasing.RePurchasing.ViewModel;

namespace AdventureWorks.Business.Purchasing.View.RePurchasing
{
    /// <summary>
    /// RequiringPurchaseProductsPage.xaml の相互作用ロジック
    /// </summary>
    public partial class RequiringPurchaseProductsPage
    {
        public RequiringPurchaseProductsPage()
        {
            InitializeComponent();
        }
    }

    public class RequiringPurchaseProductsDesignViewModel : RequiringPurchaseProductsViewModel
    {
        public RequiringPurchaseProductsDesignViewModel() : base(default!, default!, default!)
        {
        }
    }
}