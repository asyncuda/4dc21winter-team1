using Library.Audio;
using UnityEngine;
using Zenject;

namespace Library.Installers {
    [CreateAssetMenu(menuName = "Installers/DatabaseInstaller")]
    public class DatabaseInstaller : ScriptableObjectInstaller<DatabaseInstaller> {
        [SerializeField] private SoundDatabase soundDatabase = default;

        public override void InstallBindings() {
            Container.BindInstance(soundDatabase).IfNotBound();
        }
    }
}
