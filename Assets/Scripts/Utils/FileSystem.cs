using System.IO;

public static class FileSystem {
	public static string WithSuffix(string path, string suffix) {
		return
			Path.GetDirectoryName(path) + Path.DirectorySeparatorChar +
			Path.GetFileNameWithoutExtension(path) + suffix + Path.GetExtension(path);
	}

	public static string WithoutSuffix(string path, string suffix) {
		int pos = path.LastIndexOf(suffix);

		return pos < 0 ? path : path.Substring(0, pos);
	}
}