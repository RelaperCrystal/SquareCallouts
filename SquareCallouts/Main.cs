using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Mod.API;
using Rage;

namespace SquareCallouts
{
    public class Main : Plugin
    {
        public override void Finally()
        {
            
        }

        public override void Initialize()
        {
            Common.Output("LSPDFR called Initialize");
            Functions.OnOnDutyStateChanged += Functions_OnOnDutyStateChanged;
        }

        private void Functions_OnOnDutyStateChanged(bool onDuty)
        {
            Configuration.Load();
            if (Configuration.EnableGroupOfPersonsCallout) Common.Output("WIP"); // TODO implement callout
        }
    }
}
