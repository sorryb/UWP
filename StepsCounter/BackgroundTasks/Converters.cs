
using System.Collections.Generic;
using Windows.Devices.Sensors;
using Lumia.Sense;

namespace BackgroundTasks.Converters
{
    /// <summary>
    /// Extracts the step counts from pedometer readings or Lumia StepCountData
    /// </summary>
    public sealed class StepCountData
    {
        public uint RunningCount { get; private set; }
        public uint WalkingCount { get; private set; }
        public uint UnknownCount { get; private set; }

        public uint TotalCount
        {
            get
            {
                return RunningCount + WalkingCount + UnknownCount;
            }
        }

        /// <summary>
        /// Factory that creates a StepCountData object from PedometerReadings
        /// </summary>
        /// <param name="readings"></param>
        /// <returns></returns>
        public static StepCountData FromPedometerReadings(IReadOnlyList<PedometerReading> readings)
        {
            StepCountData steps = new StepCountData();
            // Get the most recent batch of 3 readings (one per StepKind)
            for (int i = 0; i < readings.Count && i < 3; i++)
            {
                var reading = readings[readings.Count - i - 1];
                switch (reading.StepKind)
                {
                    case PedometerStepKind.Running:
                        steps.RunningCount = (uint)reading.CumulativeSteps;
                        break;
                    case PedometerStepKind.Walking:
                        steps.WalkingCount = (uint)reading.CumulativeSteps;
                        break;
                    case PedometerStepKind.Unknown:
                        steps.UnknownCount = (uint)reading.CumulativeSteps;
                        break;
                    default:
                        break;
                }
            }

            // Subtract the counts from the earliest batch of 3 readings (one per StepKind)
            for (int i = 0; i < readings.Count && i < 3; i++)
            {
                var reading = readings[i];
                switch (reading.StepKind)
                {
                    case PedometerStepKind.Running:
                        steps.RunningCount -= (uint)reading.CumulativeSteps;
                        break;
                    case PedometerStepKind.Walking:
                        steps.WalkingCount -= (uint)reading.CumulativeSteps;
                        break;
                    case PedometerStepKind.Unknown:
                        steps.UnknownCount -= (uint)reading.CumulativeSteps;
                        break;
                    default:
                        break;
                }
            }

            return steps;
        }

        /// <summary>
        /// Factory that creates a StepCountData object from a Lumia StepCount
        /// </summary>
        /// <param name="stepCount"></param>
        /// <returns></returns>
        public static StepCountData FromLumiaStepCount(StepCount stepCount)
        {
            StepCountData steps = new StepCountData();

            steps.RunningCount = stepCount.RunningStepCount;
            steps.WalkingCount = stepCount.WalkingStepCount;
            steps.UnknownCount = 0;

            return steps;
        }
    }
}
