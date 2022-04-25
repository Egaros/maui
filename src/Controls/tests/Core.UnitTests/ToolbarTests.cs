﻿using System.Threading.Tasks;
using NUnit.Framework;

namespace Microsoft.Maui.Controls.Core.UnitTests
{
	[TestFixture]
	public class ToolbarTests : BaseTestFixture
	{
		[Test]
		public void ToolbarExistsForNavigationPage()
		{
			var window = new Window();
			IToolbarElement toolbarElement = window;
			var startingPage = new NavigationPage(new ContentPage());
			window.Page = startingPage;
			Assert.IsNotNull(toolbarElement.Toolbar);
		}

		[Test]
		public void ToolbarEmptyForContentPage()
		{
			Window window = new Window();
			IToolbarElement toolbarElement = window;
			var startingPage = new ContentPage();
			window.Page = startingPage;
			Assert.IsNull(toolbarElement.Toolbar);
		}

		[Test]
		public void ToolbarClearsWhenNavigationPageRemoved()
		{
			var window = new Window();
			IToolbarElement toolbarElement = window;
			var startingPage = new NavigationPage(new ContentPage());
			window.Page = startingPage;
			window.Page = new ContentPage();
			Assert.IsNull(toolbarElement.Toolbar);
		}

		[Test]
		public async Task TitleAndTitleViewAreMutuallyExclusive()
		{
			var window = new Window();
			IToolbarElement toolbarElement = window;
			var contentPage = new ContentPage() { Title = "Test Title" };
			var navigationPage = new NavigationPage(contentPage);
			window.Page = navigationPage;

			var titleView = new VerticalStackLayout();
			var toolbar = (Toolbar)toolbarElement.Toolbar;
			Assert.AreEqual("Test Title", toolbar.Title);
			NavigationPage.SetTitleView(contentPage, titleView);
			Assert.IsEmpty(toolbar.Title);
			Assert.AreEqual(titleView, toolbar.TitleView);
			NavigationPage.SetTitleView(contentPage, null);
			Assert.AreEqual("Test Title", toolbar.Title);
		}

		[Test]
		public async Task InsertPageBeforeRootPageShowsBackButton()
		{
			var window = new Window();
			IToolbarElement toolbarElement = window;
			var startingPage = new TestNavigationPage(true, new ContentPage());
			window.Page = startingPage;
			startingPage.Navigation.InsertPageBefore(new ContentPage(), startingPage.RootPage);
			await Task.Delay(50);
			Assert.True(toolbarElement.Toolbar.BackButtonVisible);
		}

		[Test]
		public async Task RemoveRootPageHidesBackButton()
		{
			var window = new Window();
			IToolbarElement toolbarElement = window;
			var startingPage = new TestNavigationPage(true, new ContentPage());
			window.Page = startingPage;
			startingPage.Navigation.PushAsync(new ContentPage());
			startingPage.Navigation.RemovePage(startingPage.RootPage);
			await Task.Delay(50);
			Assert.False(toolbarElement.Toolbar.BackButtonVisible);
		}

		[Test]
		public void BackButtonNotVisibleForInitialPage()
		{
			var window = new Window();
			IToolbarElement toolbarElement = window;
			var startingPage = new NavigationPage(new ContentPage());
			window.Page = startingPage;
			Assert.False(toolbarElement.Toolbar.BackButtonVisible);
		}
	}
}