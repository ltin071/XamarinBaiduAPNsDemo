using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BaiduAPNsDemo.Droid
{
    [Activity(Label = "BaiduAPNsDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static readonly string BAIDU_DEBUG_TAG = "BAIDU_DEBUG";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Android.Util.Log.Info(BAIDU_DEBUG_TAG, "BaiduPushMessageReceiver.InitializeBaiduPushManager");
            BaiduPushMessageReceiver.InitializeBaiduPushManager();
            Android.Util.Log.Info(BAIDU_DEBUG_TAG, "BaiduPushMessageReceiver.InitializeBaiduPushManager Finished");

        }
    }
}