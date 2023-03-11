using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;
using System.Diagnostics;
using Driver.Windows;
using AdventureWorks.Purchasing.View.RePurchasing;
using FluentAssertions;

namespace Scenario
{
    [TestFixture]
    public class Test
    {
        WindowsAppFriend _app;

        [SetUp]
        public void TestInitialize() => _app = ProcessController.Start();

        [TearDown]
        public void TestCleanup() => _app.Kill();

        [Test]
        public void TestMethod1()
        {
            var mainWindow = _app.AttachMainWindow();
            var menuPage = _app.AttachMenuPage();
            var requiringPurchaseProductsPage = _app.AttachRequiringPurchaseProductsPage();

            // RePurchasingボタンを押下する。
            menuPage.NavigateRePurchasing.EmulateClick();
            mainWindow.NavigationFrame.Should().BeOfPage<RequiringPurchaseProductsPage>();

            requiringPurchaseProductsPage.RequiringPurchaseProducts.RowCount.Should().Be(9);
            requiringPurchaseProductsPage.SelectedRequiringPurchaseProductVendorName.Text.Should().Be("Vendor 1");

            // 発注画面へ遷移
            requiringPurchaseProductsPage.PurchaseCommand.EmulateClick();
            mainWindow.NavigationFrame.Should().BeOfPage<RePurchasingPage>();


        }
    }
}
