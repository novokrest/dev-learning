using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinLearning
{
	public partial class OpacityBindingCodePage : ContentPage
	{
		public OpacityBindingCodePage()
		{
			InitializeComponent();

			Label label1 = new Label
			{
				Text = "Opacity Binding Demo (cb#1)",
				Style= (Style)Resources["labelStyle"]
			};

			label1.BindingContext = slider;
			label1.SetBinding(OpacityProperty, "Value");

			Label label2 = new Label 
			{
				Text="Opacity Binding Demo (cb#2)",
				Style = (Style)Resources["labelStyle"]
			};

			var bindingLabel2 = new Binding
			{
				Source = slider,
				Path = "Value"
			};
			label2.SetBinding(OpacityProperty, bindingLabel2);

			Label label3 = new Label
			{
				Text = "Opacity Binding Demo (cb#2)",
				Style = (Style)Resources["labelStyle"]
			};

			var bindingLabel3 = Binding.Create<Slider>((src) => src.Value);
			bindingLabel3.Source = slider;
			label3.SetBinding(OpacityProperty, bindingLabel3);

			mainStack.Children.Add(label1);
			mainStack.Children.Add(label2);
			mainStack.Children.Add(label3);
		}
	}
}

