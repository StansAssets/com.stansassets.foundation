# Unity Actions Manual
###### ***All workflows depend on [gableroux docker images](https://hub.docker.com/r/gableroux/unity3d/tags)***
***
### Request Unity Personal activation file (personal-license-request.yml)
Current workflow Requests Unity Personal Activation file for specific Version of Unity.
With the help of which we can activate Unity later.
##### Dependencies
This workflow uses environment variables such as:
* `UNITY_LOGIN` (Unity username)
* `UNITY_PASS` (Unity password)
##### Workflow description
Unity Version can be changed by editing the following block of code:
```
matrix:
    unityVersion:
        - 2019.4.1f1
```
It is also possible to request multiple activation files just by adding additional versions
```
matrix:
    unityVersion: [2019.4.1f1, 2019.4.0f1]
```
##### Working with the workflow
1. Trigger the workflow
1. Wait for it to finish.
1. Download artifact. Use .alf file in the next step.
1. Proceed to [Unity Manual Activation Web Page](https://license.unity3d.com/manual)
    * Choose / Upload the file you saved earlier (alf)
    * Select Unity Personal Edition
    * Answer required questions
    * Save the .ulf file.

1. Copy the contents of .ulf license file and paste it to your environment variable.
***
### Full CI. Personal License (full-ci-personal.yml)
Current workflow runs **all available** tests inside specified Unity Projects using Personal Unity License.
##### Dependencies
This workflow uses environment variables such as:
* `UNITY_LICENSE_2019_4_0f1`
##### Workflow description
1. Steps Explanation
   * `Checkout` - Checks repository out.
   * `Cache` - Caches Library contents on *successful run* (yes, it is a limitation from github side). With the help of this, Library won't be rebuilt next time. This makes workflow run faster.
   * `Run Tests` - Runs **all available** tests in projects with specified parameters
   * `Upload results` - Upload results artifact, if `Run Tests` step has **failed** (there is no point seeing successful results)
1. Tweaking the run for you needs
   * `matrix:`
        * `projectPath` - can be one or array of projects
        * `targetPlatform` - acceptable parameters are `editmode` and/or `playmode`
        * `unityVersion` - can be one or array of versions
1. Workflow `env:` functions:
   * `DOCKER_ACTIONS_PERSONAL` - Creates and runs docker image
   * `GET_PERSONAL_LICENSE_CONTENTS` - Changes `UNITY_LICENSE_CONTENTS` based on Unity version used.
1. Since github actions don't support usage of a variable in `unityVersion` field, we are using `GET_PERSONAL_LICENSE_CONTENTS` function. It must be updated, if you will decide to use more or different versions of Unity.
1. `Run Tests` `env:` must be supplemented by additional license environment variables, if you will decide to use more or different versions of Unity.
##### Working with the workflow
1. Trigger the workflow
1. Wait for it to finish.
1. Download failed tests artifacts, if there are any.
***
### Full CI. Pro/Plus License (full-ci-proplus.yml)
Current workflow runs **all available** tests inside specified Unity Projects using Pro/Plus Unity License.
##### Dependencies
This workflow uses environment variables such as:
* `UNITY_LOGIN` (Unity username)
* `UNITY_PASS` (Unity password)
* `UNITY_KEY` (Unity pro/plus license key)
##### Workflow description
1. Steps Explanation
   * `Checkout` - Checks repository out.
   * `Cache` - Caches Library contents on *successful run* (yes, it is a limitation from github side). With the help of this, Library won't be rebuilt next time. This makes workflow run faster.
   * `Run Tests` - Runs **all available** tests in projects with specified parameters
   * `Upload results` - Upload results artifact, if `Run Tests` step has **failed** (there is no point seeing successful results)
1. Tweaking the run for you needs
   * `matrix:`
        * `projectPath` - can be one or array of projects
        * `targetPlatform` - acceptable parameters are `editmode` and/or `playmode`
        * `unityVersion` - can be one or array of versions
1. Workflow `env:` functions:
   * `DOCKER_ACTIONS_PROPLUS` - Creates and runs docker image
##### Working with the workflow
1. Trigger the workflow
1. Wait for it to finish.
1. Download failed tests artifacts, if there are any.
***
### Selective CI on Pull Request. Personal License (pr-selective-ci-personal.yml)
Current workflow runs **PR tests only** inside specified Unity Projects using Personal Unity License.
##### Dependencies
This workflow uses environment variables such as:
* `UNITY_LICENSE_2019_4_0f1`
* `GH_API_TOKEN` - Github API token
##### Workflow description
1. Steps Explanation
   * `Checkout` - Checks repository out.
   * `Cache` - Caches Library contents on *successful run* (yes, it is a limitation from github side). With the help of this, Library won't be rebuilt next time. This makes workflow run faster.
   * `Run Tests` - Runs **PR tests only** in projects with specified parameters
   * `Upload results` - Upload results artifact, if `Run Tests` step has **failed** (there is no point seeing successful results)
1. Tweaking the run for you needs
   * `matrix:`
        * `projectPath` - can be one or array of projects
        * `targetPlatform` - acceptable parameters are `editmode` and/or `playmode`
        * `unityVersion` - can be one or array of versions
1. Workflow `env:` functions:
   * `DOCKER_ACTIONS_PERSONAL` - Creates and runs docker image
   * `GET_PR_FILENAMES` - Gets Pull Request filenames
   * `GET_PERSONAL_LICENSE_CONTENTS` - Changes `UNITY_LICENSE_CONTENTS` based on Unity version used.
1. Since github actions don't support usage of a variable in `unityVersion` field, we are using `GET_PERSONAL_LICENSE_CONTENTS` function. It must be updated, if you will decide to use more or different versions of Unity.
1. `Run Tests` `env:` must be supplemented by additional license environment variables, if you will decide to use more or different versions of Unity.
##### Working with the workflow
1. Trigger the workflow by creating a Pull Request and assigning `test-pr-files-only` label.
1. Wait for it to finish.
1. Download failed tests artifacts, if there are any.
***
### Selective CI on Pull Request. Pro/Plus License (pr-selective-ci-proplus.yml)
Current workflow runs **PR tests only** inside specified Unity Projects using Pro/Plus Unity License.
##### Dependencies
This workflow uses environment variables such as:
* `UNITY_LOGIN` (Unity username)
* `UNITY_PASS` (Unity password)
* `UNITY_KEY` (Unity pro/plus license key)
* `GH_API_TOKEN` (Github API token)
##### Workflow description
1. Steps Explanation
   * `Checkout` - Checks repository out.
   * `Cache` - Caches Library contents on *successful run* (yes, it is a limitation from github side). With the help of this, Library won't be rebuilt next time. This makes workflow run faster.
   * `Run Tests` - Runs **PR tests only** in projects with specified parameters
   * `Upload results` - Upload results artifact, if `Run Tests` step has **failed** (there is no point seeing successful results)
1. Tweaking the run for you needs
   * `matrix:`
        * `projectPath` - can be one or array of projects
        * `targetPlatform` - acceptable parameters are `editmode` and/or `playmode`
        * `unityVersion` - can be one or array of versions
1. Workflow `env:` functions:
   * `DOCKER_ACTIONS_PROPLUS` - Creates and runs docker image
   * `GET_PR_FILENAMES` - Gets Pull Request filenames
##### Working with the workflow
1. Trigger the workflow by creating a Pull Request and assigning `test-pr-files-only` label.
1. Wait for it to finish.
1. Download failed tests artifacts, if there are any.
***
