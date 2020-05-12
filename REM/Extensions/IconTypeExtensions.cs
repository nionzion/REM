using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REM
{

    public static class IconTypeExtensions
    {
        public static string ToFontAwesome(this IconType type)
        {
            // Return a FontAwesome string based on the icon type
            switch (type)
            {
                case IconType.None:
                    return String.Empty;

                case IconType.Settings:
                    return "\uf085";

                case IconType.ArrowLeft:
                    return "\uf0a8";

                case IconType.Coffee:
                    return "\uf0f4";

                case IconType.Copyright:
                    return "\uf1f9";

                case IconType.Love:
                    return "\uf004";

                case IconType.MenuBars:
                    return "\uf0c9";

                case IconType.Notebook:
                    return "\uf109";

                case IconType.Info:
                    return "\uf05a";

                case IconType.Book:
                    return "\uf02d";

                case IconType.Screen:
                    return "\uf26c";

                case IconType.Calculator:
                    return "\uf1ec";

                case IconType.Newspaper:
                    return "\uf1ea";

                case IconType.Error:
                    return "\uf2d4";

                case IconType.TaskList:
                    return "\uf0cb";

                case IconType.Sitemap:
                    return "\uf0e8";

                case IconType.Calendar:
                    return "\uf272";

                case IconType.Table:
                    return "\uf022";

                case IconType.Home:
                    return "\uf015";

                case IconType.Agent:
                    return "\uf0c0";
                // If none found, return null
                default:
                    return null;
            }
        }
    }
}
