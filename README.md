# 2022年版実践WPF業務アプリケーションのアーキテクチャ ～ドメイン駆動設計＆Clean Architectureとともに～

## はじめに

本リポジトリーは、CodeZine様に掲載いただいた「2022年版実践WPF業務アプリケーションのアーキテクチャ ～ドメイン駆動設計＆Clean Architectureとともに～」の記事内で使用したサンプルコードです。

- [見積編](https://codezine.jp/article/detail/16953)
- [設計編／前編](https://codezine.jp/article/detail/17633)
- [設計編／後編](https://codezine.jp/article/detail/17633)

本リポジトリーはGrapeCity様から下記の評価版ライセンスを提供いただくことで、実際に動作します。

ぜひ掲載の記事とあわせて実際に動作させてみてください。

## コードへの指摘や質問について

より良いコードへの改善案や質問は、いつでも受け付けております。ぜひIssueにてご連絡ください。

## 動作環境

下記の環境で動作を確認しています。

* Visual Studio 2022 Version 17.4.0+
* Docker Desktop 4.14.0
* Docker version 20.10.20
* SQL Server 2022-latest(on Docker)
* [ComponentOne for WPF Edition 2022v2](https://www.grapecity.co.jp/developer/componentone/wpf)
* [SPREAD for WPF 4.0J](https://www.grapecity.co.jp/developer/spread-wpf)
* .NET 6.0.11
* PowerShell Core

## 事前準備

下記の製品のページの「トライアル版」からライセンスを申請し、コンポーネントをインストールして、トライアルライセンスを有効化してください。

* [ComponentOne for WPF Edition 2022v2](https://www.grapecity.co.jp/developer/componentone/wpf)
* [SPREAD for WPF 4.0J](https://www.grapecity.co.jp/developer/spread-wpf)

## 動作手順

以下はPowerShell Coreから実行してください。

リポジトリーをクローンして、ディレクトリを移動する。

```powershell
git clone https://github.com/nuitsjp/WpfArchitecture2022.git
cd WpfArchitecture2022
```

SQL Serverコンテナーをビルドして起動します。

```powershell
.\Start-Dev.ps1
```

Visual Studioでソリューションを開きます。UIから開いても、もちろん問題ありません。

```powershell
.\Source\AdventureWorks.sln
```

下記の手順でスタートアッププロジェクトを構成します。

1. ソリューションを右クリックし「スタートアッププロジェクトの構成」を選択
2. 「マルチスタートアップ プロジェクト」を選択
3. 下記のプロジェクトを「開始」に変更
   1. AdventureWorks.Authentication.Jwt.Hosting.Rest
   2. AdventureWorks.Business.Purchasing.Hosting.MagicOnion
   3. AdventureWorks.Business.Purchasing.Hosting.Wpf
   4. AdventureWorks.Logging.Hosting.MagicOnion

あとはF5で実行してください。

最後に、SQL Serverを停止して終了します。

```powershell
.\Stop-Dev.ps1
```
