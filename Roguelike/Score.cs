using System;

namespace Roguelike
{
    public class Score: IComparable<Score>
    {
        public string Name{get;}
        public int NewScore{get;}

        public Score(string name, int newscore)
        {
            Name = name;
            NewScore = newscore;
        }


        /// <summary>
        /// Method made by the teacher
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Score other)
        {
            if (other == null) return -1;
            return other.NewScore - NewScore;
        }

    }
}