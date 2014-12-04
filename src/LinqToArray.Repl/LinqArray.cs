using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToArray
{
    public class LinqArray<T> : IEnumerable<T>
    {
        private T[] _array;
        private int _lowerBound;
        private int _upperBound;
        private bool _forwards;

        internal LinqArray(T[] source, int skipItems)
            : this(source,
            skipItems, source.Length - 1,
            true) { }

        internal LinqArray(T[] source, bool forwards)
            : this(source, 
            0, source.Length - 1, 
            forwards) { }

        protected LinqArray(T[] source, int lowerBound, int upperBound, bool forwards)
        {
            _array = source;
            _lowerBound = lowerBound;
            _upperBound = upperBound;

            _forwards = forwards;
        }

        public IEnumerator<T> _GetEnumerator()
        {
            int current = _forwards ? _lowerBound - 1 : _upperBound + 1;
            return new LinqArrayEnumerator<T>(_array, _lowerBound, _upperBound, current, _forwards);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _GetEnumerator();
        }

        public LinqArray<T> Skip(int items)
        {
            return new LinqArray<T>(_array,
                _forwards ? _lowerBound + items : _lowerBound,
                _forwards ? _upperBound : _upperBound - items,
                _forwards);
        }
    }

    internal class LinqArrayEnumerator<T> : IEnumerator<T>
    {
        private T[] _array;
        private int _lowerBound;
        private int _upperBound;
        private int _current;
        private bool _forwards;

        internal LinqArrayEnumerator(T[] source, int lowerBound, int upperBound, int current, bool forwards)
        {
            _array = source;
            _lowerBound = lowerBound;
            _upperBound = upperBound;
            _current = current;
            _forwards = forwards;
        }

        private T GetCurrent()
        {
            if ((_forwards && _current < _lowerBound) ||
                (!_forwards && _current > _upperBound)) throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");

            if ((_forwards && _current > _upperBound) ||
                (!_forwards && _current < _lowerBound)) throw new InvalidOperationException("Enumeration already finished");

            else return _array[_current];
        }

        public T Current
        {
            get { return GetCurrent(); }
        }

        public void Dispose()
        {
            
        }

        object System.Collections.IEnumerator.Current
        {
            get { return GetCurrent(); }
        }

        public bool MoveNext()
        {
            if (_forwards)
            {
                return ++_current <= _upperBound;
            }
            else
            {
                return --_current >= _lowerBound;
            }
        }

        public void Reset()
        {
            if (_forwards) _current = _lowerBound;
            else _current = _upperBound;
        }
    }

    public static class LinqArrayExtensions
    {
        public static LinqArray<T> Reverse<T>(this T[] array)
        {
            return new LinqArray<T>(array, false);
        }

        public static LinqArray<T> Skip<T>(this T[] array, int items)
        {
            return new LinqArray<T>(array, items);
        }
    }

}
