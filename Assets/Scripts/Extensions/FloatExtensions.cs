namespace Extensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Remaps a float from one range to another
        /// </summary>
        /// <param name="value">the value to remap</param>
        /// <param name="inputFrom">input range min</param>
        /// <param name="inputTo">input range max</param>
        /// <param name="outputFrom">output range min</param>
        /// <param name="outputTo">output range max</param>
        /// <returns>the remapped float</returns>
        public static float Map(this float value, float inputFrom, float inputTo, float outputFrom, float outputTo)
        {
            return (value - inputFrom) / (inputTo - inputFrom) * (outputTo - outputFrom) + outputFrom;
        }
    }
}
