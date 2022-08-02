namespace captive_portal_api.Models;
public static class Commands
{
    public const string ArchiveAllAlarms = "archive-all-alarms";
    public const string AddSite = "add-site	";
    public const string DeleteSite = "delete-site";
    public const string UpdateSite = "update-site"; 
    public const string GetAdmins = "get-admins"; 
    public const string MoveDevice = "move-device"; 
    public const string DeleteDevice = "delete-device";
    public const string BlockSta = "block-sta";
    public const string UnblockSta = "unblock-sta";
    public const string KickSta = "kick-sta";
    public const string ForgetSta = "forget-sta";
    public const string UnauthorizeGuest = "unauthorize-guest";
    public const string AuthorizeGuest = "authorize-guest";
    public const string Adopt = "adopt";
    public const string Restart = "restart";
    public const string ForceProvision = "force-provision";
    public const string PowerCycle = "power-cycle";
    public const string Speedtest = "speedtest";
    public const string SpeedtestStatus = "speedtest-status";
    public const string SetLocate = "set-locate";
    public const string UnsetLocate = "unset-locate";
    public const string Upgrade = "upgrade";
    public const string UpgradeExternal = "upgrade-external";
    public const string Migrate = "migrate";
    public const string CancelMigrate = "cancel-migrate";
    public const string SpectrumScan = "spectrum-scan";
    public const string ListBackups = "list-backups";
    public const string DeleteBackup = "delete-backup"; 
    public const string Backup = "backup"; 
    public const string ClearDpi = "clear-dpi"; 
 
}
	/*
     desc = Descriptive name ( required ), name = shortname ( in the URL )
     name = short name ( required )
     desc = Descriptive name ( required )
     List all administrators and permission for this site
     mac = device mac ( required ), site_id = 24 digit id ( required )
     mac = device mac ( required )
     mac = client mac ( required )
     mac = client mac ( required )
     Disconnect: mac = client mac (required )
     Forget a client ( controller 5.9.x only )
    Unauthorize a client device, mac = client mac (required)
     mac = device mac ( required )
     mac = device mac ( required )
     mac = device mac ( required )
     mac = switch mac ( required ), port_idx = PoE port to cycle ( required )
     Start a speed test
    get the current state of the speed test
     mac = device mac ( required ) blink unit to locate
     mac = device mac ( required ) led to normal state
     mac = device mac ( required ) upgrade firmware
    mac = device mac ( required ), url = firmware URL ( required )
     mac = device mac ( required ), inform_url = New Inform URL to push to device (required)
     mac = device mac ( required )
     mac = device mac ( ap only, required ) trigger RF scan
     list of autobackup files
     filename ( required )
     create a backup. This appears to backup to a fixed location in the filesystem
     resets the site wide DPI counters
     */