using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Gms.Vision.Barcodes;
using ShopIt.Android;
using Android.Gms.Vision;
using ShopIt.Android.Infrastructure;
using static Android.Gms.Vision.MultiProcessor;
using System;
using ShopIt.Models;
using Android.Util;
using Android.Content;
using AndroidNet = Android.Net;
using System.Threading;

namespace ShopIt
{
    [Activity(Label = "ShopIt", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IFactory
    {
        public bool IsAdvertViewVisible { get; set; }

        private CameraSource _cameraSource;
        private SurfaceView _cameraView;
        private TextView _barcodeInfo;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _cameraSource.Stop();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);           

            SetContentView(Resource.Layout.Main);            

            _cameraView = (SurfaceView)FindViewById(Resource.Id.camera_view);
            _barcodeInfo = (TextView)FindViewById(Resource.Id.code_info);

            var barcodeDetector = new BarcodeDetector.Builder(this)
                .SetBarcodeFormats(BarcodeFormat.QrCode)
                .Build();

            barcodeDetector.SetProcessor(new Builder(this)
                    .Build());

            _cameraSource = new CameraSource.Builder(this, barcodeDetector)
                .SetRequestedPreviewSize(640, 900)
                .SetAutoFocusEnabled(true)
                .Build();           
        }

        protected override void OnResume()
        {
            base.OnResume();

            ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    while (_cameraView.Holder.IsCreating) Thread.Sleep(50);

                    _cameraSource.Start(_cameraView.Holder);
                }
                catch (Exception) { }
            });            
        }

        protected override void OnPause()
        {
            base.OnPause();

            _cameraSource.Stop();
        }

        public Tracker Create(Java.Lang.Object item)
        {
            return new ProductTracker(_barcodeInfo, this);
        }

        public void ShowProducts(Advert advert)
        {
            var view = (ScrollView)FindViewById(Resource.Id.ad_view);
            var layout = (LinearLayout)FindViewById(Resource.Id.ad_layout);
            layout.RemoveAllViews();

            var id = 0;
            foreach (var product in advert.Products)
            {
                id++;
                ShowProductDetails(layout, product, id);
            }

            var closeButton = new Button(this);
            closeButton.SetTextSize(ComplexUnitType.Sp, 20);
            closeButton.Click += (e, args) => CloseProductView();
            closeButton.SetText("X", TextView.BufferType.Normal);
            layout.AddView(closeButton);

            view.Visibility = ViewStates.Visible;
            IsAdvertViewVisible = true;
        }

        public void ShowProductDetails(LinearLayout parentView, AdProduct product, int id)
        {
            var layout = new LinearLayout(this); 
            layout.Orientation = Orientation.Vertical;

            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent);
            layout.LayoutParameters = layoutParams;

            var innerLayout = new LinearLayout(this);
            innerLayout.Orientation = Orientation.Vertical;
            innerLayout.LayoutParameters = layoutParams;
            innerLayout.Id = id;
            innerLayout.Visibility = ViewStates.Gone;

            var productTitle = new TextView(this);
            productTitle.SetTextSize(ComplexUnitType.Sp, 20);           
            productTitle.SetPadding(16, 26, 16, 26);
            productTitle.SetText(product.Name, TextView.BufferType.Normal);
            productTitle.Click += (e, args) => OnProductExpand(productTitle, innerLayout);
            layout.AddView(productTitle);                     

            var productDescription = new TextView(this);
            productDescription.SetTextSize(ComplexUnitType.Sp, 15);
            productDescription.SetPadding(10, 10, 10, 10);
            productDescription.SetText(product.Description, TextView.BufferType.Normal);
            innerLayout.AddView(productDescription);

            foreach (var item in product.Retailers)
            {
                var button = new Button(this);
                button.SetText(item.Name, TextView.BufferType.Normal);
                button.SetPadding(16, 16, 16, 16);
                button.SetTextSize(ComplexUnitType.Sp, 20);
                button.Click += (e, args) => OpenRetailerUrl(item.Url);
                innerLayout.AddView(button);
            }

            layout.AddView(innerLayout);
            parentView.AddView(layout);
        }

        private void OnProductExpand(TextView productTitle, LinearLayout detailsLayout)
        {
            detailsLayout.Visibility = detailsLayout.Visibility == ViewStates.Visible ?
                ViewStates.Gone : 
                ViewStates.Visible;            
        }

        private void OpenRetailerUrl(string url)
        {
            var browserIntent = new Intent(Intent.ActionView, AndroidNet.Uri.Parse(url));
            StartActivity(browserIntent);
        }

        private void CloseProductView()
        {
            var view = (ScrollView)FindViewById(Resource.Id.ad_view);
            view.Visibility = ViewStates.Gone;
            IsAdvertViewVisible = false;
        }
    }
}

