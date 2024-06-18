
using G20.Core.Enums;

namespace G20.Service.Messages
{
    public static class MessageDelayPeriodExtensions
    {
        /// <summary>
        /// Returns message delay in hours
        /// </summary>
        /// <param name="period">Message delay period</param>
        /// <param name="value">Value of delay send</param>
        /// <returns>Value of message delay in hours</returns>
        public static int ToHours(this MessageDelayPeriodEnum period, int value)
        {
            return period switch
            {
                MessageDelayPeriodEnum.Hours => value,
                MessageDelayPeriodEnum.Days => value * 24,
                _ => throw new ArgumentOutOfRangeException(nameof(period)),
            };
        }
    }
}
