using System;
using Xamarin.Forms;

namespace XamarinLearning.DataBinding
{
	public partial class WebViewDemo : ContentPage
	{
		public WebViewDemo()
		{
			InitializeComponent();
		}

		public void OnUrlEntryCompleted(object sender, EventArgs args)
		{
			webView.Source = ((Entry)sender).Text;
		}

		public void OnBackButtonClicked(object sender, EventArgs args)
		{
			webView.GoBack();
		}

		public void OnForwardButtonClicked(object sender, EventArgs args)
		{
			webView.GoForward();
		}
	}
}

