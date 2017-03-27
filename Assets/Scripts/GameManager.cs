using UnityEngine;

namespace Assets.Scripts {

    public class GameManager : MonoBehaviour {

        public static GameManager Instance;

        // Private constructor since this is a singleton.
        private GameManager() { }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void EndGame() {

        }
    }
}