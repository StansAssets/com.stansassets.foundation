#!/usr/bin/env bash

ACTIVATION_OUTPUT=$(xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' \
    /opt/Unity/Editor/Unity \
    -logFile /dev/stdout \
    -batchmode \
    -quit \
    -username "$UNITY_USERNAME" \
    -password "$UNITY_PASSWORD")

ALF_CONTENTS=$(echo $ACTIVATION_OUTPUT | grep -o -P '(?=<\?xml).*(?<=</root>)')
echo "$ALF_CONTENTS" > $(pwd)/$UNITY_VERSION.alf
