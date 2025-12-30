using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.ComponentModel;
namespace BigGun;
public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;

    [Description("What ProjectileType does the Grenade thrower use? EX: \"FragGrenade\", \"Flashbang\" etc.")]
    public ProjectileType ProjectileType { get; set; } = ProjectileType.FragGrenade;

    [Description("The directory path to the .ogg file. (Must be .ogg & 48 kHz & mono)")]
    public static string Configs { get; set; } = @"C:\Users\JUICE\AppData\Roaming\EXILED\Configs";

    [Description("Recommended volume is 1.5 - 2.0")]
    public float soundVolume { get; set; } = 1.7f;

    [Description("Hint that is showed to the player, when the gun runs out of ammo")]
    public string JammedMessage { get; set; } = "The mechanism jammed!";

    [Description("For how lond does the player see the hint?")]
    public int JammedMessageDuration { get; set; } = 1;

    [Description("How many shots does the gun have before it's getting jammed?")]
    public int AvailableShots { get; set; } = 4;

    [Description("What Log.Info writes to the log when the player gets the grenade thrower by picking it up/acquiring")]
    public string LogMessage { get; set; } = "Grenade Thrower was added to the ({player.Id}) {player.Nickname}`s inventory.";
}
