using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Palette<T> : IEnumerable<T>, IReadOnlyList<T> {
	[SerializeField]
	private List<T> items;
	public int Count => items.Count;

	public T this[int index] => items[index % items.Count];

	public IEnumerator<T> GetEnumerator() {
		return ((IEnumerable<T>)items).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return ((IEnumerable)items).GetEnumerator();
	}
}