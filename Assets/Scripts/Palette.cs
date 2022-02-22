using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Palette<T> : ScriptableObject, IEnumerable<T>, IReadOnlyList<T> {
	[SerializeField]
	private List<T> items;
	public int Count => items.Count;

	public T this[int index] {
		get => items[index % items.Count];
		set => items[index % items.Count] = value;
	}

	public IEnumerator<T> GetEnumerator() {
		return ((IEnumerable<T>)items).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return ((IEnumerable)items).GetEnumerator();
	}
}