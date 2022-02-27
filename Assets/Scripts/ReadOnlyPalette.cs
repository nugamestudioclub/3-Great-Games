using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReadOnlyPalette<T> : IEnumerable<T>, IReadOnlyList<T> { }

[Serializable]
public class ReadOnlyPalette<T> : IReadOnlyPalette<T> {
	[SerializeField]
	private List<T> items;

	public ReadOnlyPalette() {
		items = new List<T>();
	}

	public int Count => items.Count;

	public T this[int index] => items[index % items.Count];

	public IEnumerator<T> GetEnumerator() {
		return ((IEnumerable<T>)items).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return items.GetEnumerator();
	}
}