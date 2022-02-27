public interface IRefreshable {
	bool IsActive { get; }
	void Refresh();
}