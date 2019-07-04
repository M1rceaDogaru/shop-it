using Android.Gms.Vision;
using Android.Gms.Vision.Barcodes;
using Android.Widget;
using ShopIt.Services;
using System;
using System.Threading.Tasks;
using System.Threading;
using Android.App;
using System.Linq;

namespace ShopIt.Android.Infrastructure
{
    class ProductTracker : Tracker
    {
        private readonly TextView _barcodeInfo;
        private readonly AdvertService _advertService;
        private readonly MainActivity _mainActivity;

        private bool _fetchingInProgress;

        public ProductTracker(TextView barcodeInfo, MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
            _barcodeInfo = barcodeInfo;

            var serverUrlTextbox = (EditText)mainActivity.FindViewById(Resource.Id.server_url);
            _advertService = new AdvertService(() => serverUrlTextbox.Text);
        }

        public override void OnUpdate(Detector.Detections detections, Java.Lang.Object item)
        {
            if (_mainActivity.IsAdvertViewVisible)
                return;

            if (_fetchingInProgress)
                return;

            var detectedItem = item as Barcode;
            if (detectedItem != null)
            {
                var value = Guid.Empty;
                if (Guid.TryParse(detectedItem.DisplayValue, out value))
                {
                    _fetchingInProgress = true;
                    DisplayMessage("Retrieving product details...");

                    ThreadPool.QueueUserWorkItem(async o => await GetAdvert(detectedItem.DisplayValue));                    
                }
                else
                {
                    DisplayMessage("Not a product QR code");
                }
            }
        }

        private async Task GetAdvert(string id)
        {
            var result = await _advertService.FetchAdvertAsync(id);

            if (result == null || result.Products == null || !result.Products.Any())
            {
                DisplayMessage("No product found");
            }
            else
            {
                DisplayMessage("Products found");
                _mainActivity.RunOnUiThread(() => _mainActivity.ShowProducts(result));
            }

            _fetchingInProgress = false;
        }

        private void DisplayMessage(string message)
        {
            _mainActivity.RunOnUiThread(() => _barcodeInfo.Post(() => _barcodeInfo.SetText(message, TextView.BufferType.Normal)));
        }
    }
}