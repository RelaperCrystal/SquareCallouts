using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response;
using LSPD_First_Response.Mod.API;
using Rage;

namespace SquareCallouts.Utils
{
    internal static class RadioHelper
    {
        internal static void DisplayDispatchNote(string text)
        {
            Game.DisplayNotification(
                    "web_lossantospolicedept",
                    "web_lossantospolicedept",
                    "Dispatch",
                    "Note",
                    text
                );
        }

        internal static void DisplayDialogue(string speaker, string text)
        {
            Game.DisplayNotification($"~b~{speaker}~w~: {text}");
        }

        internal static void ShowRespondCode(EBackupResponseType type)
        {
            string respondType = "SQUARE_CODE3";
            string notification = "Code 2";
            switch (type)
            {
                case EBackupResponseType.SuspectTransporter:
                case EBackupResponseType.Code2:
                    respondType = "SQUARE_CODE2";
                    notification = "Code 2";
                    break;
                case EBackupResponseType.Pursuit:
                case EBackupResponseType.Code3:
                default:
                    respondType = "SQUARE_CODE3";
                    notification = "Code 3";
                    break;
            }

            GameFiber.StartNew( () =>
            {
                GameFiber.Sleep(3000);
                Functions.PlayScannerAudio(respondType);
                Game.DisplayNotification($"Respond ~r~{notification}");
            });

        }
    }
}
