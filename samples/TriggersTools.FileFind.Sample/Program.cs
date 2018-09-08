using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TriggersTools.IO.Windows.Demo {
	class Program {
		static string Path { get; set; }
		static string Pattern { get; set; }
		static SearchOption Option { get; set; }
		static SearchOrder Order { get; set; }

		static void Main(string[] args) {
			ChangeSettings(System.IO.Path.GetTempPath(), "*", SearchOption.TopDirectoryOnly, SearchOrder.TopDirectoryOnly);
			EnumerateFileSystemEntries("Scanning temp...\n" +
						   "(These times will be slower than normal)");
			EnumerateFileSystemEntries("Scanning temp again...");

			Path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
			ChangeSettings(Path, "*.mp3", SearchOption.AllDirectories, SearchOrder.AllDirectories);
			EnumerateFiles("Running initial tests...");
			
			EnumerateFiles("Running initial tests again...");
			
			ChangeSettings(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "*");
			EnumerateFileSystemEntries("Scanning current disk...");

			// Cleanup
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			Console.WriteLine();
			Console.WriteLine("Done. Press any key to exit...");
			Console.ReadKey();
		}

		static void EnumerateFileSystemEntries(string label) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(label);
			Console.ResetColor();
			Test("FileFind.EnumerateFileSystemInfos     ", FileFind.EnumerateFileSystemInfos(Path, Pattern, Order));
			Test("FileFind.EnumerateFileSystemEntries   ", FileFind.EnumerateFileSystemEntries(Path, Pattern, Order));
			Test("Directory.EnumerateFileSystemEntries  ", Directory.EnumerateFileSystemEntries(Path, Pattern, Option));
			Test("DirectoryInfo.EnumerateFileSystemInfos", new DirectoryInfo(Path).EnumerateFileSystemInfos(Pattern, Option));
			Console.WriteLine();
		}

		static void EnumerateFiles(string label) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(label);
			Console.ResetColor();
			Test("FileFind.EnumerateFileInfos ", FileFind.EnumerateFileInfos(Path, Pattern, Order));
			Test("FileFind.EnumerateFiles     ", FileFind.EnumerateFiles(Path, Pattern, Order));
			Test("Directory.EnumerateFiles    ", Directory.EnumerateFiles(Path, Pattern, Option));
			Test("DirectoryInfo.EnumerateFiles", new DirectoryInfo(Path).EnumerateFiles(Pattern, Option));
			Console.WriteLine();
		}

		static void EnumerateDirectories(string label) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(label);
			Console.ResetColor();
			Test("FileFind.EnumerateDirectoryInfos  ", FileFind.EnumerateDirectoryInfos(Path, Pattern, Order));
			Test("FileFind.EnumerateDirectories     ", FileFind.EnumerateDirectories(Path, Pattern, Order));
			Test("Directory.EnumerateDirectories    ", Directory.EnumerateDirectories(Path, Pattern, Option));
			Test("DirectoryInfo.EnumerateDirectories", new DirectoryInfo(Path).EnumerateDirectories(Pattern, Option));
			Console.WriteLine();
		}

		static void ChangeSettings(string path, string pattern, SearchOption option, SearchOrder order) {
			Path = path;
			Pattern = pattern;
			Option = option;
			Order = order;
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"Path    : {Path}");
			Console.WriteLine($"Pattern : {Pattern}");
			Console.WriteLine($"Option  : {Option}");
			Console.WriteLine($"Order   : {Order}");
			Console.WriteLine();
			Console.ResetColor();
		}

		static void ChangeSettings(string path, string pattern = "*") {
			Path = path;
			Pattern = pattern;
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"Path    : {Path}");
			Console.WriteLine($"Pattern : {Pattern}");
			Console.WriteLine();
			Console.ResetColor();
		}

		static volatile int Count;
		static volatile Stopwatch Watch;
		static volatile int Top;
		static volatile int Left;
		static volatile Timer Timer;
		static volatile bool IsTimerRunning;


		static void Test(string label, IEnumerable files) {
			Console.CursorVisible = false;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write($"{label} : ");
			Console.ResetColor();
			Watch = Stopwatch.StartNew();
			Top = Console.CursorTop;
			Left = Console.CursorLeft;
			Count = 0;
			ReportProgress();
			Timer = new Timer(ReportProgress, null, 50, 50);
			try {
				foreach (object obj in files)
					Count++;
				Watch.Stop();
				while (Timer != null) ;
				//Thread.Sleep(100);
				Console.WriteLine();
			}
			catch (Exception ex) {
				Watch.Stop();
				while (Timer != null) ;
				Thread.Sleep(100);
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Failed : {ex.GetType().Name} {ex.Message}");
				Console.ResetColor();
			}
			Console.CursorVisible = true;
		}

		static void ReportProgress(object state = null) {
			if (IsTimerRunning)
				return;

			bool stopped = !Watch.IsRunning;
			if (stopped) {
				Timer.Dispose();
			}

			IsTimerRunning = true;
			Console.CursorLeft = Left;
			Console.CursorTop = Top;
			Console.Write($"{Count,-12:N0} {Watch.Elapsed:mm\\:ss\\.ffffff}");
			//if (stopped)
			//	Thread.Sleep(1);
			IsTimerRunning = false;
			if (stopped)
				Timer = null;
		}
	}
}
