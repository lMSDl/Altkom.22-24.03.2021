using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class NumbersHolder
    {
        private readonly int[] _data;
        public int Count { get; private set; }

        public const int SIZE = 16;

        public NumbersHolder()
        {
            _data = new int[SIZE];
        }

        public void Add(int element)
        {
            if (Count == _data.Length)
            {
                throw new InvalidOperationException();
            }

            _data[Count] = element;
            Count++;
        }

        public Task RemoveAsync()
        {
            return Task.Run(() =>
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException();
                }

                Count--;
            });
        }

        public int Sum()
        {
            return _data.Take(Count).Sum();
        }

        public void Sort()
        {
            var swapped = true;

            while (swapped)
            {
                swapped = false;

                for (var i = 1; i < Count; i++)
                {
                    if (_data[i - 1] > _data[i])
                    {
                        var temp = _data[i - 1];
                        _data[i - 1] = _data[i];
                        _data[i] = temp;
                        swapped = true;
                    }
                }
            }
        }

        public int[] Fetch()
        {
            return _data.Take(Count).ToArray();
        }
    }
}
