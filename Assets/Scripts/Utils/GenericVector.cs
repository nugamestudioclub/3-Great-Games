using System;

[Serializable]
public struct GenericVector<T> {
	public T x;
	public T y;

	public GenericVector(T x, T y) {
		this.x = x;
		this.y = y;
	}

	public static implicit operator GenericVector3<T>(GenericVector<T> v) {
		return new GenericVector3<T>(v.x, v.y, default);
	}
}

[Serializable]
public struct GenericVector3<T> {
	public T x;
	public T y;
	public T z;

	public GenericVector3(T x, T y, T z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public GenericVector3(T x, T y) {
		this.x = x;
		this.y = y;
		z = default;
	}

	public static implicit operator GenericVector<T>(GenericVector3<T> v) {
		return new GenericVector<T>(v.x, v.y);
	}
}