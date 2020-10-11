# Foundation Library
Foundation Library is a free open source product that contains lots of useful APIs, Utilities and Extension methods. We do not provide any articles or tutorials for these plugins. But we do try to keep every public API documented and self-explanatory.

[![NPM Package](https://img.shields.io/npm/v/com.stansassets.foundation)](https://www.npmjs.com/package/com.stansassets.foundation)
[![openupm](https://img.shields.io/npm/v/com.stansassets.foundation?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.stansassets.foundation/)
[![Licence](https://img.shields.io/npm/l/com.stansassets.foundation)](https://github.com/StansAssets/com.stansassets.foundation/blob/master/LICENSE)
[![Issues](https://img.shields.io/github/issues/StansAssets/com.stansassets.foundation)](https://github.com/StansAssets/com.stansassets.foundation/issues)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/a22ff0164da04d089f68d8209dbe6c10)](https://app.codacy.com/gh/StansAssets/com.stansassets.foundation?utm_source=github.com&utm_medium=referral&utm_content=StansAssets/com.stansassets.foundation&utm_campaign=Badge_Grade_Dashboard)


[API Reference](https://api.stansassets.com/foundation/StansAssets.Foundation.html) | [Wiki](https://github.com/StansAssets/com.stansassets.foundation/wiki) | [Unity Forum](https://forum.unity.com/threads/free-stans-assets-foundation-library.975999/)

#### Quick links to explore the library:
* [Utilities.](https://api.stansassets.com/foundation/StansAssets.Foundation.html) The collection of various utility methods.
* [Editor Utilities.](https://api.stansassets.com/foundation/StansAssets.Foundation.Editor.html)The collection of utility methods for the Unity Editor.
* [Extensions.](https://api.stansassets.com/foundation/StansAssets.Foundation.Extensions.html) Extension methods for default C# and Unity Objects.
* [Patterns.](https://api.stansassets.com/foundation/StansAssets.Foundation.Patterns.html) Implementation of well-known design patterns like Singleton, Factory, Pool, etc.
* [UI Toolkit.](https://api.stansassets.com/foundation/StansAssets.Foundation.UIElements.html) Helper methods, extensions, utilities for the new Unity [UI Toolkit framework](https://docs.unity3d.com/Manual/UIElements.html).


### Install from NPM
* Navigate to the `Packages` directory of your project.
* Adjust the [project manifest file](https://docs.unity3d.com/Manual/upm-manifestPrj.html) `manifest.json` in a text editor.
* Ensure `https://registry.npmjs.org/` is part of `scopedRegistries`.
  * Ensure `com.stansassets` is part of `scopes`.
  * Add `com.stansassets.foundation` to the `dependencies`, stating the latest version.

A minimal example ends up looking like this. Please note that the version `X.Y.Z` stated here is to be replaced with [the latest released version](https://www.npmjs.com/package/com.stansassets.foundation) which is currently [![NPM Package](https://img.shields.io/npm/v/com.stansassets.foundation)](https://www.npmjs.com/package/com.stansassets.foundation).
  ```json
  {
    "scopedRegistries": [
      {
        "name": "npmjs",
        "url": "https://registry.npmjs.org/",
        "scopes": [
          "com.stansassets"
        ]
      }
    ],
    "dependencies": {
      "com.stansassets.foundation": "X.Y.Z",
      ...
    }
  }
  ```
* Switch back to the Unity software and wait for it to finish importing the added package.

### Install from OpenUPM
* Install openupm-cli `npm install -g openupm-cli` or `yarn global add openupm-cli`
* Enter your unity project folder `cd <YOUR_UNITY_PROJECT_FOLDER>`
* Install package `openupm add com.stansassets.foundation`

### Install from a Git URL
Yoy can also install this package via Git URL. To load a package from a Git URL:

* Open [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui.html) window.
* Click the add **+** button in the status bar.
* The options for adding packages appear.
* Select Add package from git URL from the add menu. A text box and an Add button appear.
* Enter the `https://github.com/StansAssets/com.stansassets.foundation.git` Git URL in the text box and click Add.
* You may also install a specific package version by using the URL with the specified version.
  * `https://github.com/StansAssets/com.stansassets.foundation.git#X.Y.X`
  * Please note that the version `X.Y.Z` stated here is to be replaced with the version you would like to get.
  * You can find all the available releases [here](https://github.com/StansAssets/com.stansassets.foundation/releases).
  * The latest available release version is [![Last Release](https://img.shields.io/github/v/release/stansassets/com.stansassets.foundation)](https://github.com/StansAssets/com.stansassets.foundation/releases/latest)

For more information about what protocols Unity supports, see [Git URLs](https://docs.unity3d.com/Manual/upm-git.html).

## About Us
We are committed to developing high quality and engaging entertainment software. Our mission has been to bring a reliable and high-quality Unity Development service to companies and individuals around the globe. 
At Stan's Assets, we make Plugins, SDKs, Games, VR & AR Applications. Do not hesitate do get in touch, whether you have a question, want to build something, or just to say hi :) [Let's Talk!](mailto:stan@stansassets.com)

[Website](https://stansassets.com/) | [LinkedIn](https://www.linkedin.com/in/lacost/) | [Youtube](https://www.youtube.com/user/stansassets/videos) | [Github](https://github.com/StansAssets) | [AssetStore](https://assetstore.unity.com/publishers/2256)
