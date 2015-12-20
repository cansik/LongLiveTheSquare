using System;

namespace U4LongLiveTheSquare
{
	public class StopWatch
	{
		public DateTime StartTime { get; private set; } = DateTime.Now;

		public DateTime EndTime { get; private set; } = DateTime.Now;

		public TimeSpan Duration { get { return EndTime - StartTime; } }

		public StopWatch ()
		{
		}

		public void Start ()
		{
			StartTime = DateTime.Now;
		}

		public void Stop ()
		{
			EndTime = DateTime.Now;
		}
	}
}

