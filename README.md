# Boss.Az Console Job Portal

Boss.Az Console Job Portal is a C#/.NET Framework console application that simulates a recruitment platform. The application supports worker and employer registration, sign-in, vacancy management, CV management, vacancy search, job applications, notifications, and JSON-based local persistence.

The project was built as an exam/final project and now uses a clearer source layout so the domain model, authentication flow, infrastructure helpers, presentation helpers, and application entry point are easier to navigate.

## Features

- Worker and employer sign-up/sign-in flows.
- Employer vacancy creation, listing, deletion, and applicant review.
- Worker CV creation, listing, deletion, and job application flow.
- Vacancy search and filtering from the console interface.
- Notification delivery between employers and workers.
- Local JSON persistence through `Newtonsoft.Json`.
- Console UI helpers for menu navigation and formatted output.
- Exception logging to `Errors.txt`.

## Tech Stack

- C#
- .NET Framework 4.7.2
- Newtonsoft.Json 13.0.1
- Visual Studio/MSBuild project format

## Project Structure

```text
.
├── App.config
├── BossAZ.csproj
├── packages.config
├── Properties/
│   └── AssemblyInfo.cs
└── src/
    ├── App/              # Program entry point and main controller
    ├── Authentication/   # Sign-in and sign-up flows
    ├── Domain/           # Core entities: workers, employers, CVs, vacancies
    ├── Exceptions/       # Custom application exceptions
    ├── Infrastructure/   # File and JSON persistence helpers
    ├── Presentation/     # Console UI and warning helpers
    └── Services/         # Search and menu extension behavior
```

Generated build output (`bin/`, `obj/`), IDE user files, NuGet restore output, and runtime files are intentionally ignored by Git.

## Getting Started

### Prerequisites

- Visual Studio 2019 or newer, or MSBuild with .NET Framework 4.7.2 targeting pack.
- NuGet package restore enabled.

### Build and Run

1. Clone the repository:

   ```bash
   git clone https://github.com/aykhan019/Cs_ExamProject.git
   cd Cs_ExamProject
   ```

2. Open `BossAZ.csproj` in Visual Studio.
3. Restore NuGet packages if Visual Studio does not do it automatically.
4. Build and run the project.

For a command-line build with Mono:

```bash
nuget install packages.config -OutputDirectory packages
xbuild /p:Configuration=Debug BossAZ.csproj
```

The application seeds sample workers, employers, CVs, and vacancies at startup and writes the current database to `database.json` in the working directory.

## Usage

Use the keyboard-driven console menus to sign in or create an account:

- Choose **Worker** to manage CVs, browse/search vacancies, apply for jobs, and view notifications.
- Choose **Employer** to manage vacancies, review applicants, hire workers, and view notifications.

Runtime errors are written to `Errors.txt` for debugging.

## Demo

<div align="center">
  <a href="https://www.youtube.com/watch?v=mobOv9Qd304">
    <img src="https://media.aykhan.net/thumbnails/projects/c-sharp.jpeg" alt="Boss.Az console job portal demo">
  </a>
</div>

Click the image above to view the project demonstration.

## License

This project is licensed under the [MIT License](LICENSE).
