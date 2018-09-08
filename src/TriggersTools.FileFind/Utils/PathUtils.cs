using System.IO;

namespace TriggersTools.IO.Windows.Utils {
	/// <summary>A helper with extra methods for paths, files, and directories.</summary>
	internal static class PathUtils {

		#region Constants

		/// <summary>A string representation of <see cref="Path.DirectorySeparatorChar"/>.</summary>
		public static readonly string DirectorySeparatorString = new string(Path.DirectorySeparatorChar, 1);

		#endregion

		#region Checks

		/// <summary>
		/// Returns true if the file name has valid characters for a search pattern.
		/// </summary>
		/// 
		/// <param name="name">The name pattern to check.</param>
		/// <returns>True if the name pattern is valid.</returns>
		public static bool IsValidNamePattern(string name) {
			if (string.IsNullOrEmpty(name))
				return false;
			name = name.Replace("*", "").Replace("?", "");
			return name.IndexOfAny(Path.GetInvalidFileNameChars()) == -1;
		}

		#endregion

		#region Building

		/// <summary>
		/// Combines two paths with without the heavy checks that <see cref="Path.Combine"/> uses.
		/// </summary>
		/// 
		/// <param name="path1">The first path.</param>
		/// <param name="path2">The second path.</param>
		/// <returns>The combined paths split by a separator.</returns>
		public static string CombineNoChecks(string path1, string path2) {
			if (path2.Length == 0)
				return path1;

			if (path1.Length == 0)
				return path2;

			if (Path.IsPathRooted(path2))
				return path2;

			char ch = path1[path1.Length - 1];
			if (ch != Path.DirectorySeparatorChar && ch != Path.AltDirectorySeparatorChar)
				return path1 + DirectorySeparatorString + path2;
			return path1 + path2;
		}
		
		/// <summary>
		/// Adds the long path prefix "\\?\" to the front of the path, if it does not already have it.
		/// </summary>
		/// 
		/// <param name="path">The path to add the prefix to.</param>
		/// <returns>The path prefixed with "\\?\".</returns>
		public static string AddLongPathPrefix(string path) {
			if (!path.StartsWith(@"\\?\"))
				return @"\\?\" + path;
			return path;
		}

		#endregion
	}
}
