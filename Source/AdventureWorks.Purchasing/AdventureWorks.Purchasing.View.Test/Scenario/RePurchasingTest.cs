using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;
using System.Diagnostics;
using Driver.Windows;
using AdventureWorks.Purchasing.View.RePurchasing;
using FluentAssertions;
using Codeer.Friendly;
using Driver.Windows.Native;
using AdventureWorks.Purchasing.View;

namespace Scenario
{
    [TestFixture]
    public class RePurchasingTest
    {
        WindowsAppFriend _app;

        [SetUp]
        public void TestInitialize() => _app = ProcessController.Start();

        [TearDown]
        public void TestCleanup() => _app.Kill();

        [Test]
        public void 正常に再発注する()
        {
            var mainWindow = _app.AttachMainWindow();
            var menuPage = _app.AttachMenuPage();
            var rePurchasingPage = _app.AttachRePurchasingPage();
            var requiringPurchaseProductsPage = _app.AttachRequiringPurchaseProductsPage();

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // MenuPage
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            // メニューから再発注を選択し、RequiringPurchaseProductsPageへ移動する。
            menuPage.NavigateRePurchasing.EmulateClick();
            mainWindow.NavigationFrame.Should().BeOfPage<RequiringPurchaseProductsPage>();


            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // RequiringPurchaseProductsPage
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            // グリッドの表示行数の確認
            requiringPurchaseProductsPage.RequiringPurchaseProducts.RowCount.Should().Be(9);
            // 選択済みベンダーの確認
            requiringPurchaseProductsPage.SelectedRequiringPurchaseProductVendorName.Text.Should().Be("Vendor 1");

            // 発注ボタンを押下し、RePurchasingPage画面へ遷移する。
            requiringPurchaseProductsPage.PurchaseCommand.EmulateClick();
            mainWindow.NavigationFrame.Should().BeOfPage<RePurchasingPage>();

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // RePurchasingPage
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            // 発注ボタンを押下し、登録完了ダイアログで、OKを押下する。
            var async = new Async();
            rePurchasingPage.PurchaseCommand.EmulateClick(async);
            var messageBox = _app.Attach_MessageBox(@"");
            messageBox.Button_OK.EmulateClick();
            async.WaitForCompletion();

            // RequiringPurchaseProductsPage画面へ戻る
            mainWindow.NavigationFrame.Should().BeOfPage<RequiringPurchaseProductsPage>();

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // RequiringPurchaseProductsPage
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            // 発注した商品が減っていることを確認する。
            requiringPurchaseProductsPage.RequiringPurchaseProducts.RowCount.Should().Be(7);
        }
    }
}
