using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TriggersTools.IO.Windows.Utils;
using static TriggersTools.IO.Windows.Native.Win32;

namespace TriggersTools.IO.Windows.Internal {
	/// <summary>A <see cref="string"/> file enumerator for a single directory level.</summary>
	internal class CurrentPathEnumerator : IEnumerator<string> {

		#region Fields

		// Search
		/// <summary>The information about the search.</summary>
		private readonly SearchInfo search;

		// State
		/// <summary>The current file path in the directory.</summary>
		private string current;
		/// <summary>The current file name in the directory.</summary>
		private string currentName;
		/// <summary>The current file's attributes.</summary>
		private FileAttributes currentAttributes;
		/// <summary>The find handle.</summary>
		private IntPtr hFind;
		/// <summary>The find result. True when the enumeration is ongoing.</summary>
		private bool findResult;

		#endregion

		#region Constructors

		/// <summary>Constructs the <see cref="CurrentPathEnumerator"/>.</summary>
		/// 
		/// <param name="search">The information about the search.</param>
		public CurrentPathEnumerator(SearchInfo search) {
			this.search = search;
		}

		#endregion

		#region Private Helpers

		/// <summary>Determines if the find data can be included in the enumeration.</summary>
		/// 
		/// <param name="find">The <see cref="Win32FindData"/>.</param>
		/// <returns>True if the file should be included.</returns>
		private bool IncludeFind(Win32FindData find) {
			if (find.IsRelativeDirectory)
				return false;
			if (find.dwFileAttributes.HasFlag(FileAttributes.Directory)) {
				return search.Subdirs || (search.SearchOrder != SearchOrder.TopDirectoryOnly &&
											!find.dwFileAttributes.HasFlag(FileAttributes.ReparsePoint));
			}
			else {
				return search.Files;
			}
		}

		#endregion

		#region IEnumerator Implementation

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// 
		/// <returns>
		/// True if the enumerator was successfully advanced to the next element; false if the enumerator
		/// has passed the end of the collection.
		/// </returns>
		public bool MoveNext() {
			Win32FindData find = default(Win32FindData);
			do {
				if (hFind == IntPtr.Zero) {
					FindExSearchOps searchOps;
					FindExFlags searchFlags;
					FindExInfoLevels infoLevel;
					if (!search.Files)
						searchOps = FindExSearchOps.LimitToDirectories;
					else
						searchOps = FindExSearchOps.NameMatch;
					if (!search.IgnoreCase)
						searchFlags = FindExFlags.CaseSensitive;
					else
						searchFlags = FindExFlags.None;
					if (SupportsExtraFindOptions) {
						searchFlags |= FindExFlags.LargeFetch;
						infoLevel = FindExInfoLevels.Basic;
					}
					else {
						infoLevel = FindExInfoLevels.Standard;
					}
					hFind = FindFirstFileEx(search.SearchPatternPath,
											infoLevel, out find,
											searchOps, IntPtr.Zero,
											searchFlags);
					if (hFind == InvalidHandle)
						return false;
					current = PathUtils.CombineNoChecks(search.Path, find.cFileName);
					currentName = find.cFileName;
					currentAttributes = find.dwFileAttributes;
					findResult = true;
				}
				else if (findResult) {
					findResult = FindNextFile(hFind, out find);

					if (findResult) {
						current = PathUtils.CombineNoChecks(search.Path, find.cFileName);
						currentName = find.cFileName;
						currentAttributes = find.dwFileAttributes;
					}
					else {
						current = null;
						currentName = null;
						currentAttributes = 0;
					}
				}
			} while (findResult && !IncludeFind(find));
			return findResult;
		}

		/// <summary>
		/// Sets the enumerator to its initial position, which is before the first file in the
		/// directory.
		/// </summary>
		public void Reset() {
			Dispose();
		}

		#endregion

		#region IDisposable Implementation

		/// <summary>Disposes of the enumerator.</summary>
		public void Dispose() {
			if (hFind != IntPtr.Zero && hFind != InvalidHandle)
				FindClose(hFind);
			hFind = IntPtr.Zero;
			findResult = false;
			current = null;
			currentName = null;
			currentAttributes = 0;
		}

		#endregion

		#region Properties

		/// <summary>Gets if the enumerator has enumerated all files in the directory.</summary>
		public bool IsFinished => hFind == InvalidHandle || (hFind != IntPtr.Zero && !findResult);

		/// <summary>Gets the current file path in the directory.</summary>
		public string Current => current;

		/// <summary>Gets the current file name in the directory.</summary>
		public string CurrentName => currentName;

		/// <summary>Gets the current file's attributes.</summary>
		public FileAttributes CurrentAttributes => currentAttributes;

		#endregion

		#region IEnumerator Explicit Implementation

		/// <summary>Gets the current file path in the directory.</summary>
		object IEnumerator.Current => Current;

		#endregion
	}
}
