using System.Collections.Generic;

namespace Deckbuilder
{
    public class ListStack<T>
    {
        private List<T> _list;
        public int Count => _list.Count;
        
        public ListStack(List<T> list)
        {
            if (list.Count > 0)
                _list = new List<T>(list);
            else _list = new List<T>();
        }

        public T Get(int i)
        {
            return _list[i];
        }

        public T Peek()
        {
            return _list[^1];
        }

        public T Pop()
        {
            T lastElement = _list[^1];
            _list.Remove(lastElement);
            return lastElement;
        }

        public void PushToBack(T element)
        {
            _list.Add(element);
        }

        public void PushToFront(T element)
        {
            _list.Insert(0, element);
        }
        
    }
    
}