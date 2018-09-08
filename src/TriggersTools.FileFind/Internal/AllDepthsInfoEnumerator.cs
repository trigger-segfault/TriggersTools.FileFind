using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TriggersTools.IO.Windows.Internal {
	/// <summary>
	/// A <see cref="FileFindInfo"/> enumerator for <see cref="SearchOrder.AllDepths"/> order.
	/// </summary>
	internal class AllDepthsInfoEnumerator : IEnumerator<FileFindInfo> {

		#region Fields

		// Search
		/// <summary>The information about the search.</summary>
		private readonly SearchInfo search;

		// State
		/// <summary>The current file in the directory.</summary>
		private FileFindInfo current;
		/// <summary>The current enumeration state.</summary>
		private EnumerationInfoState currentState;
		/// <summary>The current enumerator for the current state.</summary>
		private CurrentInfoEnumerator currentEnumerator;
		/// <summary>The queue of enumeration states to scan.</summary>
		private readonly Queue<EnumerationInfoState> stateQueue;

		#endregion

		#region Constructors

		/// <summary>Constructs the <see cref="AllDepthsInfoEnumerator"/>.</summary>
		/// 
		/// <param name="search">The information about the search.</param>
		public AllDepthsInfoEnumerator(SearchInfo search) {
			this.search = search;
			currentState = new EnumerationInfoState(search);
			currentEnumerator = currentState.Enumerator;
			stateQueue = new Queue<EnumerationInfoState>();
			stateQueue.Enqueue(currentState);
		}

		#endregion

		#region Private Helpers

		/// <summary>Determines if the find data can be included in the enumeration.</summary>
		/// 
		/// <param name="info">The <see cref="FileFindInfo"/>.</param>
		/// <returns>True if the file should be included.</returns>
		private bool IncludeFind(FileFindInfo info, out bool subdirectory) {
			if (info.Attributes.HasFlag(FileAttributes.Directory)) {
				subdirectory = !info.Attributes.HasFlag(FileAttributes.ReparsePoint);
				return search.Subdirs && (!search.RequiresRegex ||
											search.Regex.IsMatch(info.Name));
			}
			else {
				subdirectory = false;
				return search.Files && (!search.RequiresRegex ||
											search.Regex.IsMatch(info.Name));
			}
		}

		/// <summary>Ends the current state and procedes to the next available state.</summary>
		private void DequeueState() {
			currentEnumerator.Dispose();
			stateQueue.Dequeue();
			if (stateQueue.Count > 0)
				currentState = stateQueue.Peek();
			else
				currentState = null;
			currentEnumerator = currentState?.Enumerator;
		}

		/// <summary>Enqueues the current result as a new enumeration state.</summary>
		private void EnqueueState() {
			var newState = new EnumerationInfoState(currentEnumerator.Current.FullName, search);
			stateQueue.Enqueue(newState);
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
			// We're done boise! Nothing to do.
			if (currentEnumerator == null)
				return false;

			bool findResult;
			do {
				findResult = currentEnumerator.MoveNext();
				if (findResult) {
					// Once we're in this block, findResult is only used to
					// return the current file. It can safely be set to false.
					findResult = IncludeFind(currentEnumerator.Current, out bool subdirectory);
					// We're scanning files and this guy has been caught redhanded.
					if (findResult)
						current = currentEnumerator.Current;

					// We're scanning subdirectories and this guy is invited to the party
					if (subdirectory)
						EnqueueState();
				}
				else if (!findResult) {
					// This directory has been emptied
					/*if (currentState.Search.HasSearchPattern) {
						// Now search for each individual subdirectories
						// because the search pattern didn't support it.
						currentEnumerator = currentState.ChangeToSubdirState();
					}
					else {*/
						// Go to the next state in the queue
						DequeueState();
					//}
				}
			} while (!findResult && !IsFinished);
			if (!findResult)
				current = null;
			return findResult;
		}

		/// <summary>
		/// Sets the enumerator to its initial position, which is before the first file in the
		/// directory.
		/// </summary>
		public void Reset() {
			Dispose();
			currentState = new EnumerationInfoState(search);
			currentEnumerator = currentState.Enumerator;
			stateQueue.Enqueue(currentState);
		}

		#endregion

		#region IDisposable Implementation

		/// <summary>Disposes of the enumerator.</summary>
		public void Dispose() {
			while (stateQueue.Count > 0)
				stateQueue.Dequeue().Enumerator.Dispose();
			current = null;
			currentState = null;
			currentEnumerator = null;
		}

		#endregion

		#region Properties

		/// <summary>Gets if the enumerator has enumerated all files in the directories.</summary>
		public bool IsFinished => stateQueue.Count == 0;

		/// <summary>Gets the current file in the directory.</summary>
		public FileFindInfo Current => current;

		#endregion

		#region IEnumerator Explicit Implementation

		/// <summary>Gets the current file in the directory.</summary>
		object IEnumerator.Current => Current;

		#endregion
	}
}
