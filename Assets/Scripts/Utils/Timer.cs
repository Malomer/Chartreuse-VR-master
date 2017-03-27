using System.Collections;
using UnityEngine;

namespace Utils {

    public class Timer : MonoBehaviour {

        // Default starting time in seconds
        public const float DefaultStartingTime = 100;

        // Timer static instance
        public static Timer Instance;

        // Coroutine tick variable to keep track of countdown.
        private Coroutine TickCoroutine;

        // Current time remaining
        private float Time;

        // Private constructor since this is a singleton.
        private Timer() { }

        void Awake() {

            if (Instance == null) {
                Instance = this;
            }
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void StartTimer() {
            TickCoroutine = StartCoroutine(OnTick());
        }

        public void StopTimer() {
            StopCoroutine(TickCoroutine);
        }

        public void ResetTimer() {
            Time = DefaultStartingTime;
        }

        public string GetFormattedTime() {
            return string.Format("{0:D2}:{1:D2}", Time / 60, Time % 60);
        }

        private IEnumerator OnTick() {
            while(true) {
                Time--;

                if(Time == 0) {
                    //EndGame(GameCondition.TimeRanOut);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
