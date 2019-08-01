using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace kSoft.Core.Droid
{
    [Activity(Label = "kSoft.Core", 
        Icon = "@mipmap/icon", 
        Theme = "@style/SplashTheme", 
        MainLauncher = true, 
        NoHistory =true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CallMainActivity();           
        }

       private void CallMainActivity()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}