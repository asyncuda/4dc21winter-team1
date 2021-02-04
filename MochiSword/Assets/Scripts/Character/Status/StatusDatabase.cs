using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Character.Status {
    [CreateAssetMenu(menuName = "Database/StatusDatabase")]
    public class StatusDatabase : ScriptableObject {
        [SerializeField] private List<StatusData> database = default;

        public StatusData GetData(CharacterType type) {
            return database.FirstOrDefault(x => x.Type == type);
        }
    }
}
