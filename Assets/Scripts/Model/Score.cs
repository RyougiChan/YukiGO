using Newtonsoft.Json;
using System;

namespace Assets.Scripts.Model
{
    public class Score : IComparable<Score>
    {
        public string value;
        public DateTime time;

        public Score()
        {

        }

        public Score(string value)
        {
            this.value = value;
            this.time = DateTime.Now;
        }

        public Score(string value, DateTime time)
        {
            this.value = value;
            this.time = time;
        }

        public int CompareTo(Score s)
        {
            if (s == null)
                return 1;

            else
                return Convert.ToInt32(this.value).CompareTo(Convert.ToInt32(s.value));
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
