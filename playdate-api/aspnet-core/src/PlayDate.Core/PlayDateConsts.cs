using PlayDate.Debugging;

namespace PlayDate;

public class PlayDateConsts
{
    public const string LocalizationSourceName = "PlayDate";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3c83af0f41294b8bb5e1da723d55a64b";
    
}
