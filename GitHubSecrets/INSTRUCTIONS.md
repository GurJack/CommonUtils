# Instructions for setting up GitHub Secrets for DevExpress packages

## Overview

This folder contains base64-encoded DevExpress packages that can be used as GitHub Secrets to enable building and publishing the CommonForms NuGet package in GitHub Actions.

## Steps to set up GitHub Secrets

1. **Navigate to your GitHub repository settings**
   - Go to your repository on GitHub
   - Click on "Settings" tab
   - In the left sidebar, click "Secrets and variables" → "Actions"

2. **Add secrets for each package**
   - Click "New repository secret" button
   - For each .txt file in this folder:
     - Use the filename (without .txt extension) as the "Name" (e.g., `DEVEXPRESS_WIN_DESIGN_23_2_3_NUPKG`)
     - Open the corresponding .txt file and copy its entire content
     - Paste the content into the "Value" field
     - Click "Add secret"

3. **Required secrets for CommonForms**
   The following secrets are required for building CommonForms:
   - `DEVEXPRESS_WIN_DESIGN_23_2_3_NUPKG`
   - `DEVEXPRESS_OFFICE_CORE_23_2_3_NUPKG`
   - `DEVEXPRESS_UTILS_23_2_3_NUPKG`
   - `DEVEXPRESS_WIN_23_2_3_NUPKG`

## How it works

1. GitHub Actions workflow reads these secrets
2. Decodes base64 content back to .nupkg files
3. Places them in the LocalPackages folder
4. Restores, builds, and packages CommonForms with all dependencies
5. Publishes CommonForms as a NuGet package to GitHub Packages

## Security considerations

- These secrets should only be added to **private repositories**
- DevExpress packages are licensed software - ensure you have proper licenses
- Never commit encoded package files to the repository
- Regularly rotate secrets if needed

## Troubleshooting

If you encounter issues:
1. Verify all required secrets are added
2. Check that secret names exactly match the filenames
3. Ensure the repository is private
4. Confirm you have valid DevExpress licenses
