# Pinterest Email Verification Tool

## Overview
The **Pinterest Email Verification Tool** is a Windows Forms application built using **C# (.NET Framework 4.6.1)**. It allows users to verify whether an email is connected to a Pinterest account by utilizing API handling and file I/O operations.

## Features
- Load email lists from a text file
- Check if an email is associated with a Pinterest account
- Save verification results to a file
- Simple and user-friendly WinForms interface

## Prerequisites
- **Windows OS** (Tested on Windows 10/11)
- **.NET Framework 4.6.1**
- **Visual Studio** (for development)
- **Pinterest API Access** (API Key required)

## Installation
1. Clone the repository or download the ZIP file.
2. Open the project in **Visual Studio**.
3. Restore NuGet packages (if any required).
4. Compile and run the application.

## Usage
1. Open the application.
2. Click **Load Emails** to import a list of emails from a `.txt` file.
3. Click **Start Verification** to check which emails are linked to Pinterest.
4. Save results to a file for further use.

## File I/O
- Input: Emails are read from a `.txt` file (one email per line).
- Output: Results are saved in a `.csv` or `.txt` file with status information.

## Error Handling
- Invalid emails are skipped with a warning.
- API errors (e.g., rate limits, authentication failures) are logged.
- Network failures prompt retry attempts.

## Future Enhancements
- Multi-threaded email verification for faster processing
  
## Contact
- For issues or feature requests, please open an issue on GitHub or contact [ductrung190499@gmail.com].
