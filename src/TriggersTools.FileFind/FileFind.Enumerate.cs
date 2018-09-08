using System.Collections.Generic;
using TriggersTools.IO.Windows.Internal;

namespace TriggersTools.IO.Windows {
	partial class FileFind {
		
		#region EnumeratePath

		/// <summary>
		/// Returns an enumerable collection of file paths that matches a specified search pattern
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
		/// An enumerable collection of files that matches searchPattern and searchOrder.
		/// </returns>
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return new FilePathEnumerable(path, searchPattern, true, false, searchOrder, ignoreCase);
		}

		/// <summary>
		/// Returns an enumerable collection of directory information that matches a specified search
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
		/// An enumerable collection of directories that matches searchPattern and searchOrder.
		/// </returns>
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return new FilePathEnumerable(path, searchPattern, false, true, searchOrder, ignoreCase);
		}

		/// <summary>
		/// Returns an enumerable collection of file system paths that matches a specified search
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
		/// An enumerable collection of file-system entries in the directory specified by path and that match
		/// the specified search pattern and option.
		/// </returns>
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return new FilePathEnumerable(path, searchPattern, true, true, searchOrder, ignoreCase);
		}

		#endregion
		

		#region EnumerateInfo

		/// <summary>
		/// Returns an enumerable collection of file information that matches a specified search pattern
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
		/// An enumerable collection of files that matches searchPattern and searchOrder.
		/// </returns>
		public static IEnumerable<FileFindInfo> EnumerateFileInfos(string path, string searchPattern = "*",
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return new FileInfoEnumerable(path, searchPattern, true, false, searchOrder, ignoreCase);
		}

		/// <summary>
		/// Returns an enumerable collection of directory information that matches a specified search
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
		/// An enumerable collection of directories that matches searchPattern and searchOrder.
		/// </returns>
		public static IEnumerable<FileFindInfo> EnumerateDirectoryInfos(string path,
			string searchPattern = "*", SearchOrder searchOrder = SearchOrder.TopDirectoryOnly,
			bool ignoreCase = true)
		{
			return new FileInfoEnumerable(path, searchPattern, false, true, searchOrder, ignoreCase);
		}

		/// <summary>
		/// Returns an enumerable collection of file system information that matches a specified search
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
		/// An enumerable collection of file-system entries in the directory specified by path and that match
		/// the specified search pattern and option.
		/// </returns>
		public static IEnumerable<FileFindInfo> EnumerateFileSystemInfos(string path,
			string searchPattern = "*", SearchOrder searchOrder = SearchOrder.TopDirectoryOnly,
			bool ignoreCase = true)
		{
			return new FileInfoEnumerable(path, searchPattern, true, true, searchOrder, ignoreCase);
		}

		#endregion
	}
}
