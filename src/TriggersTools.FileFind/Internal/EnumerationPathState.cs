
namespace TriggersTools.IO.Windows.Internal {
	/// <summary>A state for <see cref="string"/> file enumeration.</summary>
	internal class EnumerationPathState {

		#region Fields

		/// <summary>The current directory being enumerated.</summary>
		public CurrentPathEnumerator Enumerator { get; private set; }
		/// <summary>Get the current search information.</summary>
		public SearchInfo Search { get; private set; }

		/// <summary>Gets the number of subdirectories that have been discovered.</summary>
		public int SubdirCount { get; set; }

		#endregion

		#region Constructors

		/// <summary>Constructs the <see cref="EnumerationPathState"/>.</summary>
		/// 
		/// <param name="search">The search information to use.</param>
		public EnumerationPathState(SearchInfo search) {
			Search = search;
			Enumerator = new CurrentPathEnumerator(search);
			SubdirCount = 0;
			//IsSubdirSearch = false;
		}

		/// <summary>Constructs the <see cref="EnumerationPathState"/>.</summary>
		/// 
		/// <param name="search">The new path to use.</param>
		/// <param name="search">The search information to use.</param>
		public EnumerationPathState(string path, SearchInfo search)
			: this(new SearchInfo(path, search))
		{
		}

		#endregion
	}
}
