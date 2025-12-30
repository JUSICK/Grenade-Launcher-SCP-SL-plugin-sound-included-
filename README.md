# 💥 Grenade Thrower Plugin
### An SCP: Secret Laboratory Plugin (Exiled)

Turn the standard **COM-18** into a devastating **Grenade Launcher**.
This plugin modifies the weapon mechanics to shoot instant-detonation projectiles instead of bullets, complete with custom sound effects, recoil, and a jamming mechanic.

![Downloads](https://img.shields.io/github/downloads/JUSICK/Grenade-Launcher-SCP-SL-plugin-sound-included-/total?style=for-the-badge&color=blueviolet)
![Version](https://img.shields.io/badge/EXILED-9.0.0-blue?style=for-the-badge&logo=csharp)
![License](https://img.shields.io/github/license/JUSICK/Grenade-Launcher-SCP-SL-plugin-sound-included-?style=for-the-badge)

## ✨ Features
* **Custom Projectiles:** Shoots actual grenades (Frags, Flashbangs, or SCP-018) instead of bullets.
* **Custom Audio:** Plays a distinct `.ogg` sound file from your config folder upon firing.
* **Disposable Mechanic:** The weapon "jams" permanently after a set number of shots (default: 4), preventing reload spam.
* **Smart Logging:** Logs exactly who picks up the weapon with customizable messages.

## 🛠️ Installation

1.  **Download:** Get the latest `BigGun.dll` from the [Releases page](../../releases).
2.  **Plugin Folder:** Drop the `.dll` file into your server's plugin folder:
    `%appdata%\EXILED\Plugins`
3.  **Audio Setup:**
    * Place your custom sound file (must be `.ogg`) in your server configs folder.
    * **Note:** The file name must match the `sound_file_name` setting in your config (Default: `output.ogg`).
    * Ensure the file is **Mono** and **48kHz** (use Audacity to convert if needed).
4.  **Restart:** Restart the server to generate the config file.

## ⚙️ Configuration

You can edit these settings in your server's `config.yml` file located in `%appdata%\EXILED\Configs\[Port]-config.yml`.

| Setting | Type | Default | Description |
| :--- | :--- | :--- | :--- |
| `is_enabled` | `bool` | `true` | Toggles the plugin on or off. |
| `projectile_type` | `string` | `FragGrenade` | The projectile to launch. Options: `FragGrenade`, `Flashbang`, `Scp018`. |
| `configs` | `string` | *(Path)* | The directory path where your audio file is located. |
| `sound_file_name` | `string` | `output.ogg` | The exact name of the sound file to play (e.g., `boom.ogg`). |
| `sound_volume` | `float` | `1.7` | Volume of the shot sound. Recommended: `1.5` - `2.0`. |
| `available_shots` | `int` | `4` | How many shots the weapon has before it permanently jams. |
| `jammed_message` | `string` | *(See below)* | The hint shown when a player tries to shoot/reload an empty gun. |
| `jammed_message_duration` | `int` | `1` | How long (in seconds) the jam hint stays on screen. |
| `log_message` | `string` | *(See below)* | Message logged to server console on pickup. Supports `{player.Id}` and `{player.Nickname}`. |

## 🐛 Troubleshooting

### 🔊 No Sound?
If you hear the default gun sound instead of your custom explosion:
1.  **Check the Path:** Ensure the `configs` setting in your `config.yml` points **exactly** to the folder containing your `.ogg` file **named** `output.ogg`.
2.  **Check the Audio Format:** SCP:SL is very strict about audio. Your file **must** be:
    * Format: **.ogg**
    * Channels: **Mono** (Stereo files often fail silently)
    * Sample Rate: **48,000 Hz** (48kHz)
    * *Tip: You can fix this easily using free software like Audacity.*

### 🚫 Weapon Won't Reload?
* **This is intended behavior.** The weapon is designed to be "Disposable" (like the MicroHID) so players cannot spam grenades forever.
* **How to change it:** If you want players to reload, open your `config.yml` and increase the `available_shots` value (e.g., set it to `100`).

