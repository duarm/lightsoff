using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kurenaiz.Utilities.Types
{
    public class SafeArray<T> : IEnumerable
    {
        private readonly T[] _array;

        public SafeArray (int capacity)
        {
            _array = new T[capacity];
        }

        public T this [int index]
        {
            get => index < _array.Length ? _array[index] : default(T);
            set => _array[index] = value;
        }

        public IEnumerator GetEnumerator ()
        {
            return _array.GetEnumerator ();
        }
    }

    public class Safe2DArray<T> : IEnumerable
    {
        private readonly T[,] _array;

        public Safe2DArray (int width, int height)
        {
            _array = new T[width, height];
        }

        public T this [int x, int y]
        {
            get => WithinBounds(x,y) ? _array[x,y] : default(T);
            set => _array[x, y] = value;
        }

        public bool WithinBounds (int x, int y)
        {
            if (x < 0 || y < 0)
                return false;
            return x < _array.GetLength (0) && (y < _array.GetLength (1) ? true : false);
        }

        public int GetLength (int dimension)
        {
            return _array.GetLength (dimension);
        }

        public IEnumerator GetEnumerator ()
        {
            return _array.GetEnumerator ();
        }
    }
}