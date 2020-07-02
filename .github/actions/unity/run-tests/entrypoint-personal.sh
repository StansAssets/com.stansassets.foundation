#!/usr/bin/env bash

echo "Writing UNITY_LICENSE content to file /root/.local/share/unity3d/Unity/Unity_lic.ulf"
echo "$UNITY_LICENSE" > /root/.local/share/unity3d/Unity/Unity_lic.ulf

ACTIVATION_OUTPUT=$(xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' \
    /opt/Unity/Editor/Unity \
      -batchmode \
      -nographics \
      -logFile /dev/stdout \
      -quit \
      -manualLicenseFile)

# The exit code for personal activation is always 1;
# Successful output should include the following:
#   "... Next license update check is after 2019-11-25T18:23:38"
ACTIVATION_SUCCESSFUL=$(echo $ACTIVATION_OUTPUT | grep 'Next license update check is after' | wc -l)

UNITY_EXIT_CODE=$?

# Set exit code to 0 if activation was successful
if [[ $ACTIVATION_SUCCESSFUL -eq 1 ]]; then
    echo "Activation was successful"
    UNITY_EXIT_CODE=0
fi;

if [ -z "$PR_FILES" ]
then
    xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' \
    /opt/Unity/Editor/Unity \
    -nographics \
    -projectPath $PROJECT_PATH \
    -runTests \
    -testPlatform $TEST_PLATFORM \
    -testResults $(pwd)/$TEST_PLATFORM-results.xml \
    -logFile /dev/stdout \
    -batchmode
else
    xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' \
    /opt/Unity/Editor/Unity \
    -nographics \
    -projectPath $PROJECT_PATH \
    -runTests \
    -testPlatform $TEST_PLATFORM \
    -testResults $(pwd)/$TEST_PLATFORM-results.xml \
    -testFilter $PR_FILES \
    -logFile /dev/stdout \
    -batchmode
fi
