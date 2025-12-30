
using Exiled.API.Features;
using System.IO;
using Exiled.CustomItems.API.Features;
using server = Exiled.Events.Handlers.Server;

namespace BigGun;
public class BigGunPlugin : Plugin<Config>
{
    public override string Name => "Bomb Gun";
    public override string Author => "JUICE";
    public override System.Version Version => new(1, 0, 0);
    public static BigGunPlugin Instance { get; private set; }
    public override void OnEnabled() => OnRegistered();
    public override void OnDisabled() => OnUnregistered();
    string soundPath;
    private void OnRegistered()
    {
        Instance = this;
        soundPath = Path.Combine(Paths.Configs, BigGunPlugin.Instance.Config.soundFileName);
        CustomItem.RegisterItems();
        server.WaitingForPlayers += NotifyLog;
        server.WaitingForPlayers += OnPluginLoad;
        base.OnEnabled();
    }
    private void OnUnregistered()
    {
        CustomItem.UnregisterItems();
        server.WaitingForPlayers -= NotifyLog;
        server.WaitingForPlayers -= OnPluginLoad;
        base.OnDisabled();
        Instance = null;
    }
    void NotifyLog()
    {
        Log.Info("BigGun plugin is enitiated successfully. Thank You!");
    }
    public void OnPluginLoad()
    {
        var clip = AudioClipStorage.LoadClip(soundPath, "playersound");
        Log.Info($"Clip loaded successfully: {clip != null}");
    }

}

