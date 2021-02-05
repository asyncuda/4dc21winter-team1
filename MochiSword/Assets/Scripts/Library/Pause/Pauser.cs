using System.Collections.Generic;

namespace Library.Pause {
    public static class Pauser {
        private static List<IPausable> pausables = new List<IPausable>();
        private static bool isPausing;
        
        public static void Pause() {
            foreach (var pausable in pausables) {
                pausable.Pause();
            }
        }

        public static void Resume() {
            foreach (var pausable in pausables) {
                pausable.Resume();
            }
        }

        public static void AddList(IPausable pausable) {
            pausables.Add(pausable);
        }

        public static void RemoveList(IPausable pausable) {
            pausables.Remove(pausable);
        }

        public static void Clear() {
            pausables.Clear();
        }
    }
}
