using UnityEngine;

namespace LSL4Unity.Utils
{
    /// <summary>
    /// A singleton to provide a dedicated timestamp for each update call: FixedUpdate, Update and LateUpdate.
    /// Each sample provide by a Unity app should have the same timestamp.
    /// </summary>
    /// <remarks>
    /// Important! Make sure that the Update methods within the class are called before the default execution order!
    /// The ScriptOrder attribute takes care of that for the user. 
    /// </remarks>
    [ScriptOrder(-1000)]
    public sealed class TimeSync : MonoBehaviour
    {
        [Tooltip("Flag for making the script not destroyable on load.")] [SerializeField]
        private bool dontDestroyOnLoad = false;

        /// <summary>
        /// Singleton instance of TimeSync class
        /// </summary>
        public static TimeSync Instance { get; private set; }

        /// <summary>
        /// Timestamp at FixedUpdate
        /// </summary>
        public double FixedUpdateTimestamp { get; private set; }

        /// <summary>
        /// Timestamp at Update
        /// </summary>
        public double UpdateTimestamp { get; private set; }

        /// <summary>
        /// Timestamp at LateUpdate
        /// </summary>
        public double LateUpdateTimestamp { get; private set; }


        private void Awake()
        {
            // Create Singleton or destroy if an instance already exists
            if (Instance && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;

                // Set don't destroy on load based on flag
                if (dontDestroyOnLoad) DontDestroyOnLoad(this);
            }
        }

        private void FixedUpdate()
        {
            FixedUpdateTimestamp = LSL.LSL.local_clock();
        }

        private void Update()
        {
            UpdateTimestamp = LSL.LSL.local_clock();
        }

        private void LateUpdate()
        {
            LateUpdateTimestamp = LSL.LSL.local_clock();
        }
    }
}