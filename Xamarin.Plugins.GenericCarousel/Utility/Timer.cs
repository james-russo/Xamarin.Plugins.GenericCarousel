using System;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.Plugins.GenericCarousel
{
		internal delegate void TimerCallback(object state);

		internal sealed class Timer : CancellationTokenSource
		{
			public Timer(TimerCallback callback, object state, int delayTime, int period)
			{
				Task.Delay(delayTime, Token).ContinueWith(async (t, s) =>
					{
						var tuple = (Tuple<TimerCallback, object>)s;

						while (true)
						{
							if (IsCancellationRequested)
								break;

							tuple.Item1(tuple.Item2);

							await Task.Delay(period);
						}

					}, Tuple.Create(callback, state), CancellationToken.None,
					TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
					TaskScheduler.Default);
			}

			public new void Dispose()
			{
				base.Cancel();
			}
		}

}

