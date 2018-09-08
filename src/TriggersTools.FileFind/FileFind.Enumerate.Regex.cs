using System.Collections.Generic;
using System.Text.RegularExpressions;
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
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An enumerable collection of files that matches searchPattern and searchOrder.
		/// </returns>
		public static IEnumerable<string> EnumerateFiles(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return new FilePathEnumerable(path, regex, true, false, searchOrder);
		}

		/// <summary>
		/// Returns an enumerable collection of directory information that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An enumerable collection of directories that matches searchPattern and searchOrder.
		/// </returns>
		public static IEnumerable<string> EnumerateDirectories(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return new FilePathEnumerable(path, regex, false, true, searchOrder);
		}

		/// <summary>
		/// Returns an enumerable collection of file system paths that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// </param>
		/// <returns>
		/// An enumerable collection of file-system entries in the directory specified by path and that match
		/// the specified search pattern and option.
		/// </returns>
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return new FilePathEnumerable(path, regex, true, true, searchOrder);
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
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An enumerable collection of files that matches searchPattern and searchOrder.
		/// </returns>
		public static IEnumerable<FileFindInfo> EnumerateFileInfos(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return new FileInfoEnumerable(path, regex, true, false, searchOrder);
		}

		/// <summary>
		/// Returns an enumerable collection of directory information that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An enumerable collection of directories that matches searchPattern and searchOrder.
		/// </returns>
		public static IEnumerable<FileFindInfo> EnumerateDirectoryInfos(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return new FileInfoEnumerable(path, regex, false, true, searchOrder);
		}

		/// <summary>
		/// Returns an enumerable collection of file system information that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// </param>
		/// <returns>
		/// An enumerable collection of file-system entries in the directory specified by path and that match
		/// the specified search pattern and option.
		/// </returns>
		public static IEnumerable<FileFindInfo> EnumerateFileSystemInfos(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return new FileInfoEnumerable(path, regex, true, true, searchOrder);
		}

		#endregion
	}
}
