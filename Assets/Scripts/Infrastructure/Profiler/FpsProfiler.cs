namespace Infrastructure.Profiler
{
    using System.Text;
    using TMPro;
    using Unity.Profiling;
    using UnityEngine;

    public class FpsProfiler : MonoBehaviour
    {
        private string statsText = "";
        private ProfilerRecorder mainThreadTimeRecorder;
        private FrameTiming[] m_FrameTimings = new FrameTiming[1];
        private float lastTime;

        public TMP_Text DrawInfo { get; set; }

        static float GetRecorderFrameAverage(ProfilerRecorder recorder)
        {
            var samplesCount = recorder.Capacity;
            if (samplesCount == 0)
                return 0;

            float r = 0;
            unsafe
            {
                var samples = stackalloc ProfilerRecorderSample[samplesCount];
                recorder.CopyTo(samples, samplesCount);
                for (var i = 0; i < samplesCount; ++i)
                    r += samples[i].Value;
                r /= samplesCount;
            }

            return r;
        }

        void OnEnable()
        {
            mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);
        }

        void OnDisable()
        {
            mainThreadTimeRecorder.Dispose();
        }

        void Update()
        {
            if (Time.time - lastTime < 0.1)
                return;

            lastTime = Time.time;
            FrameTimingManager.CaptureFrameTimings();

            FrameTimingManager.GetLatestTimings((uint) m_FrameTimings.Length, m_FrameTimings);

            var sb = new StringBuilder(10);

            var time = GetRecorderFrameAverage(mainThreadTimeRecorder);
            if (time == 0)
                return;
            
            sb.AppendLine($"{1 / (GetRecorderFrameAverage(mainThreadTimeRecorder) * (1e-9f)):F0}");
            statsText = sb.ToString();
        }

        void OnGUI()
        {
            DrawInfo!.text = statsText;
        }
    }
}