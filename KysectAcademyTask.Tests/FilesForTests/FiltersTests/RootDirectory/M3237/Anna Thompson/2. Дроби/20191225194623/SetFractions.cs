using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace LABA_2
{
    public class SetFractions
    {
        private List<RationalFraction> _list { get; set; }
        private RationalFraction _max { get; set; }
        private RationalFraction _min { get; set; }
        private Dictionary<string, int> _max_count_cache { get; set; }
        private Dictionary<string, int> _min_count_cache { get; set; }

        public SetFractions()
        {
            _list = new List<RationalFraction>();
            _max = null;
            _min = null;
            _max_count_cache = new Dictionary<string, int>();
            _min_count_cache = new Dictionary<string, int>();
        }

        /// <summary>
        /// Добавление дроби
        /// </summary>
        /// <param name="Up"></param>
        /// <param name="Down"></param>
        public void Add(double Up, double Down)
        {
            Add(new RationalFraction(Up, Down));
        }
        
        public void Add(RationalFraction f)
        {
            _list.Add(f);
            _max = null;
            _min = null;
            _max_count_cache = new Dictionary<string, int>();
            _min_count_cache = new Dictionary<string, int>();
        }

        /// <summary>
        /// Получить список дробей
        /// </summary>
        /// <returns></returns>
        public List<RationalFraction> GetFractions()
        {
            return _list;
        }

        /// <summary>
        /// Максимальная дробь в наборе
        /// </summary>
        /// <returns></returns>
        public RationalFraction Max()
        {
            if (_max != null)
                return _max;

            RationalFraction new_max = null;
            _list.ForEach(x =>
            {
                if (new_max == null || x > new_max)
                    new_max = x;
            });

            _max = new_max;

            return _max;
        }

        /// <summary>
        /// Минимальная дробь в наборе
        /// </summary>
        /// <returns></returns>
        public RationalFraction Min()
        {
            if (_min != null)
                return _min;

            RationalFraction new_min = null;
            _list.ForEach(x =>
            {
                if (new_min == null || x < new_min)
                    new_min = x;
            });

            _min = new_min;

            return _min;
        }

        /// <summary>
        /// Кол-во дробей в наборе больше заданной
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int MoreCount(RationalFraction data)
        {
            if (_max_count_cache.ContainsKey(data.ToString()))
                return _max_count_cache[data.ToString()];

            var count = (from rf in _list
                         where rf > data
                        select rf).Count();

            _max_count_cache.Add(data.ToString(), count);

            return count;
        }

        /// <summary>
        /// Кол-во дробей в наборе меньше заданной
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SmallerCount(RationalFraction data)
        {
            if (_min_count_cache.ContainsKey(data.ToString()))
                return _min_count_cache[data.ToString()];

            var count = (from rf in _list
                         where rf < data
                         select rf).Count();

            _min_count_cache.Add(data.ToString(), count);

            return count;
        }     

        public override string ToString()
        {
            var answer = "SetFractions:\n";
            _list.ForEach(x =>
            {
                answer += x.ToString() + "\n";
            });
            return answer;
        }

        /// <summary>
        /// Загрузка из файла
        /// </summary>
        /// <param name="location"></param>
        public void LoadFromFile(string location)
        {
            try
            {
                var str = File.ReadAllText(location);
                _list = JsonConvert.DeserializeObject<List<RationalFraction>>(str);
            }
            catch (Exception ex)
            {
                throw new Exception("Error with deserialize json");
            }
        }
        
        /// <summary>
        /// Сохранение в файл
        /// </summary>
        /// <param name="location"></param>
        public void SaveToFile(string location)
        {
            try
            {
                File.WriteAllText(location, JsonConvert.SerializeObject(_list));
            }
            catch (Exception ex)
            {
                throw new Exception("Error with serialize SetFractions");
            }
        }
    }
}
