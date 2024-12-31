using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDLL
{
    /// <summary>
    /// Contains enums for various topics and difficulty levels.
    /// </summary>
    public class Enum
    {
        /// <summary>
        /// Enum representing different quiz topics.
        /// </summary>
        public enum Topic
        {
            Mathematics,
            Science,
            History,
            Geography
        }

        /// <summary>
        /// Enum representing different difficulty levels for quizzes.
        /// </summary>
        public enum Difficulty
        {
            Easy,
            Intermediate,
            Hard
        }
    }
}
