using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinLearning.MarkupExtension
{
	public class HslColorExtension : IMarkupExtension
	{
		public HslColorExtension()
		{
			A = 1;
		}

		public double H { get; set; }
		public double S { get; set; }
		public double L { get; set; }
		public double A { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			return Color.FromHsla(H, S, L, A);
		}
	}
}

