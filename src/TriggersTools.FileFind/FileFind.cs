using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using TriggersTools.IO.Windows.Utils;
using static TriggersTools.IO.Windows.Native.Win32;

namespace TriggersTools.IO.Windows {
	/// <summary>The search order for directories for use with <see cref="FileFind"/>.</summary>
	public enum SearchOrder {
		/// <summary>Includes only the current directory in a search operation.</summary>
		TopDirectoryOnly = 0,
		/// <summary>Each individual subdirectory is searched after the parent directory.</summary>
		AllDirectories = 1,
		/// <summary>Each individual subdirectory is searched when it is encountered.</summary>
		AllSubdirectories = 2,
		/// <summary>Each individual subdirectory is searched in the order of their depth.</summary>
		AllDepths = 3,
	}

	/// <summary>A better, faster, harder, stronger file system enumerator for Windows.</summary>
	public static partial class FileFind {

		#region GetInfo/Exact

		/// <summary>Finds the <see cref="FileFindInfo"/> with the specified path.</summary>
		/// 
		/// <param name="path">The path of the file.</param>
		/// <returns>The information of the file if found, otherwise null.</returns>
		public static FileFindInfo GetInfo(string path) {
			if (GetData(path, out Win32FindData find))
				return new FileFindInfo(find, path);
			return null;
		}

		/// <summary>Finds the case-sensitive name of the file with the specified path.</summary>
		/// 
		/// <param name="path">The path of the file.</param>
		/// <returns>The exact name of the file if found, otherwise null.</returns>
		public static string GetExactName(string path) {
			if (GetData(path, out Win32FindData find))
				return find.cFileName;
			return null;
		}

		/// <summary>Finds the case-sensitive path of the file with the specified path.</summary>
		/// 
		/// <param name="path">The path of the file.</param>
		/// <returns>The exact path of the file if found, otherwise null.</returns>
		public static string GetExactPath(string path) {
			path = Path.GetFullPath(path);

			List<string> parts = new List<string>();

			string parent = Path.GetDirectoryName(path);
			while (!string.IsNullOrEmpty(parent)) {
				string name = GetExactName(path);
				parts.Add(name);

				path = parent;
				parent = Path.GetDirectoryName(path);
			}

			// Handle the root part (i.e., drive letter or UNC \\server\share).
			string root = path;
			if (root.Contains(':')) {
				// Drive Letter
				root = root.ToUpper();
			}
			else {
				// UNC
				string[] rootParts = root.Split(Path.DirectorySeparatorChar);
				root = string.Join(PathUtils.DirectorySeparatorString, rootParts.Select(part =>
					CultureInfo.CurrentCulture.TextInfo.ToTitleCase(part)));
			}

			parts.Add(root);
			parts.Reverse();
			return Path.Combine(parts.ToArray());
		}

		/// <summary>Gets if the specified directory contains no files.</summary>
		/// 
		/// <param name="directory">The path of the directory.</param>
		/// <returns>True if the directory contains no files.</returns>
		/// 
		/// <exception cref="Win32Exception">Failed to search the directory.</exception>
		public static bool IsDirectoryEmpty(string directory) {
			string path = PathUtils.AddLongPathPrefix(Path.Combine(directory, "*"));

			Win32FindData find = new Win32FindData();
			bool findResult = true;
			FindExFlags searchFlags;
			FindExInfoLevels infoLevel;
			if (SupportsExtraFindOptions) {
				searchFlags = FindExFlags.LargeFetch;
				infoLevel = FindExInfoLevels.Basic;
			}
			else {
				searchFlags = FindExFlags.None;
				infoLevel = FindExInfoLevels.Standard;
			}
			IntPtr hFind = FindFirstFileEx(path,
										   infoLevel, out find,
										   FindExSearchOps.NameMatch, IntPtr.Zero,
										   searchFlags);
			if (hFind == InvalidHandle)
				throw new Win32Exception();
			try {
				// Skip the '.' and '..' file entries.
				while (find.IsRelativeDirectory && findResult) {
					findResult = FindNextFile(hFind, out find);
				}
				return !findResult;
			}
			finally {
				FindClose(hFind);
			}
		}

		#endregion

		#region Private Helpers
		
		/// <summary>Finds the <see cref="Win32FindData"/> with the specified path.</summary>
		/// 
		/// <param name="path">The path of the file.</param>
		/// <param name="find">The output find data.</param>
		/// <returns>True if the file was found.</returns>
		private static bool GetData(string path, out Win32FindData find) {
			path = PathUtils.AddLongPathPrefix(path);

			find = new Win32FindData();
			FindExFlags searchFlags;
			FindExInfoLevels infoLevel;
			if (SupportsExtraFindOptions) {
				searchFlags = FindExFlags.LargeFetch;
				infoLevel = FindExInfoLevels.Basic;
			}
			else {
				searchFlags = FindExFlags.None;
				infoLevel = FindExInfoLevels.Standard;
			}
			IntPtr hFind = FindFirstFileEx(path,
										   infoLevel, out find,
										   FindExSearchOps.NameMatch, IntPtr.Zero,
										   searchFlags);
			try {
				return (hFind != InvalidHandle);
			}
			finally {
				FindClose(hFind);
			}
		}

		#endregion
	}
}
