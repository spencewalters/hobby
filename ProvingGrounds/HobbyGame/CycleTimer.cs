using System;
using System.Diagnostics;

namespace HobbyGame {
    class CycleTimer {
        private Stopwatch stopWatch;
        private UInt32 count;
        public double CyclesPerSecond { get; internal set; }
        public UInt32 SecondsPerCalculation; 

        public CycleTimer() {
            SecondsPerCalculation = 1;
            CyclesPerSecond = 0;
            count = 0;
            stopWatch = Stopwatch.StartNew();
        }

        private void ResetWatch() {
            count = 0;
            stopWatch.Restart();
        }

        public void CountCycle() {
            count++;
            if (stopWatch.Elapsed.TotalSeconds > SecondsPerCalculation) {
                CyclesPerSecond = count / stopWatch.Elapsed.TotalSeconds;
                ResetWatch();
            }
        }
        
    }
}
