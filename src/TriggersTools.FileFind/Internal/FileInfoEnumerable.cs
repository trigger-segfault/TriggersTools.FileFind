using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TriggersTools.IO.Windows.Internal {
	/// <summary>An enumerable for <see cref="FileFindInfo"/>.</summary>
	internal class FileInfoEnumerable : IEnumerable<FileFindInfo> {

		#region Fields
		
		/// <summary>The information about the search.</summary>
		private readonly SearchInfo search;

		#endregion

		#region Constructors

		/// <summary>Constructs the <see cref="FileInfoEnumerable"/>.</summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="searchPattern">
		/// The search string to match against file-system entries in path. This parameter can contain a
		/// combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't
		/// support regular expressions.
		/// </param>
		/// <param name="files">True if files are returned.</param>
		/// <param name="subdirs">True if directories are returned.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <param name="ignoreCase">True if pattern casing should be ignored.</param>
		public FileInfoEnumerable(string path, string searchPattern, bool files, bool subdirs,
			SearchOrder searchOrder, bool ignoreCase)
		{
			search = new SearchInfo(path, searchPattern, files, subdirs, searchOrder, ignoreCase);
		}

		/// <summary>Constructs the <see cref="FileInfoEnumerable"/>.</summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="files">True if files are returned.</param>
		/// <param name="subdirs">True if directories are returned.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		/// <param name="ignoreCase">True if pattern casing should be ignored.</param>
		public FileInfoEnumerable(string path, Regex regex, bool files, bool subdirs,
			SearchOrder searchOrder)
		{
			search = new SearchInfo(path, regex, files, subdirs, searchOrder);
		}

		#endregion

		#region IEnumerable Implementation

		/// <summary>Gets the enumerator for the <see cref="FileFind"/>.</summary>
		public IEnumerator<FileFindInfo> GetEnumerator() {
			switch (search.SearchOrder) {
			case SearchOrder.TopDirectoryOnly:	return new CurrentInfoEnumerator(search);
			case SearchOrder.AllDirectories:	return new AllDirectoriesInfoEnumerator(search);
			case SearchOrder.AllSubdirectories: return new AllSubdirectoriesInfoEnumerator(search);
			case SearchOrder.AllDepths:			return new AllDepthsInfoEnumerator(search);
			}
			return null;
		}
		/// <summary>Gets the enumerator for the <see cref="FileFind"/>.</summary>
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		#endregion

		#region Properties

		/// <summary>
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </summary>
		public string Path => search.Path;
		/// <summary>
		/// The search string to match against file-system entries in path. This parameter can contain a
		/// combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't
		/// support regular expressions.
		/// </summary>
		public string SearchPattern => search.SearchPattern;
		/// <summary>True if files are returned.</summary>
		public bool Files => search.Files;
		/// <summary>True if directories are returned.</summary>
		public bool Subdirs => search.Subdirs;
		/// <summary>How subdirectories should be searched, if at all.</summary>
		public SearchOrder SearchOrder => search.SearchOrder;
		/// <summary>True if the search pattern is not just a '*'.</summary>
		public bool HasSearchPattern => search.HasSearchPattern;
		/// <summary>The regular expression used when the search pattern is not '*'.</summary>
		public Regex Regex => search.Regex;

		#endregion
	}
}
