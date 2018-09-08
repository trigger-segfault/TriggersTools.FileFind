using System.Text.RegularExpressions;

namespace TriggersTools.IO.Windows.Utils {
	/// <summary>A class for encapsulating a wildcard <see cref="Regex"/>.</summary>
	internal static class WildCard {

		#region Static ToRegex

		/// <summary>Converts the wild card pattern to a <see cref="Regex"/> pattern.</summary>
		/// 
		/// <param name="pattern">The wildcard pattern to convert.</param>
		/// <returns>The regex pattern as a <see cref="string"/>.</returns>
		public static string ToRegexPattern(string pattern) {
			return "^" + Regex.Escape(pattern).Replace("\\?", ".").Replace("\\*", ".*") + "$";
		}

		/// <summary>Converts the wildcard pattern to a <see cref="Regex"/>.</summary>
		/// 
		/// <param name="pattern">The wildcard pattern to convert.</param>
		/// <returns>The regex pattern as a <see cref="Regex"/>.</returns>
		public static Regex ToRegex(string pattern) {
			return new Regex(ToRegexPattern(pattern));
		}

		/// <summary>Converts the wildcard pattern to a <see cref="Regex"/>.</summary>
		/// 
		/// <param name="pattern">The wildcard pattern to convert.</param>
		/// <param name="ignoreCase">True if casing should be ignored.</param>
		/// <returns>The regex pattern as a <see cref="Regex"/>.</returns>
		public static Regex ToRegex(string pattern, bool ignoreCase) {
			return new Regex(ToRegexPattern(pattern), ignoreCase ? RegexOptions.IgnoreCase :
																   RegexOptions.None);
		}

		/// <summary>Converts the wildcard pattern to a <see cref="Regex"/>.</summary>
		/// 
		/// <param name="pattern">The wildcard pattern to convert.</param>
		/// <param name="options">The regex options to use.</param>
		/// <returns>The regex pattern as a <see cref="Regex"/>.</returns>
		public static Regex ToRegex(string pattern, RegexOptions options) {
			return new Regex(ToRegexPattern(pattern), options);
		}

		#endregion

		#region Static IsMatch

		/// <summary>Checks if the wildcard pattern matches the input.</summary>
		/// 
		/// <param name="input">The input to check.</param>
		/// <param name="pattern">The wildcard pattern to use.</param>
		/// <returns>True if the pattern matches the input.</returns>
		public static bool IsMatch(string input, string pattern) {
			return Regex.IsMatch(input, pattern);
		}

		/// <summary>Checks if the wildcard pattern matches the input.</summary>
		/// 
		/// <param name="input">The input to check.</param>
		/// <param name="pattern">The wildcard pattern to use.</param>
		/// <param name="ignoreCase">True if casing should be ignored.</param>
		/// <returns>True if the pattern matches the input.</returns>
		public static bool IsMatch(string input, string pattern, bool ignoreCase) {
			return Regex.IsMatch(input, pattern, ignoreCase ? RegexOptions.IgnoreCase :
															  RegexOptions.None);
		}

		/// <summary>Checks if the wildcard pattern matches the input.</summary>
		/// 
		/// <param name="input">The input to check.</param>
		/// <param name="pattern">The wildcard pattern to use.</param>
		/// <param name="options">The regex options to use.</param>
		/// <returns>True if the pattern matches the input.</returns>
		public static bool IsMatch(string input, string pattern, RegexOptions options) {
			return Regex.IsMatch(input, pattern, options);
		}

		#endregion
	}
}
