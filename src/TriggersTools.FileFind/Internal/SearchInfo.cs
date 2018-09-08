using System;
using System.Text.RegularExpressions;
using TriggersTools.IO.Windows.Utils;

namespace TriggersTools.IO.Windows.Internal {
	/// <summary>Information on how a directory should be searched.</summary>
	internal struct SearchInfo {

		#region Fields

		/// <summary>
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </summary>
		public string Path { get; }
		/// <summary>
		/// The search string to match against file-system entries in path. This parameter can contain a
		/// combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't
		/// support regular expressions.
		/// </summary>
		public string SearchPattern { get; }
		/// <summary>True if files are returned.</summary>
		public bool Files { get; }
		/// <summary>True if directories are returned.</summary>
		public bool Subdirs { get; }
		/// <summary>How subdirectories should be searched, if at all.</summary>
		public SearchOrder SearchOrder { get; }
		/// <summary>True if pattern casing should be ignored.</summary>
		public bool IgnoreCase { get; }
		/// <summary>The regular expression used when the search pattern is not '*'.</summary>
		public Regex Regex { get; }
		/// <summary>
		/// True if the regex is custom and should not conform to directory case sensitivity.
		/// </summary>
		public bool IsCustomRegex { get; }

		#endregion

		#region Constructors

		/// <summary>Constructs the <see cref="SearchInfo"/> enumerable.</summary>
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
		public SearchInfo(string path, string searchPattern, bool files, bool subdirs,
			SearchOrder searchOrder, bool ignoreCase) {
			if (!Enum.IsDefined(typeof(SearchOrder), searchOrder))
				throw new ArgumentException(nameof(searchOrder));
			if (!files && !subdirs)
				throw new ArgumentException($"{nameof(files)} or {nameof(subdirs)} must be true!");
			if (searchOrder == SearchOrder.AllSubdirectories && searchPattern != "*")
				throw new ArgumentException($"{nameof(SearchOrder.AllSubdirectories)} can only be " +
					$"used when a search pattern of '*'!");
			if (!PathUtils.IsValidNamePattern(searchPattern))
				throw new ArgumentException($"{nameof(searchPattern)} is not a valid wildcard pattern!");
			Path = path;
			SearchPattern = searchPattern;
			Files = files;
			Subdirs = subdirs;
			SearchOrder = searchOrder;
			IgnoreCase = ignoreCase;
			if (searchPattern != "*" && (searchOrder != SearchOrder.TopDirectoryOnly || !ignoreCase))
				Regex = WildCard.ToRegex(SearchPattern, ignoreCase);
			else
				Regex = null;
			IsCustomRegex = false;
		}

		/// <summary>Constructs the <see cref="SearchInfo"/> enumerable.</summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="regex">The regex expression to match with.</param>
		/// <param name="files">True if files are returned.</param>
		/// <param name="subdirs">True if directories are returned.</param>
		/// <param name="searchOrder">How subdirectories should be searched, if at all.</param>
		public SearchInfo(string path, Regex regex, bool files, bool subdirs, SearchOrder searchOrder) {
			if (!Enum.IsDefined(typeof(SearchOrder), searchOrder))
				throw new ArgumentException(nameof(searchOrder));
			if (!files && !subdirs)
				throw new ArgumentException($"{nameof(files)} or {nameof(subdirs)} must be true!");
			if (searchOrder == SearchOrder.AllSubdirectories)
				throw new ArgumentException($"{nameof(SearchOrder.AllSubdirectories)} can only be " +
					$"used when a search pattern of '*'!");
			Path = path;
			SearchPattern = "*";
			Files = files;
			Subdirs = subdirs;
			SearchOrder = searchOrder;
			IgnoreCase = regex.Options.HasFlag(RegexOptions.IgnoreCase);
			Regex = regex ?? throw new ArgumentNullException(nameof(regex));
			IsCustomRegex = true;
		}

		/// <summary>Constructs the <see cref="SearchInfo"/> enumerable.</summary>
		/// 
		/// <param name="path">
		/// The relative or absolute path to the directory to search. This string is not case-sensitive.
		/// </param>
		/// <param name="baseSearch">The base search state to base this state off of.</param>
		public SearchInfo(string path, SearchInfo baseSearch) {
			Path = path;
			SearchPattern = baseSearch.SearchPattern;
			Files = baseSearch.Files;
			Subdirs = baseSearch.Subdirs;
			SearchOrder = baseSearch.SearchOrder;
			IgnoreCase = baseSearch.IgnoreCase;
			Regex = baseSearch.Regex;
			IsCustomRegex = baseSearch.IsCustomRegex;
		}

		#endregion

		#region Properties

		/// <summary>True if the search pattern is not just a '*'.</summary>
		public bool HasSearchPattern => SearchPattern != "*";
		/// <summary>
		/// Gets if <see cref="Regex"/> is required in order to search for patterns.
		/// </summary>
		public bool RequiresRegex => Regex != null;
		/// <summary>Gets the search path combined with the pattern and long path prefix.</summary>
		public string SearchPatternPath {
			get {
				if (RequiresRegex)
					return PathUtils.AddLongPathPrefix(PathUtils.CombineNoChecks(Path, "*"));
				// Only use the real search pattern for top-level directories when ignoreCase is false.
				return PathUtils.AddLongPathPrefix(PathUtils.CombineNoChecks(Path, SearchPattern));
			}
		}

		#endregion
	}
}
