using System.Linq;
using System.Text.RegularExpressions;

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
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An array collection of files that matches searchPattern and searchOrder.
		/// </returns>
		public static string[] GetFiles(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return EnumerateFiles(path, regex, searchOrder).ToArray();
		}

		/// <summary>
		/// Returns an array collection of directory paths that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An array collection of directories that matches searchPattern and searchOrder.
		/// </returns>
		public static string[] GetDirectories(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly, bool ignoreCase = true)
		{
			return EnumerateDirectories(path, regex, searchOrder).ToArray();
		}

		/// <summary>
		/// Returns an array collection of file system paths that matches a specified search
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
		/// An array collection of file-system entries in the directory specified by path and that match
		/// the specified search pattern and option.
		/// </returns>
		public static string[] GetFileSystemEntries(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return EnumerateFileSystemEntries(path, regex, searchOrder).ToArray();
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
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An array collection of files that matches searchPattern and searchOrder.
		/// </returns>
		public static FileFindInfo[] GetFileInfos(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return EnumerateFileInfos(path, regex, searchOrder).ToArray();
		}

		/// <summary>
		/// Returns an array collection of directory information that matches a specified search
		/// pattern and search subdirectory option.
		/// </summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <returns>
		/// An array collection of directories that matches searchPattern and searchOrder.
		/// </returns>
		public static FileFindInfo[] GetDirectoryInfos(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return EnumerateDirectoryInfos(path, regex, searchOrder).ToArray();
		}

		/// <summary>
		/// Returns an array collection of file system information that matches a specified search
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
		/// An array collection of file-system entries in the directory specified by path and that match
		/// the specified search pattern and option.
		/// </returns>
		public static FileFindInfo[] GetFileSystemInfos(string path, Regex regex,
			SearchOrder searchOrder = SearchOrder.TopDirectoryOnly)
		{
			return EnumerateFileSystemInfos(path, regex, searchOrder).ToArray();
		}

		#endregion
	}
}
