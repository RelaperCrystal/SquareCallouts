using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rage;
using Rage.Native;

namespace SquareCallouts
{
    internal static class Configuration
    {
        private static InitializationFile SettingsFile;
        internal static Keys FreezeKey { get; private set; }
        internal static bool EnableGroupOfPersonsCallout { get; private set; }

        internal static void Load()
        {
            SettingsFile = new InitializationFile("plugins\\LSPDFR\\SquareCallouts.ini");
            EnableGroupOfPersonsCallout = SettingsFile.ReadBoolean("Flags", "EnableGathering", true);
            FreezeKey = SettingsFile.ReadEnum<Keys>("Keys", "FreezeKey", Keys.Y);
        }


    }
}
