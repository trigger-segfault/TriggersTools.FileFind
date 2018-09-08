using System;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace TriggersTools.IO.Windows.Native {
	/// <summary>Extentions for native Win32 structures.</summary>
	internal static class Win32Extensions {

		/// <summary>Converts the native <see cref="FILETIME"/> to a local <see cref="DateTime"/>.</summary>
		/// 
		/// <param name="fileTime">The native filetime to convert.</param>
		/// <returns>The new <see cref="DateTime"/> in local time.</returns>
		public static DateTime ToDateTime(this FILETIME fileTime) {
			long ticks = ((long) fileTime.dwHighDateTime << 32) | unchecked((uint) fileTime.dwLowDateTime);
			return DateTime.FromFileTime(ticks);
		}

		/// <summary>Converts the native <see cref="FILETIME"/> to a UTC <see cref="DateTime"/>.</summary>
		/// 
		/// <param name="fileTime">The native filetime to convert.</param>
		/// <returns>The new <see cref="DateTime"/> in UTC.</returns>
		public static DateTime ToDateTimeUtc(this FILETIME fileTime) {
			long ticks = ((long) fileTime.dwHighDateTime << 32) | unchecked((uint) fileTime.dwLowDateTime);
			return DateTime.FromFileTimeUtc(ticks);
		}
	}
}
