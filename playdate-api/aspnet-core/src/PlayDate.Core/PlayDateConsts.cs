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
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "ad6050c69baa4ff1a00d4e8168771659";
}
