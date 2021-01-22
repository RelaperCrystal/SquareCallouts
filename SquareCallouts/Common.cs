using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace SquareCallouts
{
    internal static class Common
    {
        internal static void Output(string text)
        {
            Game.LogTrivial("Square: " + text);
        }
    }
}
