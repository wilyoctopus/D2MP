using D2MP.Models.Exceptions;

namespace D2MP.Services.Utils
{
    public static class Retry
    {
        public async static Task<T> Do<T>(
            Func<Task<T>> action,
            TimeSpan retryInterval,
            CancellationToken cancellationToken,
            int maxAttemptCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return default(T);

                try
                {
                    Thread.Sleep(retryInterval);

                    var result = await action();

                    if (result != null)
                        return result;
                    else
                        maxAttemptCount++;
                }
                catch (ServiceUnavailableException ex)
                {
                    maxAttemptCount++;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            
            // TODO: logging
            throw new AggregateException(exceptions);
        }
    }
}
