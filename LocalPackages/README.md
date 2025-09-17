# LocalPackages Directory

This directory is used to store local NuGet packages, particularly commercial packages like DevExpress that cannot be included in the repository due to licensing restrictions.

## Purpose

- Store local copies of DevExpress NuGet packages for development
- Enable building the CommonForms project locally
- Avoid including licensed packages in the public repository

## Setup Instructions

1. Run the DevExpress package copying script:
   ```powershell
   .\Scripts\copy-devexpress-packages.ps1
   ```

2. This will populate this directory with the required DevExpress .nupkg files

## GitHub Actions

In GitHub Actions, this directory is created automatically and populated with packages from GitHub Secrets if available.

## Git Ignore

The .nupkg files in this directory are ignored by Git to prevent accidentally committing licensed packages.