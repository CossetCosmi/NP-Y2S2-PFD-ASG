using System;
using System.Linq;

namespace PFD.Component
{
    internal sealed class SpeechSuspiciousDetector
    {
        #region Properties
        private string[] KeywordArray { get; } = new string[] {
            "help", "helps",
            "negative",
            "support", "supports",
            "assist", "assists",
            "mayday",
            "hurt", "hurts",
            "wait",
            "die",
            "moment",
            "relax",
            "threaten"
        };
        #endregion Properties

        #region Singleton
        private SpeechSuspiciousDetector() { }

        public static SpeechSuspiciousDetector Instance => Nested.Instance;

        private class Nested
        {
            static Nested() { }

            internal static readonly SpeechSuspiciousDetector Instance = new SpeechSuspiciousDetector();
        }
        #endregion Singleton

        public bool IsSuspicious(string sentence)
        {
            string[] wordArray = sentence.Split(" ");
            string[] keywordArray = wordArray.Where(word => KeywordArray.Contains(word)).ToArray();

            return keywordArray.Length > 1;
        }
    }
}
