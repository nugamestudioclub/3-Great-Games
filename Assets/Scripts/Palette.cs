using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPalette<T> : IReadOnlyPalette<T>, IList<T> {
	new T this[int index] { get; set; }
}

[Serializable]
public class Palette<T> : IPalette<T>  {
	[SerializeField]
	private List<T> items;

	public Palette() {
		items = new List<T>();
	}

	public int Count => items.Count;

	public bool IsReadOnly => false;

	public T this[int index] {
		get => items[index % items.Count];
		set => items[index % items.Count] = value;
	}

	public int IndexOf(T item) => items.IndexOf(item);

	public void Insert(int index, T item) => items.Insert(index, item);

	public void RemoveAt(int index) => items.RemoveAt(index);

	public void Add(T item) => items.Add(item);

	public void Clear() => items.Clear();

	public bool Contains(T item) => items.Contains(item);

	public void CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

	public bool Remove(T item) => items.Remove(item);

	public IEnumerator<T> GetEnumerator() {
		return ((IEnumerable<T>)items).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return ((IEnumerable)items).GetEnumerator();
	}
}