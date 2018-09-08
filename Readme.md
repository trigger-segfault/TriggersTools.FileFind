# TriggersTools.FileFind ![AppIcon](https://i.imgur.com/UUPIODl.png)

[![NuGet Version](https://img.shields.io/nuget/v/TriggersTools.FileFind.svg?style=flat)](https://www.nuget.org/packages/TriggersTools.FileFind/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/TriggersTools.FileFind.svg?style=flat)](https://www.nuget.org/packages/TriggersTools.FileFind/)
[![Creation Date](https://img.shields.io/badge/created-september%202018-A642FF.svg?style=flat)](https://github.com/trigger-death/TriggersTools.FileFind/commit/251837eca13d99d4db9af923b1932e5f8b79c5ac)

An improvement on Window's existing .NET Framework API for finding files. Enumeration no longer fails when encountering a secure file. File numeration is a little bit faster. Added support for matching file names by Regex.

## General Info

The static class `FileFind` works very similarly to how `Directory` and `DirectoryInfo`'s enumerations work. To allow returning a preconstructed `FileInfo`/`DirectoryInfo`, `FileFind` uses its own class called `FileFindInfo`.

`FileFindInfo` contains the following properties:

```cs
class FileFindInfo {
    string FullName { get; }
    string Name { get; }
    long Size { get; }
    FileAttributes Attributes { get; }
    DateTime CreationTimeUtc { get; }
    DateTime CreationTime { get; }
    DateTime LastAccessTimeUtc { get; }
    DateTime LastAccessTime { get; }
    DateTime LastWriteTimeUtc { get; }
    DateTime LastWriteTime { get; }
    bool IsDirectory { get; }
    bool IsSymbolicLink { get; }
}
```

## Basic Example

Basic examples of enumerating file paths/infos or getting the same result as an array.

```cs
using TriggersTools.IO.Windows;

// List all files in the source directory
foreach (string file in FileFind.EnumerateFiles("Source", "*.cs", SearchOrder.AllDirectories))
    Console.WriteLine(file);

// Get an array of the same files as FileFindInfos
FileFindInfo[] infos = FileFind.GetFileInfos("Source", "*.cs", SearchOrder.AllDirectories);

// Count the total number of items and size of every item in the C: drive.
long totalItems = 0;
long totalSize = 0;
// Use depth order search, folders with 4 ancestores
// will be scanned before folder with 5 ancestores.
foreach (string file in FileFind.EnumerateFileSystemInfos(@"C:\", "*", SearchOrder.AllDepths)) {
    totalItems++;
    totalSize += file.Size;
}
Console.WriteLine($"Total Items: {totalItems:N0}");
Console.WriteLine($"Total Bytes: {totalSize:N0}");
```

### About SearchOrder

Unlike .NET's `SearchOption` of `TopDirectoryOnly` or `AllDirectories` with enumerating files, `FileFind` comes with 4 modes under the `SearchOrder` enumeration.

**Example File Tree:**

```
+ C:\
|-+ Program Files
| '-+ Paint.NET
|   |-+ Effects
|   | '-- Alpha2Gray.dll
|   |-- PaintDotNet.Core.dll
|   '-- PaintDotNet.exe
|-+ Users
| |-+ Trigger
| | |-+ Desktop
| | | '-- My Documents.lnk
| | '-- .gitconfig
| '-+ Public
|   |-- Public Documents
|   '-- Public Downloads
|-- hyberfil.sys
'-- pagefile.sys
```
* **TopDirectoryOnly:** Includes only the current directory in a search operation.

```
EnumerateFileSystemEntries(@"C:\", searchOrder: SearchOrder.TopDirectoryOnly);
- C:\Program Files
- C:\Users
- C:\hyberfil.sys
- C:\pagefile.sys
```

* **AllDirectories:** Each individual subdirectory is searched after the parent directory.

```
EnumerateFileSystemEntries(@"C:\", searchOrder: SearchOrder.AllDirectories);
- C:\Program Files
- C:\Users
- C:\hyberfil.sys
- C:\pagefile.sys
- C:\Program Files\Paint.NET
- C:\Program Files\Paint.NET\Effects
- C:\Program Files\Paint.NET\PaintDotNet.Core.dll
- C:\Program Files\Paint.NET\PaintDotNet.exe
- C:\Program Files\Paint.NET\Effects\Alpha2Gray.dll
- C:\Users\Trigger
- C:\Users\Public
- C:\Users\Trigger\Desktop
- C:\Users\Trigger\.gitconfig
- C:\Users\Trigger\Desktop\My Documents.lnk
- C:\Users\Public\Public Documents
- C:\Users\Public\Public Downloads
```

* **AllSubdirectories:** Each individual subdirectory is searched when it is encountered.

```
EnumerateFileSystemEntries(@"C:\", searchOrder: SearchOrder.AllSubdirectories);
- C:\Program Files
- C:\Program Files\Paint.NET
- C:\Program Files\Paint.NET\Effects
- C:\Program Files\Paint.NET\Effects\Alpha2Gray.dll
- C:\Program Files\Paint.NET\PaintDotNet.Core.dll
- C:\Program Files\Paint.NET\PaintDotNet.exe
- C:\Users
- C:\Users\Trigger
- C:\Users\Trigger\Desktop
- C:\Users\Trigger\Desktop\My Documents.lnk
- C:\Users\Trigger\.gitconfig
- C:\Users\Public
- C:\Users\Public\Public Documents
- C:\Users\Public\Public Downloads
- C:\hyberfil.sys
- C:\pagefile.sys
```

* **AllDepths:** Each individual subdirectory is searched in the order of their depth.

```
EnumerateFileSystemEntries(@"C:\", searchOrder: SearchOrder.AllDepths);
- C:\Program Files
- C:\Users
- C:\hyberfil.sys
- C:\pagefile.sys
- C:\Program Files\Paint.NET
- C:\Users\Trigger
- C:\Users\Public
- C:\Program Files\Paint.NET\Effects
- C:\Program Files\Paint.NET\PaintDotNet.Core.dll
- C:\Program Files\Paint.NET\PaintDotNet.exe
- C:\Users\Trigger\Desktop
- C:\Users\Trigger\.gitconfig
- C:\Users\Public\Public Documents
- C:\Users\Public\Public Downloads
- C:\Program Files\Paint.NET\Effects\Alpha2Gray.dll
- C:\Users\Trigger\Desktop\My Documents.lnk
```

## Regex Search

`FileFind` also allows the use of directly passing Regex for searching.

```cs
// Match 2018 photos following a yyyy-mm-dd format
Regex regex = new Regex(\d{4}-\d\d?-\d\d?.*);
var files FileFind.EnumerateFiles("Camera\2018", regex, SearchOrder.AllDirectories);
foreach (string file in files) {
    // Do something with matched file...
}
```

## Other Functions

`FileFind` contains a few other functions that make use of `FileFind` P/Invoke.

```cs
// Get the info for the first matching file, returns null when nothing is found.
FileFindInfo GetInfo(string path);

// Gets the properly-cased name of the first matching file, return null when nothing is found.
string GetExactName(string path);

// Gets the properly-cased path of the first matching file, return null when nothing is found.
string GetExactPath(string path);

// Returns true if the directory contains no files. Throws Win32Exception if the directory could not be found.
bool IsDirectoryEmpty(string directory);
```
