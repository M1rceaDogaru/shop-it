# shop-it
An Android app in Xamarin that allows users to identify and buy products by pointing the phone camera at them

## ShopIt.Android
A Xamarin ap targeting the Android platform. It presents the user with a camera with a QR scanner. When pointed at a valid physical advert it will extract the product identifier and call the ShopIt.Server to retrieve details about the product and a list of retailer URLs then present the user with buttons to open pages where the product can be bought.

## ShopIt.Server
A .NET Core MVC web server with an endpoint that receives a product identifier and returns product details and a list of retailers with URLs where the product can be bought.
