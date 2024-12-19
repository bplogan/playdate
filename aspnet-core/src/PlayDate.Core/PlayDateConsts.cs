using PlayDate.Debugging;

namespace PlayDate
{
    public class PlayDateConsts
    {
        public const string LocalizationSourceName = "PlayDate";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "a8c822a8da594cdab78b4bfe7b5d1f12";
    }
}
