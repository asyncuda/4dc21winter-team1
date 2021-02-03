using System.Diagnostics;

namespace Library {
    public class Debugger {
        /// <summary>
        /// Debug.LogをUnity Editorでのみ表示する
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o) {
            UnityEngine.Debug.Log(o);
        }
    }
}
