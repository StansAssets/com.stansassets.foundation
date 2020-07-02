#!/usr/bin/env bash

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
    -logFile /dev/stdout \
    -batchmode \
    -quit \
    -username "$UNITY_USERNAME" \
    -password "$UNITY_PASSWORD" \
    -serial "$UNITY_SERIAL"

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
    echo "Activation was successful";
    
    if [ -z "$PULL_REQUEST_URL" ]
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
        -testFilter TestsAdditionalFile \
        -logFile /dev/stdout \
        -batchmode
    fi
      
    ${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
        -batchmode \
        -quit \
        -returnlicense
else
    echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

exit $UNITY_EXIT_CODE
