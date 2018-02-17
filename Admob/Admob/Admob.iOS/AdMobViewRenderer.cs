using Admob;
using Admob.iOS;
using Google.MobileAds;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace Admob.iOS
{
	public class AdMobViewRenderer : ViewRenderer<AdMobView, BannerView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
		{
			base.OnElementChanged(e);
			if (Control == null)
			{
				SetNativeControl(CreateBannerView());				
			}
		}
		
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == nameof(BannerView.AdUnitID))
				Control.AdUnitID = Element.AdUnitId;
		}

		private BannerView CreateBannerView()
		{
			var bannerView = new BannerView(AdSizeCons.SmartBannerPortrait)
			{
				AdUnitID = Element.AdUnitId,
				RootViewController = GetVisibleViewController()
			};

			bannerView.LoadRequest(GetRequest());

			Request GetRequest()
			{
				var request = Request.GetDefaultRequest();
				return request;
			}
			
			return bannerView;
		}
			
		private UIViewController GetVisibleViewController()
		{
			var windows = UIApplication.SharedApplication.Windows;
			foreach (var window in windows)
			{
				if (window.RootViewController != null)
				{
					return window.RootViewController;
				}				
			}
			return null;
		}
	}
}