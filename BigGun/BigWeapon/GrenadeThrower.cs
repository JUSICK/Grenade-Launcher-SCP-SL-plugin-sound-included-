using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using MEC;
using System.Collections.Generic;
using UnityEngine;
using PlayerEvent = Exiled.Events.Handlers.Player;

//using AUdioPlayerAPI;

namespace BigGun.BigWeapon;

[CustomItem(ItemType.GunFSP9)]
public class GrenadeThrower : CustomItem 
{ 
        public override uint Id { get; set; } = BigGunPlugin.Instance.Config.CustimItemId;
        public override string Name { get; set; } = BigGunPlugin.Instance.Config.CustomItemName;
        public override string Description { get; set; } = BigGunPlugin.Instance.Config.Description;
        public override float Weight { get; set; } = BigGunPlugin.Instance.Config.Weight;
    public override ItemType Type { get; set; } = ItemType.GunFSP9;
    public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties() {Limit = 1}; 
    protected override void SubscribeEvents()
    {
        PlayerEvent.Shooting += OnShooting;
        PlayerEvent.ReloadingWeapon += OnReloading;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents()
    {
        PlayerEvent.Shooting -= OnShooting;
        PlayerEvent.ReloadingWeapon -= OnReloading;
        base.UnsubscribeEvents();
    }
    private void OnShooting(ShootingEventArgs ev)
    {
        if (!Check(ev.Item))
            return;

        ev.Player.ThrowGrenade(BigGunPlugin.Instance.Config.ProjectileType, true);
        Timing.CallDelayed(0.1f, () => 
        {
            CreateForPlayer(ev.Player);
        });
    }
    protected override void OnAcquired(Player player, Item item, bool displayMessage)
    {
        base.OnAcquired(player, item, displayMessage);
        string rawMessage = BigGunPlugin.Instance.Config.LogMessage;
        string finalMessage = rawMessage
            .Replace("{player.Id}", player.Id.ToString())
            .Replace("{player.Nickname}", player.Nickname);
        Log.Info(finalMessage);
        MEC.Timing.CallDelayed(0.1f, () =>
        {
            if (item is Firearm firearm)
            {
                firearm.PrimaryMagazine.MaxAmmo = 1;
                firearm.HitscanHitregModule.BaseDamage = 0;
                firearm.ClearAttachments();
                firearm.AddAttachment(AttachmentName.SoundSuppressor);
                firearm.AddAttachment(AttachmentName.ExtendedStock);
            }
        });
    }
    public void CreateForPlayer(Player player)
    {
        AudioPlayer audioPlayer = AudioPlayer.CreateOrGet($"Player {player.Nickname}", onIntialCreation: (p) =>
        {
            // Attach created audio player to player.
            p.transform.parent = player.GameObject.transform;

            // This created speaker will be in 3D space.
            Speaker speaker = p.AddSpeaker("Main", isSpatial: true, minDistance: 5f, maxDistance: 15f);

            // Attach created speaker to player.
            speaker.transform.parent = player.GameObject.transform;

            // Set local positino to zero to make sure that speaker is in player.
            speaker.transform.localPosition = Vector3.zero;
        });
        
        audioPlayer.AddClip("playersound", BigGunPlugin.Instance.Config.soundVolume);
    }

    private Dictionary<ushort, int> _gunReloadCounts = new Dictionary<ushort, int>();
    private void OnReloading(ReloadingWeaponEventArgs ev)
    {
        if (!Check(ev.Item))
            return;

            ushort serial = ev.Firearm.Serial;

        if (!_gunReloadCounts.ContainsKey(serial))
                _gunReloadCounts[serial] = 0;

        if (_gunReloadCounts[serial] >= BigGunPlugin.Instance.Config.AvailableShots)
            {
             ev.IsAllowed = false;
             ev.Player.ShowHint($"<b><color=red>{BigGunPlugin.Instance.Config.JammedMessage}</color></b>", BigGunPlugin.Instance.Config.JammedMessageDuration);
            }
        else _gunReloadCounts[serial]++;
    }
}

