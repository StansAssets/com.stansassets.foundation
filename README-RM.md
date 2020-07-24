### Draft Release Modifications (release-mods-workflow.yml)
Current workflow
* modifies/creates Draft release and modifies desired files (ex. package.json) -  `push`, `pull_request`
* adds pulls request title contents to release description - `pull_request, types: [closed]`
##### Dependencies
This workflow uses environment variables such as:
* `GH_API_TOKEN` (Github Api Token)
##### Workflow description
1. Workflow `env:` functions:
   * `GET_LATEST_RELEASE_INFO` - Gets latest release info. Sets `LATEST_RELEASE_TAG` and `LATEST_RELEASE_DRAFT_STATUS` variables
   * `CHANGE_RELEASE_TAG_BY_ONE` - Updates `LATEST_RELEASE_TAG` variable (X.Y.Z). Adds +1 to Z
   * `CREATE_NEW_DRAFT_RELEASE` - Creates new draft release, if latest release **is not** draft
   * `MODIFY_PACKAGE_JSON` - Modifies package json to be in sync with the latest draft release
   * `ADD_PULL_REQUEST_TITLE_CONTENTS` - Adds Pull Request title contents to latest draft release description
