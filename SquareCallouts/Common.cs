using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Mod.API;
using Rage;
using Rage.Native;

namespace SquareCallouts
{
    internal static class Common
    {
        internal static void Output(string text)
        {
            Game.LogTrivial("Square: " + text);
        }

        internal static Vector3 GetRandomSpawnPoint(float min, float max)
        {
            return World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(min, max));
        }

        internal static bool TryGetNextPositionOnSidewalk(Vector3 position, out Vector3 result)
        {
            Vector3 resultTemp = Vector3.Zero;
            bool success = NativeFunction.Natives.GET_SAFE_COORD_FOR_PED<bool>(position.X, position.Y, position.Z, true, ref resultTemp, 0);
            result = resultTemp;
            return success;
        }

        internal static void SetPursuitAsActive(this LHandle pursuit)
        {
            Functions.SetPursuitAsCalledIn(pursuit);
            Functions.SetPursuitCopsCanJoin(pursuit, true);
            Functions.SetPursuitIsActiveForPlayer(pursuit, true);
        }
    }
}
