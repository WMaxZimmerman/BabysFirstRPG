using System.Collections.Generic;
using System.Linq;

namespace BabysFirstRPG.Game.Game
{
    public static class Settings
    {
        private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static List<char> Alphabet => _alphabet.Select(a => a).ToList();
    }
}
