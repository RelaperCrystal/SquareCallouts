using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using SquareCallouts.Utils;

namespace SquareCallouts.Callouts
{
    [CalloutInfo("Gathering", CalloutProbability.Medium)]
    public class Gathering : CalloutBase
    {
        private List<Ped> peds = new List<Ped>();

        public bool Spotted { get; private set; }

        public override bool OnBeforeCalloutDisplayed()
        {
            Vector3 tempSpawn;
            if (!Common.TryGetNextPositionOnSidewalk(Common.GetRandomSpawnPoint(100, 500), out tempSpawn))
            {
                SpawnPoint = tempSpawn;
            }
            else
            {
                SpawnPoint = Common.GetRandomSpawnPoint(100, 500);
            }

            this.CalloutMessage = "Gathering";
            this.CalloutAdvisory = "Subject is violation of quarantine terms.";

            this.CalloutPosition = SpawnPoint;
            this.ShowCalloutAreaBlipBeforeAccepting(SpawnPoint, 100f);

            Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_MOVING_VIOLATION IN_OR_ON_POSITION ARREST_AND_QUESTION", SpawnPoint);
            
            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            int length = MathHelper.GetRandomInteger(1, 7);
            for (int i = 0; i < length; i++)
            {
                Ped ped = new Ped(SpawnPoint.Around(5f));
                ped.IsPersistent = true;
                ped.BlockPermanentEvents = true;
                ped.CanPlayAmbientAnimations = true;
                peds.Add(ped);
            }
            Blip = new Blip(SpawnPoint.Around2D(100f), 250f);
            Blip.IsRouteEnabled = true;
            Blip.Alpha = 0.5f;

            RadioHelper.DisplayDispatchNote("Arrest the suspects and return to the station for questioning.");
            Game.DisplayNotification("Suspects may be shown as ~r~dead~w~ if you arrest them by ~b~Stop The Ped.");
            RadioHelper.ShowRespondCode(EBackupResponseType.Code2);

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();
            if (!Spotted && Game.LocalPlayer.Character.Position.DistanceTo(SpawnPoint) < 50f)
            {
                Common.Output("Spotted");
                this.Spotted = true;
                Pursuit = Functions.CreatePursuit();
                foreach (var p in peds)
                {
                    Functions.AddPedToPursuit(Pursuit, p);
                }
                Pursuit.SetPursuitAsActive();
                Functions.RequestBackup(SpawnPoint, EBackupResponseType.Pursuit, EBackupUnitType.LocalUnit);
                Functions.RequestBackup(SpawnPoint, EBackupResponseType.Pursuit, EBackupUnitType.AirUnit);
                Functions.PlayScannerAudio("WE_HAVE CRIME_RESIST_ARREST TAZERS_AUTHORIZED");
                Blip.Delete();
            }

            if (Spotted)
            {
                if (!Functions.IsPursuitStillRunning(Pursuit))
                {
                    End();
                }
            }
        }

        public override void End()
        {
            base.End();
            foreach (var ped in peds)
            {
                // Prevent arrested ped flee
                if (ped && !Functions.IsPedArrested(ped))
                {
                    ped.Dismiss();
                }
            }
            if (Blip) Blip.Delete();
        }
    }
}
