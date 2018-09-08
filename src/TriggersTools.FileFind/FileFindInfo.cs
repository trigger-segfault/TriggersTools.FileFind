using System;
using System.IO;
using static TriggersTools.IO.Windows.Native.Win32;

namespace TriggersTools.IO.Windows {
	/// <summary>Information about a file scanned using <see cref="FileFind"/>.</summary>
	public class FileFindInfo {
		#region Fields

		/// <summary>The path of the file.</summary>
		public string FullName { get; }
		/// <summary>The name of the file.</summary>
		public string Name { get; }
		/// <summary>The size of the file.</summary>
		public long Size { get; }
		/// <summary>The attributes of the file.</summary>
		public FileAttributes Attributes { get; }
		/// <summary>The UTC creation time of the file.</summary>
		public DateTime CreationTimeUtc { get; }
		/// <summary>The UTC last access time of the file.</summary>
		public DateTime LastAccessTimeUtc { get; }
		/// <summary>The UTC last write time of the file.</summary>
		public DateTime LastWriteTimeUtc { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs the <see cref="FileFindInfo"/> from a <see cref="Win32FindData"/>.
		/// </summary>
		/// <param name="find">The find data to use.</param>
		/// <param name="fullPath">The full path of the file.</param>
		internal FileFindInfo(Win32FindData find, string fullPath) {
			FullName = fullPath;
			Name = find.cFileName;
			Size = find.Size;
			Attributes = find.dwFileAttributes;
			CreationTimeUtc = find.CreationTimeUtc;
			LastAccessTimeUtc = find.LastAccessTimeUtc;
			LastWriteTimeUtc = find.LastWriteTimeUtc;
		}

		#endregion

		#region Properties

		/// <summary>The local creation time of the file.</summary>
		public DateTime CreationTime => CreationTimeUtc.ToLocalTime();
		/// <summary>The UTC last access time of the file.</summary>
		public DateTime LastAccessTime => LastAccessTimeUtc.ToLocalTime();
		/// <summary>The UTC last write time of the file.</summary>
		public DateTime LastWriteTime => LastWriteTimeUtc.ToLocalTime();

		/// <summary>Gets if the file is a directory.</summary>
		public bool IsDirectory => Attributes.HasFlag(FileAttributes.Directory);
		/// <summary>Gets if the file is a symbolic link.</summary>
		public bool IsSymbolicLink => Attributes.HasFlag(FileAttributes.ReparsePoint);

		#endregion

		#region ToString

		/// <summary>Gets the string representation of the file info.</summary>
		public override string ToString() => FullName;

		#endregion
	}
}
