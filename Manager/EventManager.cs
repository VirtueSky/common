using System;

namespace Virtuesky.common
{
    public static partial class EventManager
    {
        #region GameSystem

        // Debug
        public static Action DebugChanged;

        // Currency
        public static Action SaveCurrencyTotal;

        public static Action CurrencyTotalChanged;

        // Level Spawn
        public static Action CurrentLevelChanged;

        // Setting
        public static Action MusicChanged;
        public static Action SoundChanged;

        public static Action VibrationChanged;

        // Ads
        public static Action RequestBanner;
        public static Action ShowBanner;
        public static Action RequestInterstitial;
        public static Action ShowInterstitial;
        public static Action RequestReward;

        public static Action ShowReward;

        // Other
        public static Action OnClickButton;
        public static Action CoinMove;
        public static Action<string> TrackClickButton;
        public static Action PurchaseFail;
        public static Action PurchaseSucceed;
        public static Action ClaimReward;
        public static Action CurrentSkinChanged;

        #endregion
    }
}