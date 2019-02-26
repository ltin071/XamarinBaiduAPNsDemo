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
using Com.Baidu.Android.Pushservice;

namespace BaiduAPNsDemo.Droid
{
    [BroadcastReceiver(Name = "com.company.BaiduAPNsDemo.BaiduPushMessageReceiver")]
    [IntentFilter(
    new string[]
    {
        "com.baidu.android.pushservice.action.MESSAGE",
        "com.baidu.android.pushservice.action.RECEIVE",
        "com.baidu.android.pushservice.action.notification.CLICK"
    }
)]
    public class BaiduPushMessageReceiver : PushMessageReceiver
    {
        public static string mChannelId, mUserId;

        /// <summary>
        /// Call this to initialize Baidu's push notification service for this device.
        /// 调用此功能开启百度推送通知服务。
        /// </summary>
        public static void InitializeBaiduPushManager()
        {
            string baiduPushApiKey = GetMetaDataValueByName("baidu_api_key");

            CreateBaiduPushNotificationBuilder();

            if (!string.IsNullOrWhiteSpace(baiduPushApiKey))
                PushManager.StartWork(Xamarin.Forms.Forms.Context, PushConstants.LoginTypeApiKey, baiduPushApiKey);
        }

        /// <summary>
        /// Gets the value of meta data from AndroidManifest.xml, based on the given name.
        /// 获取AndroidManifest.xml的对应meta data name的值。
        /// </summary>
        /// <param name="metaDataName"></param>
        /// <returns></returns>
        private static string GetMetaDataValueByName(string metaDataName)
        {
            Bundle metaData = null;
            string metaDataValue = "";
            Context context = Xamarin.Forms.Forms.Context;
            ApplicationInfo ai = context.PackageManager.GetApplicationInfo(context.PackageName, PackageInfoFlags.MetaData);

            if (ai != null)
                metaData = ai.MetaData;

            if (metaData != null)
                metaDataValue = metaData.GetString(metaDataName);

            return metaDataValue;
        }

        /// <summary>
        /// Set the default values for the push notification template.
        /// Note: The 'builderId' value used in the template should be the same as the 'notification_builder_id'
        /// value in the JSON payload, when sending the message.
        /// 设置推送通知的默认值。
        /// 注意：“builderId”的值必须对应服务器上的那个JSON“notification_builder_id”变数的值。
        /// </summary>
        private static void CreateBaiduPushNotificationBuilder()
        {
            // You can edit these values as you wish.
            // 随便你改变这些值。
            BasicPushNotificationBuilder builder = new BasicPushNotificationBuilder();
            builder.SetNotificationFlags((int)NotificationFlags.AutoCancel | (int)NotificationFlags.HighPriority | (int)NotificationFlags.ShowLights | (int)NotificationFlags.GroupSummary);
            builder.SetNotificationDefaults((int)NotificationDefaults.Lights | (int)NotificationDefaults.Vibrate);
            //builder.SetStatusbarIcon(Resource.Drawable.icon);
            builder.SetNotificationTitle("MY APP"); // Title at the top of the notification/通知上部分的标题

            PushManager.SetNotificationBuilder(Xamarin.Forms.Forms.Context, 1, builder);
        }

        /// <summary>
        /// Sends the push notification channel id to the server.
        /// 把channel id发送到服务器。
        /// </summary>
        /// <param name="channelId">User's push token/用户的推送通知token。</param>
        private void SendRegistrationToAppServer(string channelId)
        {
            // Here you should send channelId to your server and save it.
            // 这里应该把channelId发送到服务器并保存一下。
        }

        /// <summary>
        /// This method will be called after calling PushManager.StarWork(). The channelId should be sent to your
        /// server; this value can be used to send push notifictions to this user.
        /// 调用PushManager.StartWork()以后，此功能要提供channelId。因为channelId让你发送本用户给通知，所以你应该把channelId
        /// 保存在服务器里。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errorCode"></param>
        /// <param name="appid"></param>
        /// <param name="userId"></param>
        /// <param name="channelId"></param>
        /// <param name="requestId"></param>
        public override void OnBind(Context context, int errorCode, string appid, string userId, string channelId, string requestId)
        {
            mChannelId = channelId;
            mUserId = userId;
            Android.Util.Log.Info("BaiduAPNsDemo", "channelId:" + channelId);
            SendRegistrationToAppServer(channelId);
        }

        public override void OnDelTags(Context context, int errorCode, IList<string> sucessTags, IList<string> failTags, string requestId) { }

        public override void OnListTags(Context context, int errorCode, IList<string> tags, string requestId) { }

        public override void OnMessage(Context context, string message, string customContentString) { }

        public override void OnNotificationArrived(Context context, string title, string description, string customContentString) { }

        public override void OnNotificationClicked(Context context, string title, string description, string customContentString) { }

        public override void OnSetTags(Context context, int errorCode, IList<string> sucessTags, IList<string> failTags, string requestId) { }

        public override void OnUnbind(Context context, int errorCode, string requestId) { }
    }
}