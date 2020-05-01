# Foundation Library
Foundation Library is a free open source product that contains lots of useful APIs, Utilities and Extension methods. We do not provide any articles or tutorials for these plugins. But we do try to keep every public API documented and self-explanatory.

[![NPM Package](https://img.shields.io/npm/v/com.stansassets.foundation)](https://www.npmjs.com/package/com.stansassets.foundation)
[![Licence](https://img.shields.io/npm/l/com.stansassets.foundation)](https://github.com/StansAssets/com.stansassets.foundation/blob/master/LICENSE)
[![Issues](https://img.shields.io/github/issues/StansAssets/com.stansassets.foundation)](https://github.com/StansAssets/com.stansassets.foundation/issues)


[API Reference](https://api.stansassets.com/foundation/StansAssets.Foundation.html)

#### Quick links to explore the library:
* [Utilities.](https://api.stansassets.com/foundation/StansAssets.Foundation.html) The collection of various utility methods.
* [Editor Utilities.](https://api.stansassets.com/foundation/StansAssets.Foundation.Editor.html)The collection of utility methods for the Unity Editor.
* [Extensions.](https://api.stansassets.com/foundation/StansAssets.Foundation.Extensions.html) Extension methods for default C# and Unity Objects.
* [Patterns.](https://api.stansassets.com/foundation/StansAssets.Foundation.Patterns.html) Implementation of well-known design patterns like Singleton, Factory, Pool, etc.
* [UI Toolkit.](https://api.stansassets.com/foundation/StansAssets.Foundation.UIElements.html) Helper methods, extensions, utilities for the new Unity [UI Toolkit framework](https://docs.unity3d.com/Manual/UIElements.html).

#### How to instal
* Navigate to the `Packages` directory of your project.
* Adjust the [project manifest file](https://docs.unity3d.com/Manual/upm-manifestPrj.html) `manifest.json` in a text editor.
* Ensure `https://registry.npmjs.org/` is part of `scopedRegistries`.
  * Ensure `com.stansassets` is part of `scopes`.
  * Add `com.stansassets.foundation` to the `dependencies`, stating the latest version.

A minimal example ends up looking like this. Please note that the version `X.Y.Z` stated here is to be replaced with [the latest released version](https://github.com/StansAssets/com.stansassets.foundation/releases/latest) which is currently [![Release][Version-Release]][Releases].
  ```json
  {
    "scopedRegistries": [
      {
        "name": "npmjs",
        "url": "https://registry.npmjs.org/",
        "scopes": [
          "com.stansasset"
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
