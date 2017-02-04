# WcoeJobFairRegistration
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Student/employer registration system for TTU College of Engineering Job Fair

### Functions

 * Check-in TTU students with their student ID
 * Print student and employer information
 * Track student and employer attendance
 
### Prerequisites :floppy_disk:

 * Install the [DYMO SDK](http://www.dymo.com/en-US/dymo-user-guides/dymo-user-guides/dymo-label-v8-software-developers-kit-dymo-sdk-v8-windows-p)
 * Have a DYMO LabelWriter
 * Have a magnetic card reader

### For Everyone :busts_in_silhouette:
To use this application outside of a development environment, you will need to:

 1. Install the [Prerequisites](#prerequisites)
 2. Download the installer [here]()
 3. Install and enjoy! :smile:

### For Developers :octocat:
If you are experiencing printer errors during development:

 * Make sure that you have the DYMO `.dll`s
 * Make sure that `WcoeJobFairRegistration.csproj` is targeting `Any CPU`
  1. In Visual Studio, `right-click` the WcoeJobFairRegistration project
  2. Select `Properties`
  3. On the left side, select the `Build` tab
  4. Choose `Any CPU` from the `Platform Target` drop-down list