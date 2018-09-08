
namespace TriggersTools.IO.Windows.Internal {
	/// <summary>A state for <see cref="FileFindInfo"/> enumeration.</summary>
	internal class EnumerationInfoState {

		#region Fields

		/// <summary>The current directory being enumerated.</summary>
		public CurrentInfoEnumerator Enumerator { get; private set; }
		/// <summary>Get the current search information.</summary>
		public SearchInfo Search { get; private set; }

		/// <summary>Gets the number of subdirectories that have been discovered.</summary>
		public int SubdirCount { get; set; }
		/*/// <summary>
		/// Gets if the enumeration state has finished normal scanning, and has switched exlusively to
		/// uncovering subdirectories.
		/// </summary>
		public bool IsSubdirSearch { get; private set; }*/

		#endregion

		#region Methods

		/*/// <summary>
		/// Changes the enumeration state to just scanning the subdirectories.<para/>
		/// This should only be called when the search path is not anything goes ("*").
		/// </summary>
		/// <returns>The new <see cref="CurrentInfoEnumerator"/> that was created.</returns>
		public CurrentInfoEnumerator ChangeToSubdirState() {
			Debug.Assert(!IsSubdirSearch && Search.HasSearchPattern);
			IsSubdirSearch = true;
			Search = new SearchInfo(Search.Path, "*", Search);
			Enumerator.Dispose();
			return (Enumerator = new CurrentInfoEnumerator(Search));
		}*/

		#endregion

		#region Constructors

		/// <summary>Constructs the <see cref="EnumerationInfoState"/>.</summary>
		/// 
		/// <param name="search">The search information to use.</param>
		public EnumerationInfoState(SearchInfo search) {
			Search = search;
			Enumerator = new CurrentInfoEnumerator(search);
			SubdirCount = 0;
			//IsSubdirSearch = false;
		}

		/// <summary>Constructs the <see cref="EnumerationInfoState"/>.</summary>
		/// 
		/// <param name="search">The new path to use.</param>
		/// <param name="search">The search information to use.</param>
		public EnumerationInfoState(string path, SearchInfo search)
			: this(new SearchInfo(path, search))
		{
		}

		#endregion
	}
}
