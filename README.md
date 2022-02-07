# About
This repository contains the source for the Gymee Station software.

# Project Structure
The project has a MVVM architecture and is seperated to 4 categories with corresponding folders:  
1. Views - The screens themselves with necessary visual and frontend logic
2. View Models - Binds the views to models and performs orchestration logic
3. Models - Defines various data in the project and bussines logic relevant to it
4. Services - Includes classes to perform communication with external web systems and the camera itself

In cases of which a little viewmodel is required(or isn't at all), the code behind is used (*.cs classes).

# Requirements
The Gymee Station has a few external dependencies as follows:  

1. .NET Runtime 5 must be installed.
2. The Gymee Station requires Windows OS.
3. There must be a copy of ffmpeg.exe at the root level near the binary. ffmpeg can be downloaded [here](https://www.ffmpeg.org/download.html).
4. The Camera SDK must be installed (currently only Intel RealSense is supported).
5. Media is required to function properly. The default media can be found [here](https://google.com) with corresponding documentation.
6. There must be a json credential file for google drive, it's location can be configured in the  `drive_creds` field in GymeeConfig.json.

In addition, two configuration files must be present at the root level:  
1. GymeeConfig.json - includes general configuration data for various parts of the project
2. VidConfig.json - includes configuration data to split the training video (this is currently not in use)

# Installation

1. Download the latest distributable version from the [releases](https://github.com/YoavOber/Gymee/releases)
2. Unzip them where you wish
3. Configure the `GymeeConfig.json` file
4. Install the media according to the instructions

# Updating

1. Download the latest distributable version from the [releases](https://github.com/YoavOber/Gymee/releases)
2. Unzip to the install location, making sure **NOT** to override the json config files or media.

# On Screen Keyboard
See [here](https://support.microsoft.com/en-us/windows/use-the-on-screen-keyboard-osk-to-type-ecbb5e08-5b4e-d8c8-f794-81dbf896267a)
