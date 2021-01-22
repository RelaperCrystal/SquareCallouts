using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace SquareCallouts.Callouts
{
    public abstract class CalloutBase : Callout
    {
        internal Vector3 SpawnPoint { get; set; }
        internal LHandle Pursuit { get; set; }
        internal Blip Blip { get; set; }
    }
}
