using System.Linq;

namespace TriggersTools.IO.Windows {
	partial class FileFind {
		
		#region GetPath (Array)

		/// <summary>
		/// Returns an array collection of file paths that matches a specified search pattern
		/// and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="searchPattern">
		/// The search string to match against the names of files. This parameter can contain a combination
		/// of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support
		/// regular expressions. The default pattern is "*", which returns all files.
		/// </param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An array collection of files that matches searchPattern and searchOrder.
		/// </returns>
		public static string[] GetFiles(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return EnumerateFiles(path, searchPattern, searchOrder, ignoreCase).ToArray();
		}

		/// <summary>
		/// Returns an array collection of directory paths that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="searchPattern">
		/// The search string to match against the names of directories. This parameter can contain a
		/// combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't
		/// support regular expressions. The default pattern is "*", which returns all files.
		/// </param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An array collection of directories that matches searchPattern and searchOrder.
		/// </returns>
		public static string[] GetDirectories(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return EnumerateDirectories(path, searchPattern, searchOrder, ignoreCase).ToArray();
		}

		/// <summary>
		/// Returns an array collection of file system paths that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="searchPattern">
		/// The search string to match against file-system entries in path. This parameter can contain a
		/// combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't
		/// support regular expressions.
		/// </param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// </param>
		/// <returns>
		/// An array collection of file-system entries in the directory specified by path and that match
		/// the specified search pattern and option.
		/// </returns>
		public static string[] GetFileSystemEntries(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return EnumerateFileSystemEntries(path, searchPattern, searchOrder, ignoreCase).ToArray();
		}

		#endregion

		#region GetInfo (Array)

		/// <summary>
		/// Returns an array collection of file information that matches a specified search pattern
		/// and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="searchPattern">
		/// The search string to match against the names of files. This parameter can contain a combination
		/// of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support
		/// regular expressions. The default pattern is "*", which returns all files.
		/// </param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An array collection of files that matches searchPattern and searchOrder.
		/// </returns>
		public static FileFindInfo[] GetFileInfos(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return EnumerateFileInfos(path, searchPattern, searchOrder, ignoreCase).ToArray();
		}

		/// <summary>
		/// Returns an array collection of directory information that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="searchPattern">
		/// The search string to match against the names of directories. This parameter can contain a
		/// combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't
		/// support regular expressions. The default pattern is "*", which returns all files.
		/// </param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An array collection of directories that matches searchPattern and searchOrder.
		/// </returns>
		public static FileFindInfo[] GetDirectoryInfos(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return EnumerateDirectoryInfos(path, searchPattern, searchOrder, ignoreCase).ToArray();
		}

		/// <summary>
		/// Returns an array collection of file system information that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="searchPattern">
		/// The search string to match against file-system entries in path. This parameter can contain a
		/// combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't
		/// support regular expressions.
		/// </param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// </param>
		/// <returns>
		/// An array collection of file-system entries in the directory specified by path and that match
		/// the specified search pattern and option.
		/// </returns>
		public static FileFindInfo[] GetFileSystemInfos(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return EnumerateFileSystemInfos(path, searchPattern, searchOrder, ignoreCase).ToArray();
		}

		#endregion
	}
}
