using System.Drawing;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Design tokens from UI/02-Design-Tokens.md (approved Visual Design v1.1).
/// Milestone 1: color + typography constants for the shell only.
/// </summary>
internal static class UiTheme
{
    public static readonly Color Primary = ColorTranslator.FromHtml("#1E4B8F");
    public static readonly Color PrimaryHover = ColorTranslator.FromHtml("#163A6E");
    public static readonly Color Background = ColorTranslator.FromHtml("#F4F6F8");
    public static readonly Color Surface = ColorTranslator.FromHtml("#FFFFFF");
    public static readonly Color Text = ColorTranslator.FromHtml("#1F2933");
    public static readonly Color TextMuted = ColorTranslator.FromHtml("#6B7785");
    public static readonly Color Border = ColorTranslator.FromHtml("#D7DCE1");
    public static readonly Color Error = ColorTranslator.FromHtml("#C0392B");
    public static readonly Color SidebarBackground = ColorTranslator.FromHtml("#16324F");
    public static readonly Color SidebarText = ColorTranslator.FromHtml("#C7D3E0");
    public static readonly Color SidebarTextActive = Color.White;
    public static readonly Color SidebarAccent = ColorTranslator.FromHtml("#5B9BD5");
    public static readonly Color StatusStripBackground = ColorTranslator.FromHtml("#EEF1F4");
    public static readonly Color DisabledBackground = ColorTranslator.FromHtml("#ECEEF1");

    public static readonly Font ScreenTitle = new("Segoe UI", 14f, FontStyle.Bold);
    public static readonly Font SectionTitle = new("Segoe UI", 11f, FontStyle.Bold);
    public static readonly Font Body = new("Segoe UI", 10f, FontStyle.Regular);
    public static readonly Font Label = new("Segoe UI", 9f, FontStyle.Regular);
    public static readonly Font Button = new("Segoe UI", 10f, FontStyle.Bold);
    public static readonly Font SidebarItem = new("Segoe UI", 9.5f, FontStyle.Regular);
    public static readonly Font SidebarItemActive = new("Segoe UI", 9.5f, FontStyle.Bold);
    public static readonly Font SidebarGroup = new("Segoe UI", 8f, FontStyle.Bold);
    public static readonly Font StatusStrip = new("Segoe UI", 9f, FontStyle.Regular);

    public const int SidebarWidth = 220;
    public const int MinWindowWidth = 1024;
    public const int MinWindowHeight = 768;
}
